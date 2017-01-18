using Microsoft.VisualStudio.TestTools.UnitTesting;
using Goldbach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldbach.Tests
{
    [TestClass()]
    public class GoldbachTests
    {
        [TestMethod()]
        public void GoldbachCompositionTest()
        {
            var answer = Goldbach.Composition(100);
            Assert.AreEqual(new Tuple<int, int>(3, 97), answer);
            answer = Goldbach.Composition(8);
            Assert.AreEqual(new Tuple<int, int>(3, 5), answer);
            answer = Goldbach.Composition(10);
            Assert.AreEqual(new Tuple<int, int>(3, 7), answer);
            answer = Goldbach.Composition(6);
            Assert.AreEqual(new Tuple<int, int>(3, 3), answer);
            answer = Goldbach.Composition(4);
            Assert.AreEqual(new Tuple<int, int>(2, 2), answer);
        }

        [TestMethod()]
        public void IsPrimeTest()
        {
            Assert.IsTrue(3.IsPrime());
            Assert.IsFalse(25.IsPrime());
        }

        [TestMethod()]
        public void IsNotPrimeTest()
        {
            Assert.IsFalse(3.IsNotPrime());
        }

       
    }
}