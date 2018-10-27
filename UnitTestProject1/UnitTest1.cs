using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange - aseta odotettava tulos ja syötteet            
            //Vector2 expected = odotettava tulos
            int input1 = 300; // alkukoordinaatit
            int input2 = 600;
            int n = 1;
            Vector2 expected = new Vector2(input1, input2);
            Point1.Automove2 am = new Point1.Automove2(new Vector2(0f, 0f), new Random()); //luodaan ko olio
            
            //Act - lisää ensin reference varsinaiseen projektiin! Add reference - projects  Point1
            //Asenna Nugetillä Monogame-paketti jotta saadaan Microsoft.Xna.Framework käyttöön
            // Kutsu parametreillä testattavaa metodia
            Vector2 actual = am.Wander(expected, ref n);

            //Assert - tarkistetaan
            Console.WriteLine("Aletaan testata");
            TestContext.WriteLine("Onnistuuko..?");
            //Assert.AreEqual(expected, actual); //varmistetaan yhtäsuuruus - tai erisuuruus
            Assert.AreNotEqual(expected, actual);
            //Assert.AreSame()
            //Assert.IsNotNull()
            //Assert.IsTrue()
            //Assert.IsFalse()
            //Assert.IsNotInstanceOfType()
            //Assert. jne
        }
    }
}
