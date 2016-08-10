using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FreeFoosball.Commands
{
    class OpenCameraCommand: ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            System.Diagnostics.Process.Start("http://10.32.244.12/cgi-bin/guestimage.html");
        }

        public event EventHandler CanExecuteChanged;
    }
}
