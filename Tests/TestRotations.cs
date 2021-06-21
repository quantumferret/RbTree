using NUnit.Framework;

namespace RbTree
{
    [TestFixture]
    public class Tests {
        private RbTree<int> tree;
        [SetUp]
        public void Setup() {
            tree = new RbTree<int>();
        }

        [Test]
        public void TestLeftRotationOnRoot() {
            TestContext.Progress.WriteLine("Test left rotation centered on root:");
            tree.Root = new RbTree<int>.Node(5, tree.Nil) { Color = RbTree<int>.Node.ColorEnum.Black };
            tree.Root.Right = new RbTree<int>.Node(10, tree.Root) { Color = RbTree<int>.Node.ColorEnum.Red };
            
            Assert.AreEqual(5, tree.Root.Key);
            Assert.AreEqual(10, tree.Root.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.Leaf(), tree.Root.Left);
            TestContext.Progress.WriteLine("    Before rotation:");
            TestContext.Progress.WriteLine($"        Root: {tree.Root}, Right: {tree.Root.Right}, Left: {tree.Root.Left}");

            tree.LeftRotate(tree.Root);
            TestContext.Progress.WriteLine("    After rotation:");
            TestContext.Progress.WriteLine($"        Root: {tree.Root}, Right: {tree.Root.Right}, Left: {tree.Root.Left}");
            Assert.AreEqual(10, tree.Root.Key);
            Assert.AreEqual(5, tree.Root.Left.Key);
        }

        [Test]
        public void TestRightRotationOnRoot() {
            TestContext.Progress.WriteLine("Test right rotation centered on root:");

            tree.Root = new RbTree<int>.Node(10, tree.Nil) { Color = RbTree<int>.Node.ColorEnum.Black };
            tree.Root.Left = new RbTree<int>.Node(5, tree.Root) { Color = RbTree<int>.Node.ColorEnum.Red };
            
            Assert.AreEqual(10, tree.Root.Key);
            Assert.AreEqual(5, tree.Root.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.Leaf(), tree.Root.Right);
            TestContext.Progress.WriteLine("    Before rotation:");
            TestContext.Progress.WriteLine($"        Root: {tree.Root}, Right: {tree.Root.Right}, Left: {tree.Root.Left}");

            tree.RightRotate(tree.Root);
            TestContext.Progress.WriteLine("    After rotation:");

            TestContext.Progress.WriteLine($"        Root: {tree.Root}, Right: {tree.Root.Right}, Left: {tree.Root.Left}");
            Assert.AreEqual(5, tree.Root.Key);
            Assert.AreEqual(10, tree.Root.Right.Key);
        }
    }
}