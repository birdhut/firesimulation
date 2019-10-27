namespace FireSim.Strategy
{
    using FireSim.Neighbour;

    /// <summary>
    /// Provides the default <see cref="ISpreadStrategy"/>, 
    /// retaining boundaries as <see cref="CellState.Empty"/>m
    /// setting the state to <see cref="CellState.Burning"/> if a tree is present and any main cardinal point neighbour is buring,
    /// setting the state to <see cref="CellState.Empty"/> if a fire is present
    /// </summary>
    public class StandardSpreadStrategy : ISpreadStrategy
    {
        /// <summary>
        /// Initialises the object
        /// </summary>
        public StandardSpreadStrategy()
        {
        }

        /// <summary>
        /// Performs the default spread on the given <see cref="Cell"/> based on the <see cref="NeighbourState"/>
        /// </summary>
        /// <param name="cell">The cell to act on</param>
        /// <param name="neighbourState">The state of the neighbours</param>
        /// <returns><see cref="CellState"/></returns>
        public CellState Spread(Cell cell, NeighbourState neighbourState)
        {
            if (cell.IsBoundary)
            {
                return CellState.Empty;
            }

            var cellstate = cell.State;
            switch (cellstate)
            {
                case CellState.Tree:
                    {
                        // only use n, e, s, w directions
                        var neighbours = neighbourState.FilterByCarinalDirections(
                            CardinalDirection.North,
                            CardinalDirection.East,
                            CardinalDirection.South,
                            CardinalDirection.West);

                        return neighbours.AnyInState(CellState.Burning) ?
                            CellState.Burning
                            :
                            CellState.Tree;
                    }
                case CellState.Burning:
                    return CellState.Empty;
                case CellState.Empty:
                default:
                    return CellState.Empty;
            }
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "Standard";
        }

        #endregion
    }
}
