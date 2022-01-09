using System;

namespace Fuzzer.core
{
    /// <summary>
    /// A FuzzerStep represents a single planned interaction on a context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerStep<T>
    {
        public readonly FuzzerOperation<T> Operation;
        public readonly double Seed;
        public readonly Func<FuzzerStep<T>, FuzzerStep<T>> SimplifyOperation;
        public readonly Func<FuzzerStep<T>, FuzzerStep<T>> SimplifySeed;

        public FuzzerStep(FuzzerOperation<T> operation,
            double seed,
            Func<FuzzerStep<T>, FuzzerStep<T>> simplifyOperation = null,
            Func<FuzzerStep<T>, FuzzerStep<T>> simplifySeed = null)
        {
            Seed = seed;
            Operation = operation;
            SimplifyOperation = simplifyOperation;
            SimplifySeed = simplifySeed;
        }

        public FuzzerStep<T> WithSeed(double seed)
        {
            return new FuzzerStep<T>(Operation, seed, SimplifyOperation, SimplifySeed);
        }

        public FuzzerStep<T> WithOperation(FuzzerOperation<T> operation)
        {
            return new FuzzerStep<T>(operation, Seed, SimplifyOperation, SimplifySeed);
        }

        public FuzzerStep<T> Copy()
        {
            return new FuzzerStep<T>(Operation, Seed, SimplifyOperation, SimplifySeed);
        }

        public override string ToString()
        {
            var formatProvider = new System.Globalization.CultureInfo("en-US");
            return $"context.{Operation.Name}({Seed.ToString(formatProvider)});";
        }
    }
}