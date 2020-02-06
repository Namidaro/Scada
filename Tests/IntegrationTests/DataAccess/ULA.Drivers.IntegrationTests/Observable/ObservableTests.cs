using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusDeviceSimulatorLibrary.Strategy;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Cache;
using ULA.Drivers.DataEngines.PopDataEngine.Metadata;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;

namespace ULA.Drivers.IntegrationTests.Observable
{
    [TestFixture]
    public class ObservableTests
    {
        //private ModbusDeviceSimulator _simulator;
        private ModbusDataDriver _driver;
        private CachedDataAccessor _cachedDataAccessor;

        [SetUp]
        public void Initialization()
        {
            //this._simulator = new ModbusDeviceSimulator();
            //this._simulator.CreateDevice("Device1", 4444);
            //this._simulator.RunDevice("Device1");
            //this._simulator.AddNewAddressWithData(4444, 4096, new DateTimeValue(2014));
            //this._simulator.AddNewAddressWithData(4444, 4200, new HexadecimalValue("123"));
            //this._simulator.AddNewAddressWithData(4444, 3333, new BinaryValue(new bool[5]));
            this._driver = new ModbusDataDriver("10.12.89.118", 4444);
            this._driver.Start();
            this._cachedDataAccessor = new CachedDataAccessor(new DriverDataAccessor(this._driver));
        }

        [Test]
        public void Test1()
        {
            var obs = new DataEngines.PopDataEngine.Processor.Observable(this._cachedDataAccessor,this._driver);
            var t1 = this._driver.WriteDataAsync(new EntityMetadata() {NumberOfPoints = 10, StartAddress = 0x00000},new DateTimeValue(10).TransformDataToBytes(),
                new CancellationToken());
            Task.Factory.StartNew(t1.Start);
            t1.Wait();
            Thread.Sleep(3000);
            var t2 = this._driver.ReadDataAsync(new EntityMetadata() { NumberOfPoints = 10, StartAddress = 0x00000 },
                new CancellationToken());
            t2.Start();
            Thread.Sleep(3000);
            t2.Wait();
            Console.WriteLine(GetStringFromBytesArray(t2.Result));
        }

        [TearDown]
        public void CloseConnectionWithSimulator()
        {
            this._driver.Stop();
        }

        private string GetStringFromBytesArray(object bytes)
        {
            if (bytes is byte[])
            {
                return BitConverter.ToString((byte[])bytes);
            }
            return "";
        }
    }
}
