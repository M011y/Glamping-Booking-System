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
using System.Windows.Shapes;

namespace CA3_S00191042_MollyCandon
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        //declaring database
        CA3_S00191042Entities db = new CA3_S00191042Entities();

        public ConfirmationWindow()
        {
            InitializeComponent();
        }

        #region Window Loaded
        //textbox filled on load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //selecting all from bookings
            var query1 = from b in db.Bookings
                         select b;

            //selecting from suites where suiteID matches most recent bookingID
            var query2 = from s in db.Suites
                         where s.Id.Equals(query1.ToList().FirstOrDefault().SuitesId)
                         select s;

            //printing to screen
            tblkConfirmation.Text = query2.ToList().First().ToString() + query1.ToList().First().ToString();
        }
        #endregion

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            //close window
            this.Close();
        }
    }
}
