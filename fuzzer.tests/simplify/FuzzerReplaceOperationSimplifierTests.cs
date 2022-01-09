using System;
using Fuzzer.blueprint;
using Fuzzer.core;
using Fuzzer.simplify;
using NUnit.Framework;

namespace Fuzzer.Tests.simplify
{
    public class FuzzerReplaceOperationSimplifierTests
    {
        [Test]
        public void ReplaceOperationsAll()
        {
            var doNothingOperation = new FuzzerOperation<CalculatorContext>("DoNothing", (_, _) => { });
            var doNothingReplacer = new Func<FuzzerStep<CalculatorContext>, FuzzerStep<CalculatorContext>>(step => step.WithOperation(doNothingOperation));
            var fuzzerPlan = new FuzzerBlueprint<CalculatorContext>()
                .SimplifyingOperations(doNothingReplacer)
                .Phase(1, 10)
                .Step("Add", (context, seed) => context.Add(seed))
                .Phase(1, 10)
                .Step("Subtract", (context, seed) => context.Subtract(seed))
                .Generate();
            for (var i = 0; i < 100; i++)
            {
                var fuzzerSimplifier = new FuzzerReplaceOperationSimplifier<CalculatorContext>(fuzzerPlan);
                var fuzzerPlanSimplified = fuzzerSimplifier.Next();
                if (fuzzerPlanSimplified != null)
                {
                    fuzzerPlan = fuzzerPlanSimplified;
                }
            }

            Assert.AreEqual(2, fuzzerPlan.Phases.Count);
            foreach (var fuzzerPhase in fuzzerPlan.Phases)
            {
                Assert.GreaterOrEqual(fuzzerPhase.Steps.Count, 1);
                Assert.LessOrEqual(fuzzerPhase.Steps.Count, 10);
                foreach (var fuzzerStep in fuzzerPhase.Steps)
                {
                    Assert.AreEqual("DoNothing", fuzzerStep.Operation.Name);
                }
            }
        }
    }
}