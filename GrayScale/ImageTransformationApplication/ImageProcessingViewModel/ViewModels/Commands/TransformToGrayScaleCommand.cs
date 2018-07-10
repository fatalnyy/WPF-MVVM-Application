using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImageProcessingViewModel.ViewModels.Commands
{
    public class TransformToGrayScaleCommand : ICommand
    {
        public ImageProcessing ImageProcessing { get; set; }
        public TransformToGrayScaleCommand(ImageProcessing imageProcessing)
        {
            ImageProcessing = imageProcessing;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                ImageProcessing.TransformToGrayScale(new Bitmap(ImageProcessing.ImagePathVM));
            }
            catch
            {
               // throw new Exception("You have to load an image first!");
                MessageBox.Show("You have to load an image first!");
            }
        }
    }
}
