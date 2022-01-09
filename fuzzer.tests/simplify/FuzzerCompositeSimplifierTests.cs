using Fuzzer.simplify;
using NUnit.Framework;

namespace Fuzzer.Tests.simplify
{
    public class FuzzerCompositeSimplifierTest
    {
        [Test]
        public void CompositeAll()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase(0).Generate();
            for (var i = 0; i < 100; i++)
            {
                var fuzzerSimplifier = FuzzerCompositeSimplifier<CalculatorContext>.BuiltIn(fuzzerPlan);
                var fuzzerPlanSimplified = fuzzerSimplifier.Next();
                if (fuzzerPlanSimplified != null)
                {
                    fuzzerPlan = fuzzerPlanSimplified;
                }
            }

            Assert.AreEqual(0, fuzzerPlan.Phases.Count);
        }
    }
}