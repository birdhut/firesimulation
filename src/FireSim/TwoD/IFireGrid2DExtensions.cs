namespace FireSim.TwoD
{
    /// <summary>
    /// Provides extension methods for <see cref="IFireGrid"/> based upon a 2D array implementation
    /// </summary>
    public static class IFireGrid2DExtensions
    {
        /// <summary>
        /// Obtains the <see cref="GridLocation"/> of a cell based on index
        /// </summary>
        /// <param name="grid">The grid to which to refer</param>
        /// <param name="index">The index of the cell to obtain location for</param>
        /// <returns><see cref="GridLocation"/></returns>
        public static GridLocation GetGridLocation(this IFireGrid grid, int index) => 
            new GridLocation(index / grid.Width, index % grid.Width);

        /// <summary>
        /// Obtains the index of a cell based upon the row and column
        /// </summary>
        /// <param name="grid">The grid to which to refer</param>
        /// <param name="row">The row to target</param>
        /// <param name="col">The column to target</param>
        /// <returns><see cref="int"/> representing the index</returns>
        public static int GetIndex(this IFireGrid grid, int row, int col) => 
            (row * grid.Height) + col;

        /// <summary>
        /// Calculates whether the given row and column are on the outer border of the grid
        /// </summary>
        /// <param name="grid">The grid to which to refer</param>
        /// <param name="row">The row to target</param>
        /// <param name="col">The column to target</param>
        /// <returns>True if on the border, false otherwise</returns>
        public static bool IsBorder(this IFireGrid grid, int row, int col) =>
            row == 0 ||
            row == grid.Height - 1 ||
            col == 0 ||
            col == grid.Width - 1;
    }
}
