using System;

namespace Fuzzer.core
{
    public static class FuzzerUtils
    {
        /// <summary>
        /// Returns an integer that is greater than or equal to 0 and less than the given max value.
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="exclusiveLimit"></param>
        /// <returns></returns>
        public static int SeedToInt(double seed, int exclusiveLimit)
        {
            return Convert.ToInt32(Math.Floor(seed * exclusiveLimit));
        }
    }
}