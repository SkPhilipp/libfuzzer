using Fuzzer.blueprint;
using NUnit.Framework;

namespace Fuzzer.Tests
{
    public class CalculatorFuzzerTests
    {
        /// <summary>
        /// Fuzzes the Calculator class, does not cover Calculator.Divide method as the division method intentionally causes an error.
        /// </summary>
        [Test]
        public void FuzzCalculator()
        {
            var blueprint = new FuzzerBlueprint<CalculatorContext>()
                .Phase(0, 10)
                .Step("Add", (context, seed) => context.Add(seed))
                .Step("Subtract", (context, seed) => context.Subtract(seed))
                .Phase(1, 1)
                .Step("Multiply", (context, seed) => context.Multiply(seed));
            var fuzzer = new Fuzzer<CalculatorContext>(() => new CalculatorContext());

            fuzzer.Replay(context => {
                // Value is no longer a normal number
                context.Multiply(0.6416859769456489);
            });
            
            fuzzer.Fuzz(blueprint, 1000);
        }
    }
}