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
using System.Drawing;

namespace DMP.Controls
{
    /// <summary>
    /// HUD.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HUD : UserControl
    {

        public System.Drawing.Image bgimage { get; set; }
        public HUD()
        {
            InitializeComponent();
        }
    }
}
