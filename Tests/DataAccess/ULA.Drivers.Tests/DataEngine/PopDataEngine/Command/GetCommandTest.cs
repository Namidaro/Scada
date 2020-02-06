using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Command;
using ULA.Drivers.DataEngines.PopDataEngine.Metadata;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;

namespace ULA.Drivers.Tests.DataEngine.PopDataEngine.Command
{
    [TestFixture]
    public class GetCommandTest
    {
        private IUpdatableDataAccessor _dataDriver;
        [Test]
        public void CancelGetCommandTaskShouldBeCanceled()
        {
            var get = new GetCommand(Mock.Of<EntityMetadata>(x=>x.StartAddress == 0x1000),
                new DriverDataAccessor(new ModbusDataDriver("host", 4444)),new TaskCompletionSource<object>(),new CancellationToken(true) );
            Task.Factory.StartNew(() => get.ExecuteAsync()).Result.IsCanceled.Should().Be(true);
        }

        [Test]
        public void DefaultGetCommandTaskShouldBeNotCanceled()
        {
            var get = new GetCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),
                new DriverDataAccessor(new ModbusDataDriver("host", 4444)), new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => get.ExecuteAsync()).Result.IsCanceled.Should().Be(false);
        }

        [Test]
        public void DefaultGetCommandNotBeFaulted()
        {
            var get = new GetCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),
                new DriverDataAccessor(new ModbusDataDriver("host", 4444)), new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => get.ExecuteAsync()).Result.IsFaulted.Should().Be(false);
        }

        [Test]
        public void DefaultGetCommandResultNotBeNull()
        {
            var get = new GetCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),
                new DriverDataAccessor(new ModbusDataDriver("host", 4444)), new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => get.ExecuteAsync()).Result.Should().NotBeNull();
        }

        [Test]
        [ExpectedException(typeof(AggregateException))]
        public void GetCommandGetNullDataDriver()
        {
            var get = new UpdateCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),
                this._dataDriver, new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => get.ExecuteAsync()).Result.Should().NotBeNull();
        }
    }
}
