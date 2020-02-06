using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Cache;
using ULA.Drivers.DataEngines.PopDataEngine.Metadata;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;

namespace ULA.Drivers.Tests.DataEngine.PopDataEngine.Cache
{
    [TestFixture]
    public class CachedDataAccessorTest
    {
        private IUpdatableDataAccessor _dataDriver;

        [Test]
        public void CashedStorageGetCommandShouldBeNotCanceled()
        {
            var cache = new CachedDataAccessor(this._dataDriver);
            
            Task.Factory.StartNew(
                () =>
                    cache.GetDataAsync(Mock.Of<EntityMetadata>(x => x.StartAddress == 0x1000),
                        new CancellationToken())).IsCanceled.Should().Be(false);

        }

        [Test]
        public void CashedStoragePostCommandAndThenReadInformationFromCache()
        {
            var cache = new CachedDataAccessor(this._dataDriver);
            cache.PostDataAsync(new EntityMetadata {StartAddress = 1000}, new Byte[] {1, 2, 3, 5},
                new CancellationToken()).RunSynchronously();
            cache.GetDataAsync(new EntityMetadata {StartAddress = 1000, NumberOfPoints = 10},
                new CancellationToken()).RunSynchronously();
        }

        [Test]
        public void CashedStoragePostCommandNotCompleted()
        {
            var cache = new CachedDataAccessor(this._dataDriver);
            var beforeRun = cache.PostDataAsync(new EntityMetadata {StartAddress = 1000}, new Byte[] {1, 2, 3, 5},
                new CancellationToken()).IsCompleted;
            cache.PostDataAsync(new EntityMetadata { StartAddress = 1000 }, new Byte[] { 1, 2, 3, 5 },
                new CancellationToken()).RunSynchronously();
            beforeRun.Should().Be(false);
        }

        [Test]
        public void CashedStorageGetCommandNotFailed()
        {
            var cache = new CachedDataAccessor(this._dataDriver);
            cache.PostDataAsync(null, null,
                new CancellationToken()).IsFaulted.Should().Be(false);
        }

    }
}
