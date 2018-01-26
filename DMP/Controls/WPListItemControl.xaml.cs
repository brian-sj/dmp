using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// WPListItemControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WPListItemControl : UserControl
    {
        public WPListItemControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Max ,Min 값 처리를 더 해줘야 한다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initialHeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
