using System;
using Fuzzer.core;

namespace Fuzzer.simplify
{
    /// <summary>
    /// A IFuzzerSimplifier implementation which provides plans where a seed is replaced by a simplified version.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerReplaceSeedSimplifier<T> : FuzzerCursorBasedSimplifier<T>
    {
        public const double DeviationThreshold = 0.0000001;

        public static readonly Func<FuzzerStep<T>, FuzzerStep<T>> ZeroSeed = step => step.WithSeed(0.0);

        public FuzzerReplaceSeedSimplifier(FuzzerPlan<T> plan) : base(plan)
        {
        }

        protected override FuzzerPlan<T> SimplifyCurrent()
        {
            var cursorPhase = PlanCursor.CursorPhase;
            var cursorStep = PlanCursor.CursorPhaseStep;
            var candidatePhase = Plan.Phases[cursorPhase];
            var candidateStep = candidatePhase.Steps[cursorStep];
            if (candidateStep.SimplifySeed == null)
            {
                return null;
            }

            var simplifiedStep = candidateStep.SimplifySeed(candidateStep);
            if (Math.Abs(candidateStep.Seed - simplifiedStep.Seed) < DeviationThreshold)
            {
                return null;
            }

            var simplified = Plan.Copy();
            simplified.Phases[cursorPhase].Steps[cursorStep] = simplifiedStep;
            return simplified;
        }
    }
}