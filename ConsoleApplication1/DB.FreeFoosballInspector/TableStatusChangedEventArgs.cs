using System;

namespace DB.FreeFoosballInspector
{
    public class TableStatusChangedEventArgs : EventArgs
    {
        public bool IsFree { get; set; }
    }
}