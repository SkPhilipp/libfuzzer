using System;
using Fuzzer.blueprint;
using Fuzzer.core;
using NUnit.Framework;

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

        public void AssertFinite(double seed)
        {
            if (!double.IsFinite(Calculator.Value))
            {
                throw new Exception("This calculator is broken");
            }
        }
    }

    public class CalculatorFuzzerTests
    {
        [Test]
        public void FuzzCalculator()
        {
            var blueprint = new FuzzerBlueprint<CalculatorContext>()
                .Phase(0, 10)
                .Step("Add", (context, seed) => context.Add(seed))
                .Step("Subtract", (context, seed) => context.Subtract(seed))
                .Phase(1, 10)
                .Step("Multiply", (context, seed) => context.Multiply(seed))
                .Phase(1, 1)
                .Step("AssertFinite", (context, seed) => context.AssertFinite(seed));
            var fuzzer = new Fuzzer<CalculatorContext>(() => new CalculatorContext());
            fuzzer.Fuzz(blueprint, 1000);
        }

        [Test]
        public void FuzzCalculatorIncludingDivide()
        {
            Assert.Throws<FuzzerException<CalculatorContext>>(() =>
            {
                var blueprint = new FuzzerBlueprint<CalculatorContext>()
                    .Phase(0, 10)
                    .Step("Add", (context, seed) => context.Add(seed))
                    .Step("Subtract", (context, seed) => context.Subtract(seed))
                    .Phase(1, 10)
                    .Step("Divide", (context, seed) => context.Divide(seed))
                    .Phase(1, 1)
                    .Step("AssertFinite", (context, seed) => context.AssertFinite(seed));
                var fuzzer = new Fuzzer<CalculatorContext>(() => new CalculatorContext());
                fuzzer.Fuzz(blueprint, 1000);
            });
        }
    }
}