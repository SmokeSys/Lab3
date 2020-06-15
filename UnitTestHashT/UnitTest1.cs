using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace lab3Hash
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InsertIsWorking()
        {
            HashTabl<int, int> t = new HashTabl<int, int>();
            Random r = new Random();
            
            for (int i = 0; i < 1000; i++)
            {
                t.Insert(r.Next(0, 10000), r.Next(0, 1000000));
                Assert.AreEqual(i + 1, t.Count);
            }
        }

        [TestMethod]
        public void DeleteIsWorking()
        {
            HashTabl<int, int> t = new HashTabl<int, int>();

            for (int i = 0; i < 1000; i++)
            {
                t.Insert(i + 1, i + 10000); // 1 - 1000, 10001-110000
            }
            int counter = 1000;
            for (int i = 1000; i > 0; i--)
            {                
                t.Delete(i, i + 10000);
                Assert.AreEqual(counter--, t.Count);
            }
        }

        [TestMethod]
        public void SearchNElseWorking()
        {
            HashTabl<int, int> t = new HashTabl<int, int>();

            for (int i = 0; i < 100; i++)
            {
                t.Insert(i + 1, i + 10000); // 1 - 100, 10001-10100
            }

            for (int i = 0; i < 100; i++)
            {
                int temp = t.Search(i + 1);
                Assert.AreEqual(i + 10000, temp);
                //t.Delete(i + 1, i + 10000);
                //temp = t.Search(i + 1);
                //Assert.AreEqual(0, temp);
            }
        }
    }
}
