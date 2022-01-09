using System.Collections.Generic;

namespace Fuzzer.core
{
    /// <summary>
    /// A FuzzerPhase represents multiple planned interactions on a context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerPhase<T>
    {
        public readonly List<FuzzerStep<T>> Steps;
        public readonly int StepsMinimum;
        public readonly int StepsMaximum;

        public FuzzerPhase(int stepsMinimum, int stepsMaximum)
        {
            Steps = new List<FuzzerStep<T>>();
            StepsMinimum = stepsMinimum;
            StepsMaximum = stepsMaximum;
        }

        public FuzzerPhase<T> Copy()
        {
            var copy = new FuzzerPhase<T>(StepsMinimum, StepsMaximum);
            foreach (var step in Steps)
            {
                copy.Steps.Add(step.Copy());
            }

            return copy;
        }

        /// <summary>
        /// Displays all steps as a string, with one step preceded by a highlighting message.
        /// </summary>
        /// <param name="highlightIndex"></param>
        /// <param name="highlightMessage"></param>
        /// <returns></returns>
        public string ToStringHighlighted(int highlightIndex, string highlightMessage)
        {
            var result = new List<string>();
            for (var i = 0; i < highlightIndex; i++)
            {
                result.Add(Steps[i].ToString());
            }

            result.Add($"// {highlightMessage}");
            result.Add(Steps[highlightIndex].ToString());

            return string.Join("\n", result);
        }

        public override string ToString()
        {
            var result = new List<string>();
            foreach (var step in Steps)
            {
                result.Add(step.ToString());
            }

            return string.Join("\n", result);
        }
    }
}