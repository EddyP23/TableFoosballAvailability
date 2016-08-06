using System;

namespace DB.FreeFoosballInspector
{
    public interface IFreeFoosballInspectionManager
    {
        void StartInspection();

        IFreeFoosballInspectionManager Configure(Action<bool> inspectionAction);
    }
}
