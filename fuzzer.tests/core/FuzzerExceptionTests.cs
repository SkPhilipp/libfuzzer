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
            var fuzzerPlan = CalculatorBlueprints.MultiPhase(3).Generate();
            var calculatorContext = new CalculatorContext();
            var fuzzerExceptionCause = new Exception();
            var fuzzerExceptionIndex = fuzzerPlan.Phases[0].Steps.Count + 1;
            var fuzzerException = new FuzzerException<CalculatorContext>(fuzzerPlan, calculatorContext, fuzzerExceptionCause, 2, 1, fuzzerExceptionIndex);

            var message = fuzzerException.Message;
            Console.WriteLine(message);
            Assert.NotNull(message);
            StringAssert.Contains("Add", message);
            StringAssert.Contains("//", message);
            StringAssert.Contains("Subtract", message);
        }
    }
}