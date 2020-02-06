using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using ULA.Drivers.DataEngines.PopDataEngine.Command;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;

namespace ULA.Drivers.Tests.DataEngine.PopDataEngine.Command
{
    [TestFixture]
    public class DefaultDataProcessorCommandFactoryTest
    {
        private IUpdatableDataAccessor _dataDriver;
        [Test]
        public void CreateGetCommandCanceled()
        {
            var factory = new DefaultDataProcessorCommandFactory(this._dataDriver);
            var get = factory.CreateGetCommand(null, null, new CancellationToken(true));
            Task.Factory.StartNew(() => get.ExecuteAsync()).Result.IsCanceled.Should().Be(true);
        }

        [Test]
        public void CreatePostCommandCanceled()
        {
            var factory = new DefaultDataProcessorCommandFactory(this._dataDriver);
            var post = factory.CreatePostCommand(null,null, null, new CancellationToken(true));
            Task.Factory.StartNew(() => post.ExecuteAsync()).Result.IsCanceled.Should().Be(true);
        }

        [Test]
        public void CreateUpdateCommandCanceled()
        {
            var factory = new DefaultDataProcessorCommandFactory(this._dataDriver);
            var post = factory.CreateUpdateCommand(null, null, new CancellationToken(true));
            Task.Factory.StartNew(() => post.ExecuteAsync()).Result.IsCanceled.Should().Be(true);
        }


    }
}
