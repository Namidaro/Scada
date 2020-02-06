using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ModbusDeviceSimulatorLibrary;
using ModbusDeviceSimulatorLibrary.Strategy;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Cache;
using ULA.Drivers.DataEngines.PopDataEngine.Exceptions;
using ULA.Drivers.DataEngines.PopDataEngine.Metadata;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;

namespace ULA.Drivers.IntegrationTests.DataDriver
{
    [TestFixture]
    public class DataDriverTest
    {
        private ModbusDataDriver[] _drivers = new ModbusDataDriver[10];
        
        [SetUp]
        public void Initialization()
        {
            
            this._drivers[0] = new ModbusDataDriver("10.12.89.117", 4444);
            this._drivers[1] = new ModbusDataDriver("10.12.89.118", 4444);
            this._drivers[2] = new ModbusDataDriver("10.12.89.119", 4444);
            this._drivers[3] = new ModbusDataDriver("10.12.89.120", 4444);
            this._drivers[4] = new ModbusDataDriver("10.12.89.121", 4444);
            this._drivers[5] = new ModbusDataDriver("10.12.89.122", 4444);
            this._drivers[6] = new ModbusDataDriver("10.12.89.123", 4444);
            this._drivers[7] = new ModbusDataDriver("10.12.89.124", 4444);
            this._drivers[8] = new ModbusDataDriver("10.12.89.125", 4444);
            this._drivers[9] = new ModbusDataDriver("10.12.89.126", 4444);
            for (int i = 0; i < 10; i++)
            {
                this._drivers[i].Start();
            }
            
        }

        [Test]
        public void ConnectToSimulatorNotRaiseExceptionAndReadDataFromAddessResultShouldBe2014()
        {
            ushort address = 4096;
            for (int i = 0; i < this._drivers.Length; i++)
            {
                address++;
                Task<object> task =
                    this._drivers[i].ReadDataAsync(new EntityMetadata {NumberOfPoints = 2, StartAddress = address},
                        new CancellationToken());
                task.Start();
                Console.WriteLine(GetStringFromBytesArray(task.Result)+" id: "+ task.Id);
            }
        }

        [Test]
        public void Test()
        {
            ushort address = 4096;
            for (int i = 0; i < this._drivers.Length; i++)
            {
                address++;
                
                Task<object> task =
                    this._drivers[i].ReadDataAsync(new EntityMetadata { NumberOfPoints = 2, StartAddress = address },
                        new CancellationToken());
                task.Start();
                if(task.Id == 1 || task.Id == 4) {task.Wait(10000);Console.WriteLine("delay:10000");}
                
                Console.WriteLine(GetStringFromBytesArray(task.Result) + " id: " + task.Id);
            }
        }

        [Test]
        [ExpectedException(typeof(DataEngineEmptyMetadataException))]
        public void ReadInformationWithNullAddressShouldBeRaiseException()
        {
            Task<object> task = this._drivers[0].ReadDataAsync(null,
                new CancellationToken());
            task.Start();
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                throw ex.GetBaseException();
            }

        }

        [Test]
        public void WriteInformation()
        {
            Task task =
                this._drivers[0].WriteDataAsync(new EntityMetadata() {NumberOfPoints = 10, StartAddress = 0x0001}, 1,
                    new CancellationToken());
            task.Start();
        }

        //[Test]
        //[ExpectedException(typeof(InvalidDataException))]
        //public void WriteInformationToNotExistAddressRaiseInvalidDataException()
        //{

        //    this._driver.WriteDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 0x1006 }, new DateTimeValue(2010),
        //             new CancellationToken()).RunSynchronously();
        //    Assert.AreEqual(this._simulator.GetDataFromAddress(4444, 0x1006).ToString(), 2010);
        //}

        //[Test]
        //public void WriteAndReadDataToExistAddress()
        //{
        //    this._simulator.SetDataToAddress(4444,4200,new HexadecimalValue("abc"));
        //    Task<object> task = this._driver.ReadDataAsync(new EntityMetadata { NumberOfPoints = 3, StartAddress = 4200 },
        //       new CancellationToken());
        //    task.Start();
        //    Assert.AreEqual(new HexadecimalValue("abc").TransformDataToBytes(),task.Result);
        //}

