namespace FireSimulation
{
    using System;
    using FireSim.Renderer;
    using FireSim.TwoD;

    internal static class StandardSimulation
    {
        public static void Run()
        {
            const char QUIT = 'q';
            FireGrid grid = new FireGrid(21, 21);

            int step = 1;
            var renderer = new StringRenderer();
            Console.WriteLine("Standard Fire Simulation");
            Console.WriteLine("------------------------");
            Console.WriteLine();
            Console.WriteLine("Key:");
            Console.WriteLine($"\tEmpty = \"{renderer.Empty}\"");
            Console.WriteLine($"\tTree = \"{renderer.Tree}\"");
            Console.WriteLine($"\tBurning = \"{renderer.Fire}\"");
            Console.WriteLine();
            Console.WriteLine("Press any key to proceed, or \"q\" to quit");
            Console.WriteLine();

            bool proceed = true;

            while (proceed)
            {
                Console.WriteLine($"Step {step}:");
                Console.WriteLine();

                renderer = new StringRenderer();
                grid.Render(renderer);
                Console.WriteLine(renderer.ToString());

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("hit any key except \"q\" to continue...");

                Console.WriteLine();
                Console.WriteLine();

                if (grid.IsCompleted)
                {
                    break;
                }

                var key = Console.ReadKey();
                if (key.KeyChar == QUIT)
                {
                    proceed = false;
                    Console.WriteLine();
                    Console.WriteLine("user has quit...");
                }
                else
                {
                    proceed = true;
                }

                if (proceed)
                {
                    step++;
                    grid.Spread();
                }
            }

            Console.WriteLine();
            Console.WriteLine($"completed {step} steps");
        }
    }
}
