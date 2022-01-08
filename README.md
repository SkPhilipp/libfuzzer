# libfuzzer

Fuzzing is a technique that is used to find bugs and verify program behavior by generating random inputs and checking if the program behaves as expected.

libfuzzer is a library used to automatically test the functionalities of game engine components and  underlying classes. It allows for convenient and mostly automated testing of functionalities of the game engine. In addition, the fuzzer is able to incorporate new functionalities and verify existing behaviour without having to constantly write new tests.

Although fuzzing won't necessarily verify the functionality of individual methods or even classes, the fuzzer is able to make assertions on general program behaviour or state; it can ensure a program did not crash otherwise fail, it can apply its inputs to multiple instances of a program or class in parallel and ensure both instances behave the same way. For example, when fuzzing a class that can be serialized and deserialized; it could serialize and deserialize one instance and verify at the end of the fuzzing process that both instances remain equal. This allows for a much more thorough testing of the game engine, than manually written tests can provide, without having to write any tests at all.

Fuzzing can also be used to determine the performance impact of changes by comparing the performance of fuzzing with the performance of previous versions. Using more complex planning, realistic game state can be simulated allowing for accurate comparison of performance.

### Fuzzer Blueprints

The Fuzzer generates a plan of random steps to perform based on a concept called "blueprints". Blueprints themselves consist of phases and steps; Phases specify the limits of their steps, steps are the individual functionalities such as method invocations.

When fuzzing a calculator for example, a blueprint could be defined as follows:

```
(...)
  .Phase(0, 10)
  .Step("Add", (context, seed) => context.Calculator.Add(seed))
  .Step("Subtract", (context, seed) => context.Calculator.Subtract(seed))
  .Phase(0, 10)
  .Step("Multiply", (context, seed) => context.Calculator.Multiply(seed))
  .Step("Divide", (context, seed) => context.Calculator.Divide(seed))
```

This would define a blueprint for a calculator that has two phases, 0 to 10 steps. Plan is generated from this blueprint contain a random amount of steps, where each step is assigned its functionality and a random seed.

The allocation of seeds is done when a plan is generated, as the seeds play a role in the process of simplifying plans.

### Fuzzer Plan Simplification

Plan simplification is a process that is used to simplify a generated fuzzer plan. Generally, this is only done when a plan is
known to fail some kind of assertion. In the above example when a plan is generated and executed which causes a division by zero,
other steps before it are likely not necessary. When debugging a plan which fails an assertion, it is helpful when the plan is
simplified to see which steps get the assertion to fail.

The Fuzzer simplification process removes or modifies steps according to the rules of its plan. In the above example each phase
allows for 0 to 10 steps; Knowing this, the simplification process could remove steps up to all steps in a phase and verify that
after removal of a step the simplified plan still fails the assertion. When it does, the process is repeated using this new
simplified plan until no further simplifications can be applied. The above example could thus be simplified to a single step,
where only the Divide step remains.
