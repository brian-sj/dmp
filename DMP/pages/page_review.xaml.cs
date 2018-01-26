using DMP.util;
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
using DMP;
using DMP.DataModels;
using Microsoft.Win32;
using System.Xml.Linq;

namespace DMP
{
    /// <summary>
    /// Page2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageReview : Page
    {
        public PageReview()
        {
            InitializeComponent();

            /*
            tbMaxWpDistance.Text = String.Format("Max. WP {0}",  GvarDesignModel.Instance.MaxWPDistance    );
            tbTotalDistance.Text = String.Format("여행거리 : {0}m" , GvarDesignModel.Instance.TotalFlightDistance );
            tbTotalFlightTime.Text = String.Format(" 시간 : {0} 초" ,   100   );
            tbAvgSpeed.Text = String.Format(" 평균 속도 : {0} m/s", GvarDesignModel.Instance.AvgSpeed );
            tbTotalTarget.Text = String.Format(" 총 타겟 수 : {0} 개 ", GvarDesignModel.Instance.ITotalTPCount );
            tbTotalWP.Text = String.Format(" 총 WP 수 : {0} 개 ", GvarDesignModel.Instance.ITotalWPCount );
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListToXML xmlcon = new ListToXML();

            XDocument xml = new XDocument();
            XElement xmlele;
            //xml.Add( xmlcon.TPListToXML( GvarDesignModel.Instance.TargetPointList));
            //xml.Add( xmlcon.WPListToXML(GvarDesignModel.Instance.WaypointList));

            xmlele = xmlcon.ListAllToXML();
            //xmlele = xmlcon.TPListToXML(GvarDesignModel.Instance.TargetPointList);
            //xmlele.Add( xmlcon.WPListToXML(GvarDesignModel.Instance.WaypointList).Nodes());

            xml.Add(xmlele);
            //var list = xmlcon.TPListFromXML(xml);

            SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "xml documents (.xml)|*.xml"; // Filter files by extension

            dlg.Filter = "XML-File | *.xml";
            if (dlg.ShowDialog() == true)
            {
                xml.Save(dlg.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnSendToDrone_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
