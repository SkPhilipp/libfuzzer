using Fuzzer.simplify;
using NUnit.Framework;

namespace Fuzzer.Tests.simplify
{
    public class FuzzerRemovePhaseSimplifierTests
    {
        [Test]
        public void RemoveMultiplePhases()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase(0, 0);
            var fuzzerRemovePhaseSimplifier = new FuzzerRemovePhaseSimplifier<CalculatorContext>(fuzzerPlan);
            fuzzerPlan = fuzzerRemovePhaseSimplifier.Next();

            Assert.AreEqual(0, fuzzerPlan.Phases.Count);
        }

        [Test]
        public void RemoveNothing()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase();
            var fuzzerRemovePhaseSimplifier = new FuzzerRemovePhaseSimplifier<CalculatorContext>(fuzzerPlan);
            fuzzerPlan = fuzzerRemovePhaseSimplifier.Next();

            Assert.IsNull(fuzzerPlan);
        }
    }
}