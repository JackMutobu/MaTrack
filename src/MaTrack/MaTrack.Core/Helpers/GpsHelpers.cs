using MaTrack.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaTrack.Core.Helpers
{
    public static class GpsHelpers
    {
        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }
        public static double DistanceTo(this LocationDto baseCoordinates, LocationDto targetCoordinates)
        {
            return DistanceTo(baseCoordinates, targetCoordinates, UnitOfLength.Kilometers);
        }

        public static double DistanceTo(this LocationDto baseCoordinates, LocationDto targetCoordinates, UnitOfLength unitOfLength)
        {
            var baseRad = Math.PI * double.Parse(baseCoordinates.Latitude) / 180;
            var targetRad = Math.PI * double.Parse(targetCoordinates.Latitude) / 180;
            var theta = double.Parse(baseCoordinates.Longitude) - double.Parse(targetCoordinates.Longitude);
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unitOfLength.ConvertFromMiles(dist);
        }
        public class UnitOfLength
        {
            public static UnitOfLength Kilometers = new UnitOfLength(1.609344);
            public static UnitOfLength NauticalMiles = new UnitOfLength(0.8684);
            public static UnitOfLength Miles = new UnitOfLength(1);

            private readonly double _fromMilesFactor;

            private UnitOfLength(double fromMilesFactor)
            {
                _fromMilesFactor = fromMilesFactor;
            }

            public double ConvertFromMiles(double input)
            {
                return input * _fromMilesFactor;
            }
        }
    }
}
