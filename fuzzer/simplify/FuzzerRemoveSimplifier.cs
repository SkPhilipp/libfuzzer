using Fuzzer.core;

namespace Fuzzer.simplify
{
    /// <summary>
    /// A IFuzzerSimplifier implementation which provides plans where a step is removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerRemoveSimplifier<T> : FuzzerCursorBasedSimplifier<T> where T : FuzzerContext
    {
        public FuzzerRemoveSimplifier(FuzzerPlan<T> plan) : base(plan)
        {
        }
        
        protected override FuzzerPlan<T> SimplifyCurrent()
        {
            var cursorPhase = PlanCursor.CursorPhase;
            var cursorStep = PlanCursor.CursorPhaseStep;
            var candidatePhase = Plan.Phases[cursorPhase];
            if (candidatePhase.Steps.Count <= candidatePhase.StepsMinimum)
            {
                return null;
            }

            var simplified = Plan.Copy();
            simplified.Phases[cursorPhase].Steps.RemoveAt(cursorStep);
            return simplified;
        }
    }
}