using System;
using System.Collections.Generic;
using Fuzzer.core;

namespace Fuzzer.blueprint
{
    static class FuzzerBlueprintRandom
    {
        public static readonly Random Random = new Random();
    }

    /// <summary>
    /// FuzzerBlueprint is a utility for creating and generating FuzzerPlans.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuzzerBlueprint<T>
    {
        private readonly List<FuzzerPhase<T>> _phases;
        private Func<FuzzerStep<T>, FuzzerStep<T>> _simplifyOperation;
        private Func<FuzzerStep<T>, FuzzerStep<T>> _simplifySeed;

        public FuzzerBlueprint()
        {
            _phases = new List<FuzzerPhase<T>>();
            _simplifyOperation = null;
            _simplifySeed = null;
        }

        public FuzzerBlueprint<T> SimplifyingOperations(Func<FuzzerStep<T>, FuzzerStep<T>> simplifyOperation)
        {
            _simplifyOperation = simplifyOperation;
            return this;
        }

        public FuzzerBlueprint<T> SimplifyingSeeds(Func<FuzzerStep<T>, FuzzerStep<T>> simplifySeed)
        {
            _simplifySeed = simplifySeed;
            return this;
        }

        private FuzzerPhase<T> ActivePhase()
        {
            if (_phases.Count == 0)
            {
                throw new Exception("No active Phase");
            }

            return _phases[_phases.Count - 1];
        }

        public FuzzerBlueprint<T> Phase(int stepsMinimum, int stepsMaximum)
        {
            _phases.Add(new FuzzerPhase<T>(stepsMinimum, stepsMaximum));
            return this;
        }

        public FuzzerBlueprint<T> Step(string name, Action<T, double> implementation)
        {
            var operation = new FuzzerOperation<T>(name, implementation);
            var step = new FuzzerStep<T>(operation, 0, _simplifyOperation, _simplifySeed);
            ActivePhase().Steps.Add(step);
            return this;
        }

        public FuzzerPlan<T> Generate()
        {
            var plan = new FuzzerPlan<T>();
            foreach (var phaseBlueprint in _phases)
            {
                var fuzzerPhase = new FuzzerPhase<T>(phaseBlueprint.StepsMinimum, phaseBlueprint.StepsMaximum);
                var fuzzerPhaseSteps =
                    FuzzerBlueprintRandom.Random.Next(phaseBlueprint.StepsMinimum, phaseBlueprint.StepsMaximum);
                for (var i = 0; i < fuzzerPhaseSteps; i++)
                {
                    var fuzzerStepCandidateIndex = FuzzerBlueprintRandom.Random.Next(0, phaseBlueprint.Steps.Count);
                    var fuzzerStep = phaseBlueprint.Steps[fuzzerStepCandidateIndex];
                    var fuzzerStepSeeded = fuzzerStep.WithSeed(FuzzerBlueprintRandom.Random.NextDouble());
                    fuzzerPhase.Steps.Add(fuzzerStepSeeded);
                }

                plan.Phases.Add(fuzzerPhase);
            }

            return plan;
        }
    }
}