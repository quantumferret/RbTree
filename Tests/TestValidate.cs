using NUnit.Framework;
using RbTree;
using static RbTree.RbTree<int>;

namespace Tests {
    [TestFixture]
    public class TestValidate {
        private RbTree<int> tree;

        [SetUp]
        public void Setup() {
            tree = new RbTree<int>();
            tree.Add(3);
            tree.Add(2);
            tree.Add(4);
            tree.Add(1);
            tree.Add(5);
            tree.Add(0);
            tree.Add(-1);
            tree.Add(-2);
            tree.Add(-3);
            tree.Add(-4);
            tree.Add(-5);
        }

        [Test]
        public void TestValidTree() {
            TestContext.Progress.WriteLine("Testing Validate on a valid tree");
            Assert.AreEqual(true, tree.Validate());
        }
        
        [Test]
        public void TestRedConsistency() {
            Node max = tree.Maximum(tree.Root);
            max.Right = new Node(max.Key + 1) { Color = Node.ColorEnum.Red };
            max.Right.Right = new Node(max.Right.Key + 1) { Color = Node.ColorEnum.Red };
            TestContext.Progress.WriteLine("Testing RedConsistency after manually violating property 4");
            Assert.AreEqual(false, tree.RedConsistency(tree.Root));
        }

        [Test]
        public void TestBlackConsistency() {
            TestContext.Progress.WriteLine("Testing BlackConsistency with invalid tree");
            Node max = tree.Maximum(tree.Root);
            max.Right = new Node(max.Key + 1) { Color = Node.ColorEnum.Black };
            max.Right.Parent = max;
            Assert.AreEqual(false, tree.BlackConsistency());

            max.Right.Parent = null;
            max.Right = tree.Nil;

            max.Color = max.Color == Node.ColorEnum.Black ? Node.ColorEnum.Red : Node.ColorEnum.Black;
            Assert.AreEqual(false, tree.BlackConsistency());
        }
    }
}