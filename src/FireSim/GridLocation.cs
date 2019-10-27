namespace FireSim
{
    using System;

    /// <summary>
    /// Represents a row/column location on a grid
    /// </summary>
    public struct GridLocation : IEquatable<GridLocation>
    {
        /// <summary>
        /// Represents an empty grid location
        /// </summary>
        public static readonly GridLocation Empty = new GridLocation(-1, -1);

        /// <summary>
        /// Initialises the object
        /// </summary>
        /// <param name="row">The row for this location</param>
        /// <param name="column">The column for this location</param>
        public GridLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Gets the row for this location
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets the column for this location
        /// </summary>
        public int Column { get; }

        #region Overrides

        public override bool Equals(object obj)
        {
            return obj is GridLocation location && Equals(location);
        }

        public bool Equals(GridLocation other)
        {
            return Row == other.Row &&
                   Column == other.Column;
        }

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Column.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"Row {Row} Column {Column}";
        }

        public static bool operator ==(GridLocation left, GridLocation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridLocation left, GridLocation right)
        {
            return !(left == right);
        }

        #endregion
    }
}
