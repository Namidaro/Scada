using System;
using FluentAssertions;
using NUnit.Framework;
using ULA.Drivers.DataEngines.PopDataEngine.Cache;

namespace ULA.Drivers.Tests.DataEngine.PopDataEngine.Cache
{
    [TestFixture]
    class CacheTests
    {
        [Test]
        public void PostIfnformationToCacheStorage()
        {
            var cache = new CachedStorage();
            object inf = new Byte[1, 3, 4, 5, 6];
            cache.SetValueById("1", inf);
            object result;
            cache.TryGetValueById("1", out result);
            result.Should().Be(inf);
        }

        [Test]
        public void PostNullIfnformationToCacheStorage()
        {
            var cache = new CachedStorage();
            
            cache.SetValueById("1", null);
            object result;
            cache.TryGetValueById("1", out result);
            result.Should().BeNull();
        }

        [Test]
        public void GetfnformationFromEmptyCacheStorage()
        {
            var cache = new CachedStorage();

            
            object result;
            cache.TryGetValueById("1", out result);
            result.Should().BeNull();
        }

        [Test]
        public void PostInformationWithNullKeyToCacheStorage()
        {
            var cache = new CachedStorage();

            cache.SetValueById(null, null);
            object result;
            cache.TryGetValueById(null, out result);
            result.Should().BeNull();
        }

        [Test]
        public void PostInformationAndGetInformationWithDifferentKeys()
        {
            var cache = new CachedStorage();

            cache.SetValueById("1", null);
            object result;
            cache.TryGetValueById("2", out result);
            result.Should().BeNull();
        }
    
    }
}
