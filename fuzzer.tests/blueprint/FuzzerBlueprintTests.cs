using NUnit.Framework;

namespace Fuzzer.Tests.blueprint
{
    public class FuzzerBlueprintTests
    {
        [Test]
        public void GenerateMultiPhase()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase();

            Assert.AreEqual(3, fuzzerPlan.Phases.Count);
            foreach (var fuzzerPhase in fuzzerPlan.Phases)
            {
                Assert.GreaterOrEqual(fuzzerPhase.Steps.Count, 1);
                Assert.LessOrEqual(fuzzerPhase.Steps.Count, 10);
                foreach (var fuzzerStep in fuzzerPhase.Steps)
                {
                    Assert.LessOrEqual(fuzzerStep.Seed, 1);
                    Assert.GreaterOrEqual(fuzzerStep.Seed, 0);
                }
            }
        }
    }
}