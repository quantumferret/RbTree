using NUnit.Framework;

namespace RbTree {
    [TestFixture]
    public class TestInserts {
        private RbTree<int> tree;

        [SetUp]
        public void Setup() {
            tree = new RbTree<int>();
        }

        [Test]
        public void TestInsert() {
            Assert.Pass();
        }
    }
}