using System;
using System.Drawing;

namespace DB.FreeFoosballInspector
{
    public class FreeFoosballInspector<TInspector> where TInspector: IFreeFoosballInspector
    {
        public event EventHandler TableStatusChangedEvent;
        private readonly IFreeFoosballInspector _foosballInspector;

        public FreeFoosballInspector()
        {
            _foosballInspector = Activator.CreateInstance<TInspector>();
        }

        public void Start()
        {
            var timer = new System.Timers.Timer(1000);

            timer.Elapsed += (sender, args) =>
            {
                if (_foosballInspector.HasChangedToFree())
                {
                    TableStatusChangedEvent?.Invoke(this, new TableStatusChangedEventArgs() {IsFree = true});
                }
                if (_foosballInspector.HasChangedToOccupied())
                {
                    TableStatusChangedEvent?.Invoke(this, new TableStatusChangedEventArgs() { IsFree = false });
                }
            };
            timer.Start();
        }
    }
}
