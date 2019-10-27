namespace FireSim
{
    using System.Collections.Generic;
    using FireSim.Neighbour;

    /// <summary>
    /// Provides an interface for selecting <see cref="NeighbourState"/> of a given cell
    /// </summary>
    public interface INeighbourStrategy
    {
        /// <summary>
        /// Creates the <see cref="NeighbourState"/> based on the provided parameters
        /// </summary>
        /// <param name="grid">The <see cref="IFireGrid"/> containing the cells</param>
        /// <param name="lastState">The cells at their last state</param>
        /// <param name="currentCell">The current cell to process neighbours for</param>
        /// <returns><see cref="NeighbourState"/></returns>
        NeighbourState GetNeighbours(IFireGrid grid, List<Cell> lastState, Cell currentCell);
    }
}
