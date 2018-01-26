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

namespace DMP.Controls
{
    /// <summary>
    /// flDashBoard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class flDashBoard : System.Windows.Controls.UserControl
    {
        public flDashBoard()
        {
            InitializeComponent();
        }



        private void ShowCustomerDetail()
        {
            
        }

        private void HideCustomerDetail()
        {
            
        }

        private void DoHideCustDetail(object sender, RoutedEventArgs e)
        {
            HideCustomerDetail();
        }

        private void DoCustomerDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ShowCustomerDetail();  
        }
    }
}
