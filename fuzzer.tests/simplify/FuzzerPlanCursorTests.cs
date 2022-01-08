using Fuzzer.simplify;
using NUnit.Framework;

namespace Fuzzer.Tests.simplify
{
    public class FuzzerPlanCursorTests
    {
        private static FuzzerPlanCursor<CalculatorContext> FuzzerPlanCursor()
        {
            var fuzzerPlan = CalculatorBlueprints.MultiPhase(2, 2);
            var fuzzerPlanCursor = new FuzzerPlanCursor<CalculatorContext>(fuzzerPlan);
            return fuzzerPlanCursor;
        }

        [Test]
        public void InitialValue()
        {
            var fuzzerPlanCursor = FuzzerPlanCursor();
            Assert.IsTrue(fuzzerPlanCursor.IsValid());
            Assert.AreEqual(2, fuzzerPlanCursor.CursorPhase);
            Assert.AreEqual(1, fuzzerPlanCursor.CursorPhaseStep);
        }

        [Test]
        public void IterateToSecondPhaseFirstStep()
        {
            var fuzzerPlanCursor = FuzzerPlanCursor();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            Assert.IsTrue(fuzzerPlanCursor.IsValid());
            Assert.AreEqual(1, fuzzerPlanCursor.CursorPhase);
            Assert.AreEqual(0, fuzzerPlanCursor.CursorPhaseStep);
        }

        [Test]
        public void IterateToFirstPhaseFirstStep()
        {
            var fuzzerPlanCursor = FuzzerPlanCursor();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            Assert.IsTrue(fuzzerPlanCursor.IsValid());
            Assert.AreEqual(0, fuzzerPlanCursor.CursorPhase);
            Assert.AreEqual(0, fuzzerPlanCursor.CursorPhaseStep);
        }

        [Test]
        public void IterateToInvalid()
        {
            var fuzzerPlanCursor = FuzzerPlanCursor();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            Assert.IsFalse(fuzzerPlanCursor.IsValid());
        }

        [Test]
        public void ResetAfterIterate()
        {
            var fuzzerPlanCursor = FuzzerPlanCursor();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Next();
            fuzzerPlanCursor.Reset();
            Assert.IsTrue(fuzzerPlanCursor.IsValid());
            Assert.AreEqual(2, fuzzerPlanCursor.CursorPhase);
            Assert.AreEqual(1, fuzzerPlanCursor.CursorPhaseStep);
        }
    }
}