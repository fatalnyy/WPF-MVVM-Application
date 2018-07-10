using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageProcessingViewModel.ViewModels.Commands
{
    public class OpenFileDialogCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ImageProcessing ImageProcessing { get; set; }

        public OpenFileDialogCommand(ImageProcessing imageProcessing)
        {
            ImageProcessing = imageProcessing;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ImageProcessing.LoadImage();
        }
    }
}
