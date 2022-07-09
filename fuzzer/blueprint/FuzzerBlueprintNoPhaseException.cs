using System;

namespace Fuzzer.blueprint
{
    public class FuzzerBlueprintNoPhaseException : Exception
    {
        public FuzzerBlueprintNoPhaseException() : base($"No Phase has been registered yet on this blueprint.")
        {
        }
    }
}