using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Point1;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Vector2 test = new Vector2(100f, 200f);
            Vector2 result;
            int n = 2;
            Automove2 am = new Automove2(test, new Random());
            //for (int i = 0; i < 10; i++)
            {
                result = am.Wander(test, ref n);
                Console.WriteLine("Testing test.X  " + test.X);
                Console.WriteLine("Testing test.Y  " + test.Y);
                Console.WriteLine("result.X =  " + result.X);
                Console.WriteLine("result.Y =  " + result.Y);
                Assert.AreEqual(result, test);
            }
        }
    }
}
