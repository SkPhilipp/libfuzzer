using System;
using Fuzzer.core;
using NUnit.Framework;

namespace Fuzzer.Tests.core
{
    public class FuzzerExceptionTest
    {
        [Test]
        public void Message()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase(3);
            var calculatorContext = new CalculatorContext();
            var fuzzerExceptionCause = new Exception();
            var fuzzerException =
                new FuzzerException<CalculatorContext>(fuzzerPlan, calculatorContext, fuzzerExceptionCause, 1, 3, 3);

            var message = fuzzerException.Message;
            Console.WriteLine(message);
            Assert.NotNull(message);
            Assert.IsTrue(message.Contains("Add"));
            Assert.IsTrue(message.Contains("Subtract"));
            Assert.IsTrue(message.Contains("Multiply"));
        }
    }
}