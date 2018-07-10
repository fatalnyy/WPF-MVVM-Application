using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessingModel
{
    public class ImageModel
    {
        private string _imagePath;
        private long _processedTime;
        private BitmapImage _processedImage;

        /// <summary>
        /// Properties for ImageModel fields
        /// </summary>
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
            }
        }

        public BitmapImage ProcessedImage
        {
            get
            {
                return _processedImage;
            }
            set
            {
                _processedImage = value;
            }
        }

        public long ProcessedTime
        {
            get
            {
                return _processedTime;
            }
            set
            {
                _processedTime = value;
            }
        }
    }
}
