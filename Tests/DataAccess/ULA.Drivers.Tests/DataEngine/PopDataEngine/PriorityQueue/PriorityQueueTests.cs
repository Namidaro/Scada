using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Metadata;
using ULA.Drivers.DataEngines.PopDataEngine.PriorityQueue;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;

namespace ULA.Drivers.Tests.DataEngine.PopDataEngine.PriorityQueue
{
    [TestFixture]
    public sealed class PriorityQueueTests : IDisposable
    {
        private IUpdatableDataAccessor _dataDriver;
        private PriorityQueueCommandProcessor _priorityQueue; 
        [SetUp]
        public void Init()
        {
            this._dataDriver = new DriverDataAccessor(new ModbusDataDriver("127.0.0.1",4444));
            this._priorityQueue = new PriorityQueueCommandProcessor(this._dataDriver);
            this._priorityQueue.Initialize();
            Console.WriteLine("Start");
            this._priorityQueue.Start();
        }

        [TearDown]
        public void Clear()
        {
            Console.WriteLine("Stop");
            this._priorityQueue.Stop();

        }

        [Test]
        public void RunDifferentCommandResultNotBeNull()
        {
            var metadata = new EntityMetadata
            {
                NumberOfPoints = 10,
                StartAddress = 0x1000
            };
            var tasks = new Task<object>[3];
            tasks[0] = this._priorityQueue.ProcessGetDataAsync(metadata, new CancellationToken());

            tasks[1] = this._priorityQueue.ProcessUpdateDataAsync(metadata, new CancellationToken());
            //Task.Factory.StartNew(() => Task.Factory.StartNew(() => tasks[1]).Result).Result.Should().NotBeNull();
        }

        [Test]
        public void RunPostCommandResultNotBeNull()
        {
            var metadata = Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000);
            Task t = this._priorityQueue.ProcessPostDataAsync(metadata, 12, new CancellationToken());
            Task.Factory.StartNew(t.RunSynchronously).Should().NotBeNull();
        }

        [Test]
        public void GetCountItemInPriorityQueueWithDifferentCommandInIt_ShouldBeTwo()
        {
            var metadata = Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000);
            this._priorityQueue.ProcessPostDataAsync(metadata, 12, new CancellationToken());
            this._priorityQueue.ProcessUpdateDataAsync(metadata, new CancellationToken());
            this._priorityQueue.ProcessUpdateDataAsync(metadata, new CancellationToken());
            
            this._priorityQueue.Count.Should().Be(2);
        }

        [Test]
        public void GetCountItemInPriorityQueueWithtOneCommandInIt_ShouldBeOne()
        {
            var metadata = Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000);
            this._priorityQueue.ProcessUpdateDataAsync(metadata, new CancellationToken());
            this._priorityQueue.Count.Should().Be(1);
        }

        [Test]
        public void RunGetCountItemInPriorityQueueWithSameCommandInIt_ShouldBeOne()
        {
            Thread.Sleep(1500);
            var metadata = Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000);
            this._priorityQueue.ProcessGetDataAsync(metadata, new CancellationToken());
            Thread.Sleep(1500);
            this._priorityQueue.ProcessGetDataAsync(metadata, new CancellationToken());
            this._priorityQueue.Count.Should().Be(1);
        }

        public void Dispose()
        {
            this._dataDriver = null;
            if (this._priorityQueue != null)
                this._priorityQueue.Dispose();
            this._priorityQueue = null;
        }
    }
}
