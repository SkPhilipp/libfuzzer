using System;

namespace Fuzzer.core
{
    /// <summary>
    /// A FuzzerContext is the target of fuzzer interactions and holds state during a fuzzing iteration.
    /// </summary>
    public class FuzzerContext
    {
        /// <summary>
        /// Returns an integer that is greater than or equal to 0 and less than the given max value.
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="exclusiveLimit"></param>
        /// <returns></returns>
        protected int SeedToInt(double seed, int exclusiveLimit)
        {
            return Convert.ToInt32(Math.Floor(seed * exclusiveLimit));
        }
    }
}