        //[Test]
        //public void UpdateDataFromCachedDataAccessorResaltShouldBeNotNull()
        //{
            
        //    var cache = new CachedDataAccessor(new DriverDataAccessor(new ModbusDataDriver("127.0.0.1", 4444)));
        //    Assert.NotNull(Task.Factory.StartNew(
        //        () => cache.UpdateDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4096 },
        //            new CancellationToken())).Result);
        //}

        //[Test]
        //public void Write2010ToExistAddressAndThenReadShouldBe2010()
        //{
        //    Task t = this._driver.WriteDataAsync(new EntityMetadata {NumberOfPoints = 10, StartAddress = 4096},
        //        new DateTimeValue(2010).TransformDataToBytes(),
        //        new CancellationToken());
        //    t.Start();
        //    Task<object> taskGetResult = this._simulator.GetDataFromAddressAsync(4444, 4096);
        //    Thread.Sleep(1000);
        //    taskGetResult.Start();
        //    Assert.AreEqual(taskGetResult.Result.ToString(), new DateTimeValue(2010).ToString());
        //}

        //[Test]
        //[ExpectedException(typeof(AggregateException))]
        //public void ReadFromNotExistAddrssShouldBeLogicalErrorAndRaisedException()
        //{
        //    Task<object> task = this._driver.ReadDataAsync(new EntityMetadata { NumberOfPoints = 1, StartAddress = 0x0205 },
        //        new CancellationToken());
        //    task.Start();
        //    task.Wait();
        //}

        //[Test]
        //public void CreateNewAddressAndWriteNewHexadecimalValueThereAndReadIt()
        //{
        //    this._simulator.AddNewAddressWithData(4444,4201,new HexadecimalValue("32"));
        //    Task t = this._driver.WriteDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4201 },
        //        new HexadecimalValue("AB3AB3AB3AB3AB3AB3").TransformDataToBytes(),
        //        new CancellationToken());
        //    t.Start();
        //    Task<object> taskGetResult = this._simulator.GetDataFromAddressAsync(4444, 4201);
        //    Thread.Sleep(1000);
        //    taskGetResult.Start();
        //    byte[] tempBytes = new HexadecimalValue("AB3AB3AB3AB3AB3AB3").TransformDataToBytes();
        //    Assert.AreEqual(taskGetResult.Result.ToString(), Convert.ToString(new HexadecimalValue("AB3AB3AB3AB3AB3AB3").TransformDataFromBytes(tempBytes)));
        //}

        //[Test]
        //public void WriteBoolArrayWithTrueDataAndThenReadDataFromThisAddressShouldBeBoolArrayWithTrueData()
        //{
        //    var t = this._driver.WriteDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 3333 },
        //        new BinaryValue(new[]{true,true}).TransformDataToBytes(),
        //        new CancellationToken());
        //    t.Start();
        //    Task<object> taskGetResult = this._simulator.GetDataFromAddressAsync(4444, 3333);
        //    Thread.Sleep(1000);
        //    taskGetResult.Start();
        //    Assert.AreEqual(taskGetResult.Result.ToString(), "0000000100000001");
        //}

        //[Test]
        //public void ReadAsyncFromDifferentDeviceWithSameAddress()
        //{
        //    this._simulator.CreateDevice("Device2", 8001);
        //    this._simulator.CreateDevice("Device3",8080);
        //    this._simulator.RunDevice(8001);
        //    this._simulator.RunDevice(8080);
        //    this._simulator.AddNewAddressWithData("Device2",4096,new DateTimeValue(2010));
        //    this._simulator.AddNewAddressWithData("Device3",4096,new DateTimeValue(7777));
        //    var driver1 = new ModbusDataDriver("127.0.0.1", 8001);

        //    driver1.Start();
        //    var driver2 = new ModbusDataDriver("127.0.0.1", 8080);
        //    driver2.Start();

        //    Task<object> task1 = driver1.ReadDataAsync(new EntityMetadata {NumberOfPoints = 10, StartAddress = 4096},
        //        new CancellationToken());
        //    Task<object> task2 = driver2.ReadDataAsync(new EntityMetadata {NumberOfPoints = 10, StartAddress = 4096},
        //        new CancellationToken());
        //    Task.Factory.StartNew(() =>
        //    {
        //        task1.Start();
        //        task2.Start();
        //    });
        //    driver1.Stop();
        //    driver2.Stop();
        //    Console.WriteLine("task1.Result: {0}\ntask2.Result: {1}", GetStringFromBytesArray(task1.Result), GetStringFromBytesArray(task2.Result));
        //    Assert.AreNotEqual(task1.Result,task2.Result);
        //}

