using System;
namespace FireSimulation
{
    using System.Linq;
    using FireSim;
    using FireSim.Neighbour;
    using FireSim.Renderer;
    using FireSim.TwoD;

    internal static class EnhancedSimulation
    {
        public static void Run()
        {
            const char QUIT = 'q';
            FireGrid grid = new FireGrid(21, 21);

            int step = 1;
            var renderer = new StringRenderer();
            var northerlySpread = new NortherlyWindSpreadStrategy();

            Console.WriteLine("Northerly Wind Fire Simulation");
            Console.WriteLine("------------------------------");
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
                    grid.Spread(northerlySpread);
                }
            }

            Console.WriteLine();
            Console.WriteLine($"completed {step} steps");
        }

        /// <summary>
        /// Simulates a northerly wind, only spreading fire from the south point of a cell
        /// </summary>
        private class NortherlyWindSpreadStrategy : ISpreadStrategy
        {
            public CellState Spread(Cell cell, NeighbourState neighbourState)
            {
                if (cell.IsBoundary ||
                    cell.State == CellState.Burning ||
                    cell.State == CellState.Empty)
                {
                    return CellState.Empty;
                }

                var southNeighbour = neighbourState.States
                    .Where(x => x.Direction == CardinalDirection.South)
                    .FirstOrDefault();

                if (southNeighbour == default)
                {
                    return cell.State;
                }

                if (southNeighbour.State == CellState.Burning)
                {
                    return CellState.Burning;
                }

                return CellState.Tree;
            }
        }
    }
}
