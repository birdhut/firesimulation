namespace FireSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandArgs(args);

            if (options.UseEnhanced)
            {
                EnhancedSimulation.Run();
            }
            else
            {
                StandardSimulation.Run();
            }
        }
    }
}