        //[Test]
        //public void ReadAsyncFromDifferentDeviceWithDifferentAddress()
        //{
        //    this._simulator.CreateDevice("Device2", 8001);
        //    this._simulator.CreateDevice("Device3", 8080);
        //    this._simulator.RunDevice(8001);
        //    this._simulator.RunDevice(8080);
        //    this._simulator.AddNewAddressWithData("Device2", 1200, new DateTimeValue(2010));
        //    this._simulator.AddNewAddressWithData("Device3", 4500, new DateTimeValue(7777));
        //    var driver1 = new ModbusDataDriver("127.0.0.1", 8001);

        //    driver1.Start();
        //    var driver2 = new ModbusDataDriver("127.0.0.1", 8080);
        //    driver2.Start();

        //    Task<object> task1 = driver1.ReadDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 1200 },
        //        new CancellationToken());
        //    Task<object> task2 = driver2.ReadDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4500 },
        //        new CancellationToken());
        //    Task.Factory.StartNew(() =>
        //    {
        //        task1.Start();
        //        task2.Start();
        //    });
        //    driver1.Stop();
        //    driver2.Stop();
        //    Console.WriteLine("task1.Result: {0}\ntask2.Result: {1}", GetStringFromBytesArray(task1.Result), GetStringFromBytesArray(task2.Result));
        //    Assert.AreNotEqual(task1.Result, task2.Result);
        //}

        //[Test]
        //public void ReadAsyncFromDifferentAddressFromOneDevice()
        //{
        //    Task<object> task1 = this._driver.ReadDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4096 },
        //        new CancellationToken());
        //    Task<object> task2 = this._driver.ReadDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4200 },
        //        new CancellationToken());
        //    Task.Factory.StartNew(() =>
        //    {
        //        task1.Start();
        //        task2.Start();
        //    });
        //    Console.WriteLine("task1.Result: {0}\ntask2.Result: {1}", GetStringFromBytesArray(task1.Result), GetStringFromBytesArray(task2.Result));
        //    Assert.AreNotEqual(task1.Result,task2.Result);
        //}

        //[Test]
        //public void ReadFromSameAdrressFromOneDevice()
        //{
        //    Task<object> task1 = this._driver.ReadDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4096 },
        //        new CancellationToken());
        //    Task<object> task2 = this._driver.ReadDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4096 },
        //        new CancellationToken());
        //    Task.Factory.StartNew(() =>
        //    {
        //        task1.Start();
        //        task2.Start();
        //    });
        //    Console.WriteLine("task1.Result: {0}\ntask2.Result: {1}", GetStringFromBytesArray(task1.Result), GetStringFromBytesArray(task2.Result));
        //    Assert.AreEqual(task1.Result, task2.Result);
        //}

        //[Test]
        //public void ReadThenWriteDataToAddressShouldBeReadResult()
        //{
        //    this._simulator.SetDataToAddress(4444,4096,new DateTimeValue(1312));
        //    Task<object> taskRead = this._driver.ReadDataAsync(new EntityMetadata { NumberOfPoints = 2, StartAddress = 4096 },
        //        new CancellationToken());
        //    Task taskWrite = this._driver.WriteDataAsync(new EntityMetadata { NumberOfPoints = 10, StartAddress = 4096 },
        //        new DateTimeValue(999).TransformDataToBytes(),
        //        new CancellationToken());
        //    taskRead.Start();
        //    taskRead.Wait();

        //    taskWrite.Start();
            
        //    taskWrite.Wait();
        //    Console.WriteLine("task1.Result: {0}", GetStringFromBytesArray(taskRead.Result));
        //    Assert.AreEqual(taskRead.Result,new DateTimeValue(1312).TransformDataToBytes());

        //}

        [TearDown]
        public void CloseConnectionWithSimulator()
        {
            foreach (ModbusDataDriver t in this._drivers)
                t.Stop();
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
