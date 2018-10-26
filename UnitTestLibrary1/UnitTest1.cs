using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestLibrary1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange - aseta odotettava tulos ja syötteet
            //int expected = 5; // odotettava tulos
            int input1 = 300;
            int input2 = 600;
            Vector2 paikka = new Vector2(input1, input2);

            //Act - lisää ensin reference varsinaiseen projektiin! Add reference - projects  Point1
            //Asenna Nugetillä Monogame-paketti MonoGame.Framework.Windows8.3.6.0.1625
            // Kutsu parametreillä testattavaa metodia
            Point1.




        }
    }
}
