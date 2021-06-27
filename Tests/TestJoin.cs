using NUnit.Framework;
using RbTree;
using static RbTree.RbTree<int>;

namespace Tests {
    [TestFixture]
    public class TestJoin {
        private RbTree<int> t1, t2;

        [SetUp]
        public void Setup() {
            t1 = new RbTree<int>();
            t1.Add(3);
            t1.Add(2);
            t1.Add(4);
            t1.Add(1);
            t1.Add(5);

            t2 = new RbTree<int>();
            t2.Add(9);
            t2.Add(8);
            t2.Add(10);
            t2.Add(7);
            t2.Add(11);
        }

        [Test]
        public void Test() {
            var t3 = Join(t1, 6, t2);
            t3.Print();
        }
    }
}