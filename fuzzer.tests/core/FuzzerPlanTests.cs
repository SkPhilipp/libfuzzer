using NUnit.Framework;

namespace Fuzzer.Tests.core
{
    public class FuzzerPlanTests
    {
        [Test]
        public void Message()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase();
            var copy = fuzzerPlan.Copy();

            Assert.AreEqual(fuzzerPlan.Phases.Count, copy.Phases.Count);
            for (var i = 0; i < fuzzerPlan.Phases.Count; i++)
            {
                var fuzzerPhase = fuzzerPlan.Phases[i];
                var copyPhase = copy.Phases[i];
                Assert.AreEqual(fuzzerPhase.StepsMinimum, copyPhase.StepsMinimum);
                Assert.AreEqual(fuzzerPhase.StepsMaximum, copyPhase.StepsMaximum);
                Assert.AreEqual(fuzzerPhase.Steps.Count, copyPhase.Steps.Count);
                for (var k = 0; k < fuzzerPhase.Steps.Count; k++)
                {
                    var fuzzerStep = fuzzerPhase.Steps[k];
                    var copyStep = copyPhase.Steps[k];
                    Assert.AreEqual(fuzzerStep.Operation.Name, copyStep.Operation.Name);
                    Assert.AreEqual(fuzzerStep.Seed, copyStep.Seed);
                    Assert.AreEqual(fuzzerStep.SimplifyOperation, copyStep.SimplifyOperation);
                    Assert.AreEqual(fuzzerStep.SimplifySeed, copyStep.SimplifySeed);
                }
            }
        }
    }
}