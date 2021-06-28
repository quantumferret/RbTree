using NUnit.Framework;
using RbTree;

namespace Tests {
    [TestFixture]
    public class TestSubtree {
        [Test]
        public void Test() {
            var t1 = new RbTree<int>();
            t1.Add(3);
            t1.Add(2);
            t1.Add(4);
            t1.Add(1);
            t1.Add(5);
            t1.Add(0);
            t1.Add(-1);
            t1.Add(-2);
            t1.Add(-3);
            t1.Add(-4);
            t1.Add(-5);
            
            var t2 = t1.Subtree(t1.Root.Left.Left);

            t1.Remove(-5);

            Assert.AreEqual(true, t2.Contains(-5));
            Assert.AreEqual(true, t2.Validate());
        }
    }
}