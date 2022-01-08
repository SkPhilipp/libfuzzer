using System.Collections.Generic;
using Fuzzer.core;

namespace Fuzzer.simplify
{
    /// <summary>
    /// A IFuzzerSimplifier implementation which provides plans simplified by a delegate simplifier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerCompositeSimplifier<T> : IFuzzerSimplifier<T>
    {
        private readonly List<IFuzzerSimplifier<T>> _delegates;

        private FuzzerCompositeSimplifier(List<IFuzzerSimplifier<T>> delegates)
        {
            _delegates = delegates;
        }

        public FuzzerPlan<T> Next()
        {
            FuzzerPlan<T> simplified = null;
            while (simplified == null && _delegates.Count > 0)
            {
                var simplifier = _delegates[_delegates.Count - 1];
                simplified = simplifier.Next();
                if (simplified == null)
                {
                    _delegates.RemoveAt(_delegates.Count - 1);
                }
            }

            return simplified;
        }

        /// <summary>
        /// Constructs a new simplifier for the given plan, containing;
        /// - FuzzerRemoveSimplifier
        /// - FuzzerReplaceOperationSimplifier
        /// - FuzzerReplaceSeedSimplifier
        /// </summary>
        /// <returns></returns>
        public static IFuzzerSimplifier<T> BuiltIn(FuzzerPlan<T> plan)
        {
            var fuzzerSimplifiers = new List<IFuzzerSimplifier<T>>
            {
                new FuzzerRemoveSimplifier<T>(plan),
                new FuzzerReplaceOperationSimplifier<T>(plan),
                new FuzzerReplaceSeedSimplifier<T>(plan),
                new FuzzerRemovePhaseSimplifier<T>(plan)
            };
            return new FuzzerCompositeSimplifier<T>(fuzzerSimplifiers);
        }
    }
}