namespace FireSim.Neighbour
{
    using System;

    /// <summary>
    /// Represents a neighbouring cell by <see cref="CardinalDirection"/>
    /// and <see cref="CellState"/>
    /// </summary>
    public struct CellNeighbour : IEquatable<CellNeighbour>
    {
        /// <summary>
        /// Initialises the object
        /// </summary>
        /// <param name="direction">The direction of the neighbour</param>
        /// <param name="state">The state of the neighbour</param>
        public CellNeighbour(CardinalDirection direction, CellState state)
        {
            Direction = direction;
            State = state;
        }

        /// <summary>
        /// Gets the <see cref="CardinalDirection"/> which this instance represents
        /// </summary>
        public CardinalDirection Direction { get; }

        /// <summary>
        /// Gets the <see cref="CellState"/> of the cell represented by the <see cref="Direction"/>
        /// </summary>
        public CellState State { get; }

        #region Overrides

        public override bool Equals(object obj)
        {
            return obj is CellNeighbour neighbour && Equals(neighbour);
        }

        public bool Equals(CellNeighbour other)
        {
            return Direction == other.Direction &&
                   State == other.State;
        }

        public override int GetHashCode()
        {
            var hashCode = -1698409424;
            hashCode = hashCode * -1521134295 + Direction.GetHashCode();
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Direction}={State}";
        }

        public static bool operator ==(CellNeighbour left, CellNeighbour right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CellNeighbour left, CellNeighbour right)
        {
            return !(left == right);
        }

        #endregion
    }
}
