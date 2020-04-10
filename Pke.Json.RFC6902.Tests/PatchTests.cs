using System;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pke.Json.RFC6902.Tests
{
    public class PatchTests
    {
        [Fact]
        public void Patch_ShouldContainNoOps_WhenJsonObjectsAreEqual()
        {
            var fromJson = JToken.FromObject(new TestObject());
            var toJson = JToken.FromObject(new TestObject());

            var patch = new Patch(fromJson, toJson);

            Assert.NotNull(patch);
            Assert.Empty(patch);
        }

        [Fact]
        public void Patch_ShouldContainAddOps_WhenToJsonObjectHasPropertiesFromJsonObjectDoesnt()
        {
            var fromJson = JToken.FromObject(new TestObject());
            var toJson = JToken.FromObject(new TestObjectWithAddedProperties());

            var patch = new Patch(fromJson, toJson);

            Assert.NotNull(patch);
            Assert.Contains(patch, x => x.Op == OperationType.Add);
        }

        [Fact]
        public void Patch_ShouldContainRemoveOps_WhenToJsonObjectDoesntHavePropertiesFromJsonObjectDoes()
        {
            var fromJson = JToken.FromObject(new TestObject());
            var toJson = JToken.FromObject(new TestObjectWithRemovedProperties());

            var patch = new Patch(fromJson, toJson);

            Assert.NotNull(patch);
            Assert.Contains(patch, x => x.Op == OperationType.Remove);
        }

        public class TestObject
        {
            public string Property1 { get; set; }

            public string Property2 { get; set; }
        }

        public class TestObjectWithAddedProperties : TestObject
        {
            public string Property3 { get; set; }

            public string Property4 { get; set; }
        }

        public class TestObjectWithRemovedProperties
        {
            public string Property1 { get; set; }
        }
    }
}
