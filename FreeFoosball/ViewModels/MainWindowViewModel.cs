using System;
using System.Drawing;
using System.Windows.Input;
using FreeFoosball.Assets;
using FreeFoosball.Commands;

namespace FreeFoosball.ViewModels
{
    public class MainWindowViewModel
    {
        public string Title { get; set; }

        public Image BackgroundImage => Resources.foosball;

        public ICommand CloseCommand => new CloseCommand(CloseAction);

        public Action CloseAction { get; set; }
    }
}
