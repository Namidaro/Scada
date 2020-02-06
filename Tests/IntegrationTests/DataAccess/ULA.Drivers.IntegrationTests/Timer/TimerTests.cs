using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Metadata;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;
using ULA.Drivers.DataEngines.PopDataEngine.Stream;

namespace ULA.Drivers.IntegrationTests.Timer
{
    [TestFixture]
    public class TimerTests
    {
        //[Test]
        //public void WorkTimer()
        //{
        //    List<EntityMetadata> metadatas = new List<EntityMetadata>();
        //    metadatas.Add(new EntityMetadata{NumberOfPoints = 2, StartAddress = 0x1000});
        //    metadatas.Add(new EntityMetadata{NumberOfPoints = 2, StartAddress = 0x1001});
        //    metadatas.Add(new EntityMetadata{NumberOfPoints = 2, StartAddress = 0x1002});
        //    metadatas.Add(new EntityMetadata{NumberOfPoints = 2, StartAddress = 0x1003});
        //    metadatas.Add(new EntityMetadata{NumberOfPoints = 2, StartAddress = 0x1004});
        //    metadatas.Add(new EntityMetadata{NumberOfPoints = 2, StartAddress = 0x1005});
        //    metadatas.Add(new EntityMetadata{NumberOfPoints = 2, StartAddress = 0x1006});
        //    var drivarDataAccessor = new DriverDataAccessor(new ModbusDataDriver("10.12.89.122", 4444));
        //    drivarDataAccessor.Start();
        //    var timer = new DefaultDataEntityStreamProducer(60000, drivarDataAccessor, metadatas);
        //    timer.Start();
        //    Thread.Sleep(70000);
        //    timer.Stop();
        //}
    }
}
