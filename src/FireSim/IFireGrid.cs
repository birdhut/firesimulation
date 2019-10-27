namespace FireSim
{
    /// <summary>
    /// Provides an interface for a Fire Grid simulation
    /// </summary>
    public interface IFireGrid
    {
        /// <summary>
        /// Gets the height of the grid
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the width of the grid
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets a value indicating whether the simulation is complete
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Performs a spread of the fire
        /// </summary>
        /// <param name="newSpreadStrategy">An optional strategy to use when spreading the fire</param>
        void Spread(ISpreadStrategy newSpreadStrategy = null);

        /// <summary>
        /// Renders the current simulation state to the provided renderer
        /// </summary>
        /// <param name="renderer"><see cref="IRenderer"/> to render to</param>
        void Render(IRenderer renderer);
    }
}