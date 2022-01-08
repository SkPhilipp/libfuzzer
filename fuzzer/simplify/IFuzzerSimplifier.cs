using Fuzzer.core;

namespace Fuzzer.simplify
{
    /// <summary>
    /// An IFuzzerSimplifier usually references a single plan and provides alternate plans which resemble the
    /// original with minor changes simplifying it.
    ///
    /// Simplifying plans can be useful when looking at exceptions caused by executing a plan, when a simplified plan
    /// produces the same exception, it lowers the complexity of reproducing and locating the exception's cause.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFuzzerSimplifier<T>
    {
        FuzzerPlan<T> Next();
    }
}