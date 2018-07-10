using ImageProcessingViewModel.ViewModels.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageProcessingModel;
using System.Windows;

namespace ImageProcessingViewModel.ViewModels
{
    public class ImageProcessing : INotifyPropertyChanged
    {
        public OpenFileDialogCommand OpenFileDialogCommand { get; set; }
        public SaveFileDialogCommand SaveFileDialogCommand { get; set; }
        public TransformToGrayScaleCommand TransformToGrayScaleCommand { get; set; }
        private ImageModel imageModel = new ImageModel();

        public ImageProcessing()
        {
            OpenFileDialogCommand = new OpenFileDialogCommand(this);
            SaveFileDialogCommand = new SaveFileDialogCommand(this);
            TransformToGrayScaleCommand = new TransformToGrayScaleCommand(this);
        }

        /// <summary>
        /// Properties for ImageModel object called imageModel
        /// </summary>
        public string ImagePathVM
        {
            get
            {
                return imageModel.ImagePath;
            }
            set
            {
                imageModel.ImagePath = value;
                OnPropertyChanged("ImagePathVM");
            }
        }
        public BitmapImage ProcessedImageVM
        {
            get
            {
                return imageModel.ProcessedImage;
            }
            set
            {
                imageModel.ProcessedImage = value;
                OnPropertyChanged("ProcessedImageVM");
            }
        }
        public long ProcessedTimeVM
        {
            get
            {
                return imageModel.ProcessedTime;
            }
            set
            {
                imageModel.ProcessedTime = value;
                OnPropertyChanged("ProcessedTimeVM");
            }
        }

        public void LoadImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = (".jpg");
            dialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png;";

            if (dialog.ShowDialog() == true)
                ImagePathVM = dialog.FileName;
        }

        public void SaveImage()
        {
           SaveFileDialog dialog = new SaveFileDialog();
           dialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png;";
           dialog.FilterIndex = 3;

           
           if (dialog.ShowDialog() == true)
           {
                Bitmap finalImage = BmpImage2Bmp(ProcessedImageVM);
                finalImage.Save(dialog.FileName);
           }

        }

        /// <summary>
        /// Method for converting Bitmap to BitmapImage
        /// </summary>
        private BitmapImage Bmp2BmpImage(Bitmap bmp)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bmp.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.StreamSource = stream;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.EndInit();
                return bmpImage;
            }
        }

        /// <summary>
        /// Method for converting BitmapImage to Bitmap
        /// </summary>
        private Bitmap BmpImage2Bmp(BitmapImage bmpImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bmpImage));
                enc.Save(stream);
                Bitmap bmp = new Bitmap(stream);
                return new Bitmap(bmp);
            }
        }

        /// <summary>
        /// Method to transform an Image to Gray scale
        /// </summary>
        public void TransformToGrayScale(Bitmap b)
        {
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < b.Width; i++)
                 for (int j = 0; j < b.Height; j++)
                 {
                    Color c1 = b.GetPixel(i, j);
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;

                    int gray = (byte)(.2126 * r1 + .7152 * g1 + .0722 * b1);
                    b.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                 }

             BitmapImage bitmap = Bmp2BmpImage(b);
             ProcessedImageVM = bitmap;
             watch.Stop();
             ProcessedTimeVM = watch.ElapsedMilliseconds;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
