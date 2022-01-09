using Fuzzer.blueprint;

namespace Fuzzer.Tests
{
    public static class CalculatorBlueprints
    {
        public static FuzzerBlueprint<CalculatorContext> MultiPhase(int stepsMinimum = 1, int stepsMaximum = 10)
        {
            return new FuzzerBlueprint<CalculatorContext>()
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Add", (context, seed) => context.Add(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Subtract", (context, seed) => context.Subtract(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Multiply", (context, seed) => context.Multiply(seed));
        }

        public static FuzzerBlueprint<CalculatorContext> Erring(int stepsMinimum = 1, int stepsMaximum = 10)
        {
            return new FuzzerBlueprint<CalculatorContext>()
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Add", (context, seed) => context.Add(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Subtract", (context, seed) => context.Subtract(seed))
                .Phase(stepsMinimum, stepsMaximum)
                .Step("Multiply", (context, seed) => context.Multiply(seed))
                .Phase(1, 1)
                .Step("Divide", (context, seed) => context.Divide(seed))
                .Phase(1, 1)
                .Step("AssertFinite", (context, seed) => context.AssertFinite(seed));
        }
    }
}