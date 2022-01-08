using System.Collections.Generic;

namespace Fuzzer.core
{
    /// <summary>
    /// A FuzzerPlan represents multiple phases of planned interactions on a context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerPlan<T>
    {
        public readonly List<FuzzerPhase<T>> Phases;

        public FuzzerPlan()
        {
            Phases = new List<FuzzerPhase<T>>();
        }

        public FuzzerPlan<T> Copy()
        {
            var copy = new FuzzerPlan<T>();
            foreach (var phase in Phases)
            {
                copy.Phases.Add(phase.Copy());
            }

            return copy;
        }

        /// <summary>
        /// Displays all phases as a string, with one particular operation highlighted.
        /// </summary>
        /// <param name="highlightIndex"></param>
        /// <param name="highlightMessage"></param>
        /// <returns></returns>
        public string ToString(int highlightIndex, string highlightMessage)
        {
            var highlightIndexRelative = highlightIndex;
            var result = new List<string>();
            foreach (var phase in Phases)
            {
                if (highlightIndexRelative >= 0 && highlightIndexRelative < phase.Steps.Count)
                {
                    result.Add(phase.ToString(highlightIndexRelative, highlightMessage));
                }
                else
                {
                    result.Add(phase.ToString());
                }

                highlightIndexRelative -= phase.Steps.Count;
            }

            return string.Join("\n", result);
        }

        public override string ToString()
        {
            return ToString(-1, null);
        }
    }
}