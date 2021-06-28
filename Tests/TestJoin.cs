using NUnit.Framework;
using NUnit.Framework.Internal;
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
            t1.Add(0);
            t1.Add(-1);
            t1.Add(-2);

            t2 = new RbTree<int>();
            t2.Add(9);
            t2.Add(8);
            t2.Add(10);
            t2.Add(7);
            t2.Add(11);
        }

        [Test]
        public void TestEqualBh() {
            TestContext.Progress.WriteLine($"{t1.Bh}, {t2.Bh}");
            var t3 = Join(t1, 6, t2);
            t3.Print();
            Assert.Pass();
        }

        [Test]
        public void TestUnequalBh() {
            t1.Add(-3);
            t1.Add(-4);
            t1.Add(-5);
            TestContext.Progress.WriteLine($"{t1.Bh}, {t2.Bh}");
            System.Console.SetOut(TestContext.Progress);
            t1.Print();
            Assert.Pass();
        }
    }
}