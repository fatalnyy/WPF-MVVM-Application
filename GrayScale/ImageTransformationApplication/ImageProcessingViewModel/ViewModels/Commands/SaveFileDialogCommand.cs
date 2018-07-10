using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImageProcessingViewModel.ViewModels.Commands
{
    public class SaveFileDialogCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ImageProcessing ImageProcessing { get; set; }

        public SaveFileDialogCommand(ImageProcessing imageProcessing)
        {
            ImageProcessing = imageProcessing;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                ImageProcessing.SaveImage();
            }
            catch
            {
                // throw new Exception("You have to load and transform image first!");
                MessageBox.Show("You have to load and transform an image first!");
            }

        }
    }
}
