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

        public void MakeError()
        {
            Interactions++;
            throw new Exception("A calculated error");
        }
    }

    public class CalculatorContext
    {
        public readonly Calculator Calculator = new();
    }

    public static class CalculatorBlueprints
    {
        private static FuzzerBlueprint<CalculatorContext> Base()
        {
            var doNothingOperation = new FuzzerOperation<CalculatorContext>("DoNothing", (_, _) => { });
            var doNothingReplacer = new Func<FuzzerStep<CalculatorContext>, FuzzerStep<CalculatorContext>>(step =>
                step.WithOperation(doNothingOperation));
            return new FuzzerBlueprint<CalculatorContext>()
                .SimplifyingOperations(doNothingReplacer)
                .SimplifyingSeeds(FuzzerReplaceSeedSimplifier<CalculatorContext>.ZeroSeed);
        }

        public static FuzzerPlan<CalculatorContext> MultiPhase(int stepsMinimum = 1, int stepsMaximum = 10)
        {
            return Base()
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Add", (context, seed) => context.Calculator.Add(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Subtract", (context, seed) => context.Calculator.Subtract(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Multiply", (context, seed) => context.Calculator.Multiply(seed))
                .Generate();
        }

        public static FuzzerPlan<CalculatorContext> Erring(int stepsMinimum = 1, int stepsMaximum = 10)
        {
            return Base()
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Add", (context, seed) => context.Calculator.Add(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Subtract", (context, seed) => context.Calculator.Subtract(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Multiply", (context, seed) => context.Calculator.Multiply(seed))
                .Phase(1, 1)
                .Step("MakeError", (context, _) => context.Calculator.MakeError())
                .Generate();
        }
    }
}