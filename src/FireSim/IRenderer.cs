namespace FireSim
{
    /// <summary>
    /// Provides an interface for a fire simulation renderer
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Renders the given cell
        /// </summary>
        /// <param name="cell">The <see cref="Cell"/> to render</param>
        void Render(Cell cell);

        /// <summary>
        /// Renders a new column
        /// </summary>
        /// <returns>True if succeeded, False otherwise</returns>
        bool MoveNextColumn();

        /// <summary>
        /// Renders a new row
        /// </summary>
        /// <returns>True if succeeded, False otherwise</returns>
        bool MoveNextRow();
    }
}
