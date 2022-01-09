using Fuzzer.core;

namespace Fuzzer.Tests
{
    public class CalculatorContext
    {
        public readonly Calculator Calculator = new();

        public void Add(double seed)
        {
            var value = FuzzerUtils.SeedToInt(seed, 10);
            Calculator.Add(value);
        }

        public void Subtract(double seed)
        {
            var value = FuzzerUtils.SeedToInt(seed, 10);
            Calculator.Subtract(value);
        }

        public void Multiply(double seed)
        {
            var value = FuzzerUtils.SeedToInt(seed, 10);
            Calculator.Multiply(value);
        }

        public void Divide(double seed)
        {
            // always divides by 0
            var value = FuzzerUtils.SeedToInt(seed, 1);
            Calculator.Divide(value);
        }
    }
}