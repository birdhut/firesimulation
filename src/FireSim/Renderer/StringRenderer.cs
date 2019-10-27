namespace FireSim.Renderer
{
    using System.Text;

    /// <summary>
    /// Provides an implementation of an <see cref="IRenderer"/> that writes to a string
    /// </summary>
    public class StringRenderer : IRenderer
    {
        private const string COL_SPACE = " ";
        private const string DEFAULT_EMPTY = "O";
        private const string DEFAULT_TREE = "!";
        private const string DEFAULT_FIRE = "^";
        private const string ROW_TERMINATOR = "\n";

        private readonly StringBuilder builder;

        /// <summary>
        /// Initialises the object with default values
        /// </summary>
        public StringRenderer() : this(DEFAULT_EMPTY, DEFAULT_TREE, DEFAULT_FIRE)
        {
        }

        /// <summary>
        /// Initialises the object with a custom string for empty and default for tree and fire
        /// </summary>
        /// <param name="empty">The string for empty</param>
        public StringRenderer(string empty) : this(empty, DEFAULT_TREE, DEFAULT_FIRE)
        {
        }

        /// <summary>
        /// Initialises the object with a custom string for empty and tree and default fire
        /// </summary>
        /// <param name="empty">The string for empty</param>
        /// <param name="tree">The string for tree</param>
        public StringRenderer(string empty, string tree) : this(empty, tree, DEFAULT_FIRE)
        {
        }

        /// <summary>
        /// Initialises the object with a custom string for empty, tree and fire
        /// </summary>
        /// <param name="empty">The string for empty</param>
        /// <param name="tree">The string for tree</param>
        /// <param name="fire">The string for fire</param>
        public StringRenderer(string empty, string tree, string fire)
        {
            Empty = empty;
            Tree = tree;
            Fire = fire;
            this.builder = new StringBuilder(1000);
        }

        /// <summary>
        /// Gets the value used for <see cref="CellState.Empty"/>
        /// </summary>
        public string Empty { get; }

        /// <summary>
        /// Gets the value used for <see cref="CellState.Tree"/>
        /// </summary>
        public string Tree { get; }

        /// <summary>
        /// Gets the value used for <see cref="CellState.Burning"/>
        /// </summary>
        public string Fire { get; }

        /// <summary>
        /// Advances the string by a space
        /// </summary>
        /// <returns>True</returns>
        public bool MoveNextColumn()
        {
            builder.Append(COL_SPACE);
            return true;
        }

        /// <summary>
        /// Advances the string by a new line
        /// </summary>
        /// <returns></returns>
        public bool MoveNextRow()
        {
            builder.Append(ROW_TERMINATOR);
            return true;
        }

        /// <summary>
        /// Renders the given cell as a string
        /// </summary>
        /// <param name="cell">The <see cref="Cell"/> to render</param>
        public void Render(Cell cell)
        {
            switch (cell.State)
            {
                case CellState.Burning:
                    builder.Append(Fire);
                    break;
                case CellState.Tree:
                    builder.Append(Tree);
                    break;
                case CellState.Empty:
                default:
                    builder.Append(Empty);
                    break;
            }
        }

        /// <summary>
        /// Returns the entire rendering as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return builder.ToString();
        }

    }
}
