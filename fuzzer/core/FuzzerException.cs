using System;
using System.Text;

namespace Fuzzer.core
{
    /// <summary>
    /// A FuzzerException captures information of an exception which occurs during a fuzzing iteration.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerException<T> : Exception
    {
        public readonly FuzzerPlan<T> Plan;
        public readonly T Context;
        public readonly int IndexPhase;
        public readonly int IndexPhaseStep;
        public readonly int IndexOperation;

        public FuzzerException(FuzzerPlan<T> plan, T context, Exception exception,
            int indexPhase, int indexPhaseStep, int indexOperation)
            : base(null, exception)
        {
            Plan = plan;
            Context = context;
            IndexPhase = indexPhase;
            IndexPhaseStep = indexPhaseStep;
            IndexOperation = indexOperation;
        }

        public override string Message
        {
            get
            {
                var result = new StringBuilder();
                var message = InnerException?.Message ?? "Erred";
                result.Append("\n");
                result.Append(Plan.ToString(IndexOperation, message));
                return result.ToString();
            }
        }
    }
}