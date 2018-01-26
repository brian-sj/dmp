using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMP.DataModels;
using System.Xml.Linq;
using System.Xml;
using System.Collections.ObjectModel;

namespace DMP.util
{
    public class ListToXML
    {
        /// <summary>
        /// TargetPointList 와 WayPointList를 xml로 보낸다.
        /// </summary>
        /// <returns></returns>
        public XElement ListAllToXML()
        {
            XElement tp = TPListToXML( GvarDesignModel.Instance.TPList );
            XElement wp = WPListToXML( GvarDesignModel.Instance.WPList );
            XElement home = HomePositionToXML( GvarDesignModel.Instance.HomePosition);
            XElement option = DefaultOptionToXML(GvarDesignModel.Instance );
            tp.Add( wp.Nodes() );
            tp.Add(home.Nodes());
            tp.Add(option.Nodes());
            return tp;
        }
        
        public XElement DefaultOptionToXML(GvarDesignModel g)
        {
            XElement waypoint = new XElement("options");

            return new XElement("List",
                new XElement("option"
                         ,new XAttribute("initialheight" , g.InitialHeight) 
                        , new XAttribute("speed", g.Speed)
                        , new XAttribute("landingheight", g.Landing_height)
                        , new XAttribute("landingstyle", g.Landing_style)
                        , new XAttribute("initialheighttype", g.InitialHeightType)
                        , new XAttribute("headingtype", g.Headingtype)
                        , new XAttribute("showheight", g.ShowHeight)
                        , new XAttribute("showdistance", g.ShowDistance)
                    //, new XAttribute("landingposition", g.LangdingPosition)

                    )
                );
        }
        /// <summary>
        /// HomePosition 도 별도로 저장 
        /// </summary>
        /// <param name="wm"></param>
        /// <returns></returns>
        public XElement HomePositionToXML( WayPointModel wm )
        {

            //XElement waypoint = new XElement("waypoint");

            return new XElement("List", 
                new XElement("waypoint" ,
                        new XAttribute("index", wm.Index),
                        new XAttribute("latitude", wm.Latitude),
                        new XAttribute("longitude", wm.Longitude),
                        new XAttribute("height", wm.Height),
                        new XAttribute("speed", wm.Speed),
                        new XAttribute("bearing", wm.Bearing),
                        new XAttribute("target", wm.Target),
                        new XAttribute("name", wm.Name),
                        new XAttribute("pointtype", (int)PointType.HOME)
                 )
             );
        }
        /// <summary>
        /// TargetPoint는 별도로....
        /// </summary>
        /// <param name="tpList"></param>
        /// <returns></returns>
        public XElement  TPListToXML( ObservableCollection<WayPointModel> tpList)
        {
            return new XElement( "List" , from item in tpList
                                                select new XElement("waypoint",
                                                        new XAttribute("index", item.Index),
                                                        new XAttribute("latitude", item.Latitude),
                                                        new XAttribute("longitude", item.Longitude),
                                                        new XAttribute("height", item.Height),
                                                        new XAttribute("speed", item.Speed),
                                                        new XAttribute("bearing", item.Bearing),
                                                        new XAttribute("target", item.Target),
                                                        new XAttribute("name", item.Name),
                                                            new XAttribute("pointtype", (int)PointType.TARGET )

                                                            )
                                                );
        }
        /// <summary>
        /// WayPoint도 별도로 저장
        /// </summary>
        /// <param name="wpList"></param>
        /// <returns></returns>
        public XElement WPListToXML (ObservableCollection<WayPointModel>wpList)
        {
            return new XElement("List" , from item in wpList
                                         select new XElement( "waypoint",
                                                        new XAttribute("index" , item.Index),
                                                        new XAttribute("latitude", item.Latitude),
                                                        new XAttribute("longitude", item.Longitude),
                                                        new XAttribute("height", item.Height),
                                                        new XAttribute("speed", item.Speed),
                                                        new XAttribute("bearing", item.Bearing),
                                                        new XAttribute("target", item.Target),
                                                        new XAttribute("name", item.Name),
                                                        new XAttribute("pointtype",(int) PointType.WAYPOINT)
                                            )
                                         );
        }
       

        /// <summary>
        /// xml을 읽어서 ObservableCollection로 반환하는 함수 . GvarModel에 넣어준다... Relay는 아직 없다. 
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        //public List<WayPointModel> WPListFromXML(XDocument xdoc)
        public void WPListFromXML(XDocument xdoc)
        {
            IEnumerable<XElement> targets = xdoc.Root.Elements("waypoint");
            ObservableCollection<WayPointModel> wplist = new ObservableCollection<WayPointModel>();
            ObservableCollection<WayPointModel> tplist = new ObservableCollection<WayPointModel>();
            ObservableCollection<WayPointModel> relist = new ObservableCollection<WayPointModel>();
            
            foreach (var item in targets)
            {
                try {
                    WayPointModel wm = new WayPointModel();
                    wm.Index = (int)item.Attribute("index");
                    wm.Height = (float)item.Attribute("height");
                    wm.Latitude = (double)item.Attribute("latitude");
                    wm.Longitude = (double)item.Attribute("longitude");
                    wm.Speed = (float)item.Attribute("speed");
                    wm.Bearing   = (float)item.Attribute("bearing");
                    wm.Target = (int)item.Attribute("target");
                    wm.Name = item.Attribute("name").Value;
                    wm.PointType = (int)item.Attribute("pointtype");
                    if (wm.PointType == (int)PointType.TARGET)
                    {
                        tplist.Add(wm);
                    }
                    else if (wm.PointType == (int)PointType.HOME)
                    {
                        GvarDesignModel.Instance.HomePosition = wm;
                    }
                    else 
                    {
                        wplist.Add(wm);   // 일단 나머지는 다 WayPoint에 넣는다. 
                    }
                }
                catch (Exception ) { DMP.Dialogs.CustomMessageBox.Show("XML 파일에 이상이 있습니다. 확인 바랍니다."); }
            }

            GvarDesignModel.Instance.WPList = wplist;
            GvarDesignModel.Instance.TPList = tplist;
            
            //return list;
        }

        public void OptionsFromXML(XDocument xdoc )
        {
            IEnumerable<XElement> targets = xdoc.Root.Elements("options");
            ObservableCollection<WayPointModel> wplist = new ObservableCollection<WayPointModel>();
            ObservableCollection<WayPointModel> tplist = new ObservableCollection<WayPointModel>();
            ObservableCollection<WayPointModel> relist = new ObservableCollection<WayPointModel>();

            var g = GvarDesignModel.Instance;

            foreach (var item in targets)
            {
                try
                {


                    g.InitialHeight = (int)item.Attribute("initialheight");
                    g.Speed = (float)item.Attribute("speed");
                    g.Landing_height = (float)item.Attribute("landingheight");
                    g.Landing_style = (int)item.Attribute("landingstyle");
                    g.InitialHeightType = (int)item.Attribute("initialheighttype");
                    g.Headingtype = (int)item.Attribute("headingtype");
                    g.ShowHeight = (int)item.Attribute("showdistance");
                    g.ShowDistance = (int)item.Attribute("showheight");
                    //g.LandingPosition = (int)item.Attribute("landingposition");

                }
                catch (Exception) { DMP.Dialogs.CustomMessageBox.Show("XML 파일에 이상이 있습니다. 확인 바랍니다."); }
            }
        }


    }
}
