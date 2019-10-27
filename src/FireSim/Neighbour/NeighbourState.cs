namespace FireSim.Neighbour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the state of specified neighbours to a given cell
    /// </summary>
    public struct NeighbourState : IEquatable<NeighbourState>
    {
        /// <summary>
        /// Initialises the object with the given states
        /// </summary>
        /// <param name="states">The <see cref="CellNeighbour"/> states</param>
        public NeighbourState(params CellNeighbour[] states)
        {
            States = states ?? throw new ArgumentNullException(nameof(states));
        }

        /// <summary>
        /// Gets the array of <see cref="CellNeighbour"/> states for this instance
        /// </summary>
        public CellNeighbour[] States { get; }

        /// <summary>
        /// Filters the current <see cref="NeighbourState"/> to include only the <see cref="CellNeighbour"/>s
        /// specified at the given <see cref="CardinalDirection"/>s
        /// </summary>
        /// <param name="directions">The <see cref="CardinalDirection"/>s to include</param>
        /// <returns><see cref="NeighbourState"/></returns>
        public NeighbourState FilterByCarinalDirections(params CardinalDirection[] directions)
        {
            var filtered = this.States.Where(x => directions.Contains(x.Direction));
            return new NeighbourState(filtered.ToArray());
        }

        /// <summary>
        /// Searches the current instance to determine whether any of the <see cref="CellNeighbour"/>s
        /// are in the specified state.
        /// </summary>
        /// <param name="state">The <see cref="CellState"/> to check</param>
        /// <returns></returns>
        public bool AnyInState(CellState state)
        {
            return States.Any(x => x.State == state);
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            return obj is NeighbourState state && Equals(state);
        }

        public bool Equals(NeighbourState other)
        {
            return EqualityComparer<CellNeighbour[]>.Default.Equals(States, other.States);
        }

        public override int GetHashCode()
        {
            return 1127215959 + EqualityComparer<CellNeighbour[]>.Default.GetHashCode(States);
        }

        public override string ToString()
        {
            return string.Join(" ", States);
        }

        public static bool operator ==(NeighbourState left, NeighbourState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NeighbourState left, NeighbourState right)
        {
            return !(left == right);
        }

        #endregion
    }
}
