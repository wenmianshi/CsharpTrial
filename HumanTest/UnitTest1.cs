using Hogwarts;
namespace HumanTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Human human = new Human("Miachen", "Shi", 26);
            human.CelebrateBirtday();
            Assert.AreEqual(27, human.Age, 0, "The human was not the right age");
            Assert.IsNotNull(human, "No human was created");
        }
    }
}