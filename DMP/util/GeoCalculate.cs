using DMP.DataModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP.util
{
    public class GeoCalculate
    {

        public static double CalculateBearing( Location latlng1 , Location latlng2)
        {
            /*
            var lat1 = deg2rad(latlng1.Latitude);
            var lon1 = latlng1.Longitude;
            
            var lat2 = deg2rad(latlng2.Latitude);
            var lon2 = latlng2.Longitude;
            var dLon = deg2rad(lon2 - lon1);

            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);

            var brng = ( rad2deg(Math.Atan2(y, x)) + 360) % 360;
            return brng;
            */

            double latFrom = latlng1.Latitude * (Math.PI / 180.0);
            double longFrom = latlng1.Longitude * (Math.PI / 180.0);
            double latTo = latlng2.Latitude * (Math.PI / 180.0);
            double longTo = latlng2.Longitude * (Math.PI / 180.0);

            double y = Math.Sin(longTo - longFrom) * Math.Cos(latTo);
            double x = (Math.Cos(latFrom) * Math.Sin(latTo)) -
                (Math.Sin(latFrom) * Math.Cos(latTo) * Math.Cos(longFrom - longTo));

            double rval = Math.Atan2(y, x) ;
            double angle = (rval * (180.0 / Math.PI) + 360) % 360;
            return angle;// back to degrees
        }

        public static double Distance(double lat1, double lon1, double lat2, double lon2, string unit = "m")
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;  //  state miles M 
            if (unit == "m")     // miter 
            {
                dist = dist * 1.609344 * 1000;
            }
            else if (unit == "N")  // Nautical Miles 
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
        public static double Distance(Location location1, Location location2)
        {
            return Distance( location1.Latitude , location1.Longitude , location2.Latitude , location2.Longitude , "m");
        }
        public static double Distance(WayPointModel from , WayPointModel to)
        {
            return Distance( from.Latitude , from.Longitude , to.Latitude , to.Longitude ,"m");
        }
        public static double CalculateDistanceTotal( params Location[] locations  )
        {
            double totalDistance = 0.0;
            for ( int i=0; i< locations.Length -1; i++)
            {
                Location current = locations[i];
                Location next = locations[i + 1];

                totalDistance += Distance(current , next);
            }

            return totalDistance;
        } 

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
