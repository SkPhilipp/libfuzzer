using System;
using Fuzzer.blueprint;
using Fuzzer.core;
using Fuzzer.simplify;

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
            if (!double.IsFinite(Value))
            {
                throw new Exception("This calculator is broken");
            }
        }

        public void Subtract(double value)
        {
            Interactions++;
            Value -= value;
            if (!double.IsFinite(Value))
            {
                throw new Exception("This calculator is broken");
            }
        }

        public void Multiply(double value)
        {
            Interactions++;
            Value *= value;
            if (!double.IsFinite(Value))
            {
                throw new Exception("This calculator is broken");
            }
        }

        public void Divide(double value)
        {
            Interactions++;
            Value /= value;
            if (!double.IsFinite(Value))
            {
                throw new Exception("This calculator is broken");
            }
        }
    }
}