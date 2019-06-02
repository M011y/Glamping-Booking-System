using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CA3_S00191042_MollyCandon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //declaring database
        CA3_S00191042Entities db = new CA3_S00191042Entities();

        #region Properties
        //properties for arrival date, departure date and suite
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Suite { get; set; }
        #endregion

        #region ComboBox and DatePickers
        //populate combo box on load, and exclude past dates from date pickers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //creating list for combobox
            IList<Person> people = new List<Person>();
            //adding objects to list
            people.Add(new Person("1"));
            people.Add(new Person("2"));
            people.Add(new Person("3+"));

            //setting combobox source to list
            cbxCapacity.ItemsSource = people;

            //excluding past dates from date pickers
            dpArrivalDate.DisplayDateStart = DateTime.Today.AddDays(0);
            dpDepartureDate.DisplayDateStart = DateTime.Today.AddDays(1);
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Search Button
        //when search button is clicked
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //determine what capacity was selected
            Person selectedCapacity = cbxCapacity.SelectedItem as Person;

            //getting values for dates
            ArrivalDate = dpArrivalDate.SelectedDate.Value;
            DepartureDate = dpDepartureDate.SelectedDate.Value;

            //checking if date is available - flags where it is not
            var query = from b in db.Bookings
                        where (!(ArrivalDate >= b.ArrivalDate && ArrivalDate < b.DepartureDate) && (DepartureDate > b.ArrivalDate && DepartureDate <= b.DepartureDate))
                        select b.SuitesId;

            //search through database for suites with that capacity, excludes entries where SuitesID has been flagged in above query
            //-- problem: NOT WORKING only checks the last booking, and only if changed??
            var query1 = from s in db.Suites
                         where (s.Capacity.Equals(selectedCapacity.Capacity))
                         && (s.Id != query.ToList().FirstOrDefault())
                         select s.Name;

            //return results in list box
            lbxSuites.ItemsSource = query1.ToList();
        }
        #endregion

        #region Details
        //showing suite details on selection
        private void lbxSuites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //determine what suite was selected
            string selectedSuite = lbxSuites.SelectedItem as string;

            if (selectedSuite != null)
            {
                //search through database for suite
                var query = from s in db.Suites
                            where s.Name.Equals(selectedSuite)
                            select s;

                //declaring Suite value to be used for new database entry
                Suite = query.ToList().First().Id;

                //return results in details text block
                tblkSuiteDetails.Text = query.ToList().First().GetDetails() + ($"\nArrival date: {ArrivalDate}" +
                    $"\nDeparture date: {DepartureDate}");
            }
        }
        #endregion

        #region Book Button
        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            //new booking object
            Booking b = new Booking()
            {
                ArrivalDate = ArrivalDate,
                DepartureDate = DepartureDate,
                SuitesId = Suite
            };

            //add to database
            db.Bookings.Add(b);

            //save changes
            db.SaveChanges();

            //adding new window object
            ConfirmationWindow confirmation = new ConfirmationWindow
            {
                //set owner of add window to main window to allow access to collection
                Owner = this
            };

            //displays new window - won't allow user back to main window without interaction
            confirmation.ShowDialog();

            //reset details box
            tblkSuiteDetails.Text = string.Empty;
        }
        #endregion
    }
}