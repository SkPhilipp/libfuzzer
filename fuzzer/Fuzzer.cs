using System;
using Fuzzer.blueprint;
using Fuzzer.core;
using Fuzzer.simplify;

namespace Fuzzer
{
    public class Fuzzer<T>
    {
        private readonly Func<T> _contextBuilder;

        public Fuzzer(Func<T> contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }

        /// <summary>
        /// Executes a given fuzzerPlan onto a locally built context.
        /// </summary>
        /// <param name="fuzzerPlan">The plan which to execute</param>
        /// <returns>The context on which the plan was executed</returns>
        /// <exception cref="FuzzerException&lt;T&gt;">Thrown when any exception was captured</exception>
        public T Run(FuzzerPlan<T> fuzzerPlan)
        {
            var context = _contextBuilder();
            var indexPhase = 0;
            var indexPhaseStep = 0;
            var indexOperation = 0;
            try
            {
                foreach (var fuzzerPhase in fuzzerPlan.Phases)
                {
                    foreach (var fuzzerStep in fuzzerPhase.Steps)
                    {
                        var fuzzerOperation = fuzzerStep.Operation;
                        fuzzerOperation.Implementation(context, fuzzerStep.Seed);
                        indexPhaseStep++;
                        indexOperation++;
                    }

                    indexPhaseStep = 0;
                    indexPhase++;
                }

                return context;
            }
            catch (Exception e)
            {
                throw new FuzzerException<T>(fuzzerPlan, context, e, indexPhase, indexPhaseStep, indexOperation);
            }
        }

        /// <summary>
        /// Simplifies an exception's plan by repeatedly attempting simplified plans until simplification is fully exhausted.
        /// </summary>
        /// <param name="exception">The exception which to simplify</param>
        /// <returns>The most-simplified variant of the plan which causes a FuzzerException</returns>
        private FuzzerException<T> Simplify(FuzzerException<T> exception)
        {
            var currentException = exception;
            var currentSimplifier = FuzzerCompositeSimplifier<T>.BuiltIn(currentException.Plan);

            var candidatePlan = currentSimplifier.Next();
            while (candidatePlan != null)
            {
                try
                {
                    Run(candidatePlan);
                }
                catch (FuzzerException<T> candidateException)
                {
                    currentException = candidateException;
                    currentSimplifier = FuzzerCompositeSimplifier<T>.BuiltIn(candidatePlan);
                }

                candidatePlan = currentSimplifier.Next();
            }

            return currentException;
        }

        /// <summary>
        /// Generates fuzzer plans from a given blueprint and runs each through <see cref="RunSimplifying"/>.
        /// </summary>
        /// <param name="fuzzerBlueprint">Blueprint from which to generate plans to fuzz</param>
        /// <param name="iterationsLimit">Maximum amount of iterations to attempt before ending successfully</param>
        /// <returns></returns>
        public void Fuzz(FuzzerBlueprint<T> fuzzerBlueprint, int iterationsLimit)
        {
            for (var i = 0; i < iterationsLimit; i++)
            {
                var plan = fuzzerBlueprint.Generate();
                RunSimplifying(plan);
            }
        }


        /// <summary>
        /// See <see cref="Run"/> and <see cref="Simplify"/>
        /// </summary>
        /// <param name="fuzzerPlan">The plan which to execute</param>
        /// <returns>The context on which the plan was executed</returns>
        /// <exception cref="FuzzerException&lt;T&gt;">Thrown when any exception was captured</exception>
        public T RunSimplifying(FuzzerPlan<T> fuzzerPlan)
        {
            try
            {
                return Run(fuzzerPlan);
            }
            catch (FuzzerException<T> e)
            {
                throw Simplify(e);
            }
        }
    }
}