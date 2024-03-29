﻿using Fuzzer.core;
using NUnit.Framework;

namespace Fuzzer.Tests
{
    public class FuzzerTests
    {
        [Test]
        public void FuzzPositive()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase().Generate();
            var fuzzer = new Fuzzer<CalculatorContext>(() => new CalculatorContext());
            var calculatorContext = fuzzer.Run(fuzzerPlan);

            Assert.Greater(calculatorContext.Calculator.Interactions, 0);
        }

        [Test]
        public void FuzzErring()
        {
            var fuzzerPlan = CalculatorBlueprints.Erring().Generate();
            var fuzzer = new Fuzzer<CalculatorContext>(() => new CalculatorContext());
            try
            {
                fuzzer.Run(fuzzerPlan);
                Assert.Fail();
            }
            catch (FuzzerException<CalculatorContext> e)
            {
                Assert.NotNull(e.Context);
                Assert.Greater(e.Context.Calculator.Interactions, 0);
                Assert.AreEqual(fuzzerPlan, e.Plan);
                Assert.AreEqual(4, e.IndexPhase);
                Assert.AreEqual(0, e.IndexPhaseStep);
                Assert.AreNotEqual(0, e.IndexOperation);
            }
        }

        [Test]
        public void FuzzErringSimplifying()
        {
            var fuzzerPlan = CalculatorBlueprints.Erring(0).Generate();
            var fuzzer = new Fuzzer<CalculatorContext>(() => new CalculatorContext());
            try
            {
                fuzzer.RunSimplifying(fuzzerPlan);
                Assert.Fail();
            }
            catch (FuzzerException<CalculatorContext> e)
            {
                Assert.NotNull(e.Context);
                Assert.AreEqual(1, e.Context.Calculator.Interactions);
                Assert.AreEqual(1, e.IndexPhase);
                Assert.AreEqual(0, e.IndexPhaseStep);
                Assert.AreEqual(1, e.IndexOperation);
            }
        }
    }
}