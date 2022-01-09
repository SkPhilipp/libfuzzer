using System.Collections.Generic;
using System.Linq;

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

        private string Indented(string code)
        {
            return "    " + code.Replace("\n", "\n    ");
        }

        /// <summary>
        /// Displays all phases as a string, with one phase's step preceded by a highlighting message.
        /// </summary>
        /// <param name="highlightIndex"></param>
        /// <param name="highlightMessage"></param>
        /// <returns></returns>
        public string ToString(int highlightIndex, string highlightMessage)
        {
            var highlightIndexRelative = highlightIndex;
            var result = new List<string>();
            result.Add("-------------------------- replay start --------------------------");
            result.Add("fuzzer.Replay(context => {");
            foreach (var phase in Phases)
            {
                if (highlightIndexRelative >= 0 && highlightIndexRelative < phase.Steps.Count)
                {
                    result.Add(Indented(phase.ToStringHighlighted(highlightIndexRelative, highlightMessage)));
                    break;
                }

                result.Add(Indented(phase.ToString()));
                highlightIndexRelative -= phase.Steps.Count;
            }

            result.Add("});");
            result.Add("-------------------------- replay end --------------------------");
            return string.Join("\n", result);
        }

        public override string ToString()
        {
            return ToString(-1, null);
        }
    }
}