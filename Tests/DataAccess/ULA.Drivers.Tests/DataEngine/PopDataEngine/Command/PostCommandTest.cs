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
    class PostCommandTest
    {
        private IUpdatableDataAccessor _dataDriver;
        [Test]
        public void CancelPostCommandTaskShouldBeCanceled()
        {
            var post = new PostCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),new Object(),
                this._dataDriver, new TaskCompletionSource<object>(), new CancellationToken(true));
            Task.Factory.StartNew(() => post.ExecuteAsync()).Result.IsCanceled.Should().Be(true);
        }

        [Test]
        public void DefaultPostCommandTaskShouldBeNotCanceled()
        {
            var post = new PostCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000), new object(),
                this._dataDriver, new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => post.ExecuteAsync()).IsCanceled.Should().Be(false);
        }

        [Test]
        public void DefaultPostCommandNotBeFaulted()
        {
            var post = new PostCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),new Object(),
                this._dataDriver, new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => post.ExecuteAsync()).IsFaulted.Should().Be(false);
        }

        [Test]
        public void DefaultPostCommandResultNotBeNull()
        {
            var post = new PostCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),new Object(),
               this._dataDriver, new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => post.ExecuteAsync()).Should().NotBeNull();
        }

        [Test]
        [ExpectedException(typeof(AggregateException))]
        public void PostCommandWithNullValue()
        {
            var post = new PostCommand(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000), null,
                this._dataDriver, new TaskCompletionSource<object>(), new CancellationToken());
            Task.Factory.StartNew(() => post.ExecuteAsync()).Result.Should().NotBeNull();
        }
    }
}
