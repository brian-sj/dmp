using DMP.Controls.pushpin;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DMP.DataModels
{
    class MapModel : ViewModelBase
    {
        MapLayer _ml_pushpin;   // 푸시핀및 툴팁 그린다. 
        MapLayer _ml_flight ;   // 비행경로 그린다. 
        MapLayer _ml_polyline;  // 폴리라인 그린다. 
        Map _map;






        public ControlTemplate Ttemplate { get;  set; } //= (ControlTemplate)Application.Current.Resources["CustomTPushpinTemplate"];
        public ControlTemplate Wtemplate { get;  set; }//= (ControlTemplate)Application.Current.Resources["CustomPushpinTemplate"];
        public ControlTemplate Htemplate { get;  set; }// = (ControlTemplate)Application.Current.Resources["CustomHPushpinTemplate"];



        ObservableCollection<DMPPushpin> _pushpins;

        public int MapStatus { get; set;} 

        public Map DMap { get => _map; set { _map = value; OnPropertyChanged(); } }
        public MapLayer DmlPushpin { get => _ml_pushpin; set { _ml_pushpin = value; OnPropertyChanged(); } }
        public MapLayer DmlFlight { get => _ml_flight; set => _ml_flight = value; }  // 바인딩 안했음. 
        public MapLayer DmlPolyline { get => _ml_polyline; set => _ml_polyline = value; }  // 바인딩 안했음. 

        internal ObservableCollection<DMPPushpin> DPushpins { get => _pushpins; set { _pushpins = value; OnPropertyChanged(); } }
    }
}
