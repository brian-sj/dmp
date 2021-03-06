﻿using log4net;
using MissionPlanner;
using MissionPlanner.Comms;
using MissionPlanner.Mavlink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace DMP
{
    /// <summary>
    /// page_connect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageConnect : Page
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private bool skipconnectcheck = false;
        public PageConnect()
        {
            InitializeComponent();
        }
        
    }
}
