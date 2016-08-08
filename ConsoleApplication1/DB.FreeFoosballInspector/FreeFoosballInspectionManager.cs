using System;
using System.Timers;

namespace DB.FreeFoosballInspector
{
    public class FreeFoosballInspectionManager<TInspector> : IFreeFoosballInspectionManager
        where TInspector : IFreeFoosballInspector
    {
        private readonly IFreeFoosballInspector _foosballInspector;
        private Action<bool> _inspectionAction;

        public FreeFoosballInspectionManager()
        {
            _foosballInspector = Activator.CreateInstance<TInspector>();
        }

        public IFreeFoosballInspectionManager Configure(Action<bool> inspectionAction)
        {
            _inspectionAction = inspectionAction;
            return this;
        }

        public void StartInspection()
        {
            var timer = new Timer(1000);

            timer.Elapsed += (sender, args) =>
            {
                if (_foosballInspector.HasChangedToFree())
                {
                    _inspectionAction?.Invoke(true);
                }
                else
                if (_foosballInspector.HasChangedToOccupied())
                {
                    _inspectionAction?.Invoke(false);
                }
            };
            timer.Start();
        }
    }
}
