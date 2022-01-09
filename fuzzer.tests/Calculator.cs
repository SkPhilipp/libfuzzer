namespace Fuzzer.Tests
{
    public class Calculator
    {
        public double Value { get; private set; }
        public double Interactions { get; private set; }

        public void Add(double value)
        {
            Interactions++;
            Value += value;
        }

        public void Subtract(double value)
        {
            Interactions++;
            Value -= value;
        }

        public void Multiply(double value)
        {
            Interactions++;
            Value *= value;
        }

        public void Divide(double value)
        {
            Interactions++;
            Value /= value;
        }
    }
}