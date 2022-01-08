using Fuzzer.core;

namespace Fuzzer.simplify
{
    /// <summary>
    /// A FuzzerPlanCursor acts as a descending cursor referring to a step within a phase of a plan.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerPlanCursor<T>
    {
        private readonly FuzzerPlan<T> _plan;
        public int CursorPhase { private set; get; }
        public int CursorPhaseStep { private set; get; }

        public FuzzerPlanCursor(FuzzerPlan<T> plan)
        {
            _plan = plan;
            Reset();
        }

        public void Reset()
        {
            CursorPhase = _plan.Phases.Count - 1;
            if (CursorPhase == -1)
            {
                CursorPhaseStep = -1;
            }
            else
            {
                CursorPhaseStep = _plan.Phases[CursorPhase].Steps.Count;
                Next();
            }
        }

        public void Next()
        {
            if (CursorPhaseStep > 0)
            {
                CursorPhaseStep--;
                return;
            }

            while (CursorPhase > 0)
            {
                CursorPhase--;
                CursorPhaseStep = _plan.Phases[CursorPhase].Steps.Count - 1;
                if (CursorPhaseStep >= 0)
                {
                    return;
                }
            }

            CursorPhase = -1;
            CursorPhaseStep = -1;
        }

        public bool IsValid()
        {
            return CursorPhase >= 0 && CursorPhaseStep >= 0;
        }
    }
}