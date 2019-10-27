namespace FireSim
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a cell in a fire simulation
    /// </summary>
    public sealed class Cell : IEquatable<Cell>
    {

        /// <summary>
        /// Initialises the object
        /// </summary>
        /// <param name="index">An index for this cell</param>
        /// <param name="location">A <see cref="GridLocation"/> for this cell</param>
        /// <param name="state">The current <see cref="CellState"/> of this cell</param>
        /// <param name="isBoundary">Indicates whether this cell is a boundary cell</param>
        public Cell(int index, GridLocation location, CellState state, bool isBoundary = false)
        {
            Index = index;
            Location = location;
            State = state;
            IsBoundary = isBoundary;
        }

        /// <summary>
        /// Gets the index of this instance
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the <see cref="GridLocation"/> of this instance
        /// </summary>
        public GridLocation Location { get; }

        /// <summary>
        /// Gets the <see cref="CellState"/> of this instance
        /// </summary>
        public CellState State { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a boundary cell
        /// </summary>
        public bool IsBoundary { get; }

        #region Overrides

        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }

        public bool Equals(Cell other)
        {
            return other != null &&
                   Index == other.Index &&
                   State == other.State &&
                   Location == other.Location &&
                   IsBoundary == other.IsBoundary;
        }

        public override int GetHashCode()
        {
            var hashCode = 141045143;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            hashCode = hashCode * -1521134295 + Location.GetHashCode();
            hashCode = hashCode * -1521134295 + IsBoundary.GetHashCode();
            return hashCode;
        }


        public override string ToString()
        {
            return $"{Index} {Location} {State}";
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return EqualityComparer<Cell>.Default.Equals(left, right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !(left == right);
        }

        #endregion
    }
}
