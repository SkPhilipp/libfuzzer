using System;
using Fuzzer.blueprint;
using Fuzzer.core;
using Fuzzer.simplify;

namespace Fuzzer.Tests
{
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
                .Step("Add", (context, seed) => context.Add(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Subtract", (context, seed) => context.Subtract(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Multiply", (context, seed) => context.Multiply(seed))
                .Generate();
        }

        public static FuzzerPlan<CalculatorContext> Erring(int stepsMinimum = 1, int stepsMaximum = 10)
        {
            return Base()
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Add", (context, seed) => context.Add(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Subtract", (context, seed) => context.Subtract(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Multiply", (context, seed) => context.Multiply(seed))
                .Phase(1, 1)
                .Step("Divide", (context, seed) => context.Divide(seed))
                .Generate();
        }
    }
}