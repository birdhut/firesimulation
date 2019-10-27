using FireSim.Neighbour;

namespace FireSim
{
    /// <summary>
    /// Provides an interface for determining spread
    /// </summary>
    public interface ISpreadStrategy
    {
        /// <summary>
        /// Performs a fire spread on the given <see cref="Cell"/> based on the <see cref="NeighbourState"/>
        /// </summary>
        /// <param name="cell">The cell to act on</param>
        /// <param name="neighbourState">The state of the neighbours</param>
        /// <returns><see cref="CellState"/></returns>
        CellState Spread(Cell cell, NeighbourState neighbourState);
    }
}
