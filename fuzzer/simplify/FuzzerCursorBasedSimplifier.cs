using Fuzzer.core;

namespace Fuzzer.simplify
{
    public abstract class FuzzerCursorBasedSimplifier<T> : IFuzzerSimplifier<T> where T : FuzzerContext
    {
        protected readonly FuzzerPlan<T> Plan;
        protected readonly FuzzerPlanCursor<T> PlanCursor;

        protected FuzzerCursorBasedSimplifier(FuzzerPlan<T> plan)
        {
            Plan = plan;
            PlanCursor = new FuzzerPlanCursor<T>(plan);
        }

        /// <summary>
        /// Creates a simplified plan by making a change at the step to which the cursor currently points.
        /// </summary>
        /// <returns>A simplified plan, or `null` indicating the simplification is not possible at the cursor's step. </returns>
        protected abstract FuzzerPlan<T> SimplifyCurrent();

        public FuzzerPlan<T> Next()
        {
            while (PlanCursor.IsValid())
            {
                var plan = SimplifyCurrent();
                PlanCursor.Next();
                if (plan != null)
                {
                    return plan;
                }
            }

            return null;
        }
    }
}