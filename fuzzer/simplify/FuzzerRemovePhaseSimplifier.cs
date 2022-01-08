using Fuzzer.core;

namespace Fuzzer.simplify
{
    /// <summary>
    /// A IFuzzerSimplifier implementation which provides plan where already empty phases are removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerRemovePhaseSimplifier<T> : IFuzzerSimplifier<T>
    {
        private readonly FuzzerPlan<T> _plan;
        private bool _provided;

        public FuzzerRemovePhaseSimplifier(FuzzerPlan<T> plan)
        {
            _plan = plan;
            _provided = false;
        }

        public FuzzerPlan<T> Next()
        {
            if (_provided)
            {
                return null;
            }

            _provided = true;
            var simplified = new FuzzerPlan<T>();
            foreach (var fuzzerPhase in _plan.Phases)
            {
                if (fuzzerPhase.Steps.Count > 0)
                {
                    simplified.Phases.Add(fuzzerPhase);
                }
            }

            return _plan.Phases.Count == simplified.Phases.Count ? null : simplified;
        }
    }
}