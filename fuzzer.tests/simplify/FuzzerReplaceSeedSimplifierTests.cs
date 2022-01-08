using Fuzzer.simplify;
using NUnit.Framework;
using static Fuzzer.simplify.FuzzerReplaceSeedSimplifier<Fuzzer.Tests.CalculatorContext>;

namespace Fuzzer.Tests.simplify
{
    public class FuzzerReplaceSeedSimplifierTests
    {
        [Test]
        public void ReplaceSeedAll()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase();
            for (var i = 0; i < 100; i++)
            {
                var fuzzerSimplifier = new FuzzerReplaceSeedSimplifier<CalculatorContext>(fuzzerPlan);
                var fuzzerPlanSimplified = fuzzerSimplifier.Next();
                if (fuzzerPlanSimplified != null)
                {
                    fuzzerPlan = fuzzerPlanSimplified;
                }
            }

            Assert.AreEqual(3, fuzzerPlan.Phases.Count);
            foreach (var fuzzerPhase in fuzzerPlan.Phases)
            {
                Assert.GreaterOrEqual(fuzzerPhase.Steps.Count, 1);
                Assert.LessOrEqual(fuzzerPhase.Steps.Count, 10);
                foreach (var fuzzerStep in fuzzerPhase.Steps)
                {
                    Assert.LessOrEqual(fuzzerStep.Seed, DeviationThreshold);
                }
            }
        }
    }
}