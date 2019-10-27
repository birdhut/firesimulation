namespace FireSimulation
{
    using System;
    using System.Linq;

    public class CommandArgs
    {
        private static readonly string[] ENHANCED_FLAGS = { "e", "enhanced" };
        public CommandArgs(string[] args)
        {
            UseEnhanced = ParseEnhanced(args);
        }

        public bool UseEnhanced { get; }

        private bool ParseEnhanced(string[] args)
        {
            foreach (var flag in ENHANCED_FLAGS)
            {
                if (args.Contains(flag))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
