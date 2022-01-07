using System;

namespace Fuzzer.core
{
    /// <summary>
    /// A FuzzerOperation is a named interaction which can be applied on a context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerOperation<T> where T : FuzzerContext
    {
        public readonly string Name;
        public readonly Action<T, double> Implementation;

        public FuzzerOperation(string name, Action<T, double> implementation)
        {
            Name = name;
            Implementation = implementation;
        }
    }
}