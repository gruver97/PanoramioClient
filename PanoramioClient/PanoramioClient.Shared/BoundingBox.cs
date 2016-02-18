using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace PanoramioClient
{
    /// <summary>
    /// Discribes a rectangular region. This region usually encloses a set of geometries or represents a area of view.
    /// </summary>
    public class BoundingBox
    {


        /// <summary>
        /// Discribes a rectangular region. This region usually encloses a set of geometries or represents a area of view.
        /// </summary>
        /// <param name="center">Center of bounding box</param>
        /// <param name="width">Width of bounding box in degress</param>
        /// <param name="height">Height of bounding box in degress</param>
        public BoundingBox(BasicGeoposition center, double width, double height)
        {
            Width = width;
            Height = height;
            Center = center;          
        }


        /// <summary>
        /// North Latitude Coordinate
        /// </summary>
        public double MaxY => Center.Latitude + Height / 2;

        /// <summary>
        /// South Latitude Coordinate
        /// </summary>
        public double MinY => Center.Latitude - Height / 2;

        /// <summary>
        /// Most Easterly Longitude Coordinate (right side of bounding box)
        /// </summary>
        public double MaxX => Center.Longitude + Width / 2;

        /// <summary>
        /// Most Westerly Longitude Coordinate (left side of bounding box)
        /// </summary>
        public double MinX => Center.Longitude - Width / 2;

        /// <summary>
        /// Width of the bounding box in degress
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of the bounding box in degress
        /// </summary>
        public double Height { get; set; }

        public BasicGeoposition Center { get; set; }
    }
}
