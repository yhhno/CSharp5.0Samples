using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using Delegate;

namespace Delegate_Test
{
    [TestClass]
    public class Delegate_Test

    {
        [TestMethod]
        public void Test_Delegate_AddCalculator()
        {
            int num1 = 100;
            int num2 = 200;

            Delegate.Delegate.GetCalculateValueDelegate d = Delegate.Delegate.AddCalculator;

            Assert.AreEqual(d(num1, num2), 300);
        }
    }
}
