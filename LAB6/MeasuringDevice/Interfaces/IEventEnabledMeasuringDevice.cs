using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice.Interfaces
{
    public interface IEventEnabledMeasuringDevice:IMeasuringDevice
    {
        event EventHandler NewMeasurementTaken;
        //event HeartBeatEventHandler HeartBeat;
        //int HeartBeatInterval { get; }


    }
}
