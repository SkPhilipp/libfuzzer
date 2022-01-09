using Fuzzer.simplify;
using NUnit.Framework;

namespace Fuzzer.Tests.simplify
{
    public class FuzzerRemoveSimplifierTests
    {
        [Test]
        public void RemoveAll()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase(0).Generate();
            for (var i = 0; i < 100; i++)
            {
                var fuzzerSimplifier = new FuzzerRemoveSimplifier<CalculatorContext>(fuzzerPlan);
                var fuzzerPlanSimplified = fuzzerSimplifier.Next();
                if (fuzzerPlanSimplified != null)
                {
                    fuzzerPlan = fuzzerPlanSimplified;
                }
            }

            Assert.AreEqual(3, fuzzerPlan.Phases.Count);
            foreach (var fuzzerPhase in fuzzerPlan.Phases)
            {
                Assert.AreEqual(0, fuzzerPhase.Steps.Count);
            }
        }
    }
}