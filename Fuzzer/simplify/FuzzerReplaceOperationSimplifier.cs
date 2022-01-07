using Fuzzer.core;

namespace Fuzzer.simplify
{
    /// <summary>
    /// A IFuzzerSimplifier implementation which provides plans where an operation is replaced by a simplified version.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerReplaceOperationSimplifier<T> : FuzzerCursorBasedSimplifier<T> where T : FuzzerContext
    {
        public FuzzerReplaceOperationSimplifier(FuzzerPlan<T> plan) : base(plan)
        {
        }

        protected override FuzzerPlan<T> SimplifyCurrent()
        {
            var cursorPhase = PlanCursor.CursorPhase;
            var cursorStep = PlanCursor.CursorPhaseStep;
            var candidatePhase = Plan.Phases[cursorPhase];
            var candidateStep = candidatePhase.Steps[cursorStep];
            if (candidateStep.SimplifyOperation == null)
            {
                return null;
            }

            var simplifiedStep = candidateStep.SimplifyOperation(candidateStep);
            if (candidateStep.Operation.Name == simplifiedStep.Operation.Name)
            {
                return null;
            }

            var simplified = Plan.Copy();
            simplified.Phases[cursorPhase].Steps[cursorStep] = simplifiedStep;
            return simplified;
        }
    }
}