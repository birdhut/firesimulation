namespace FireSim.TwoD
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FireSim.Strategy;

    /// <summary>
    /// Provides a <see cref="IFireGrid"/> implementation based upon storing the cells in a two-dimensional array
    /// </summary>
    public sealed class FireGrid : IFireGrid
    {
        /// <summary>
        /// Width and Height cannot be less than this
        /// </summary>
        private const int MIN_SIZE = 3;
        private readonly INeighbourStrategy neighbourStrategy;
        private readonly GridLocation? fireLocation;

        /// <summary>
        /// Initialises the grid
        /// </summary>
        /// <param name="width">The width of the grid</param>
        /// <param name="height">The height of the grid</param>
        /// <param name="fireLocation">Optional starting location of the fire.  Center cell is used if null.</param>
        /// <param name="neighbourStrategy">Optional neighbour strategy.  <see cref="CardinalNeighbourStrategy"/> is used if null</param>
        public FireGrid(int width, int height, GridLocation? fireLocation = null, INeighbourStrategy neighbourStrategy = null)
        {
            if (width < MIN_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height < MIN_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            Width = width;
            Height = height;
            this.fireLocation = fireLocation == null ? new GridLocation(Width / 2, Height / 2) : fireLocation;
            this.neighbourStrategy = neighbourStrategy ?? new CardinalNeighbourStrategy();
            this.InternalCells = CreateCells();
            SetCompletedState();
        }

        /// <summary>
        /// Gets the width of the grid
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of the grid
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets a value indicating that no fires are left in the grid
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Gets the current state of cells in the grid
        /// </summary>
        internal List<Cell> InternalCells { get; private set; }

        /// <summary>
        /// Spreads the fire by one increment
        /// </summary>
        /// <param name="newSpreadStrategy">Optional spread strategy.  If null, <see cref="StandardSpreadStrategy"/> is used</param>
        public void Spread(ISpreadStrategy newSpreadStrategy = null)
        {
            if (IsCompleted)
            {
                return;
            }

            var spreadStrategy = newSpreadStrategy ?? new StandardSpreadStrategy();

            List<Cell> nextCells = new List<Cell>(this.InternalCells.Count);

            foreach (var cell in this.InternalCells)
            {
                var neighbours = this.neighbourStrategy.GetNeighbours(this, this.InternalCells, cell);
                var state = spreadStrategy.Spread(cell, neighbours);

                nextCells.Add(new Cell(cell.Index, cell.Location, state, cell.IsBoundary));
            }

            this.InternalCells = nextCells;
            SetCompletedState();
        }

        /// <summary>
        /// Renders the current cell state to the <see cref="IRenderer"/>
        /// </summary>
        /// <param name="renderer">Renderer to render to</param>
        public void Render(IRenderer renderer)
        {
            var lastLocation = this.GetGridLocation(0);
            foreach (var cell in this.InternalCells)
            {
                var location = this.GetGridLocation(cell.Index);
                if (location.Row > lastLocation.Row)
                {
                    lastLocation = location;
                    renderer.MoveNextRow();
                }
                else if (location.Column > lastLocation.Column)
                {
                    renderer.MoveNextColumn();
                }

                renderer.Render(cell);
            }
        }

        /// <summary>
        /// Calculates whether completed and sets <see cref="IsCompleted"/>
        /// </summary>
        private void SetCompletedState()
        {
            IsCompleted = !this.InternalCells.Any(x => x.State == CellState.Burning);
        }

        /// <summary>
        /// Generates the starting cells for this instance
        /// </summary>
        /// <returns><see cref="List{Cell}"/></returns>
        private List<Cell> CreateCells()
        {
            var totalCells = Width * Height;
            List<Cell> items = new List<Cell>(totalCells);
            
            for (int idx = 0; idx < totalCells; idx++)
            {
                var location = this.GetGridLocation(idx);
                bool isEdge = this.IsBorder(location.Row, location.Column);

                CellState state = isEdge ? CellState.Empty :
                    location == this.fireLocation ? CellState.Burning :
                        CellState.Tree;

                items.Add(new Cell(idx, location, state, isEdge));
            }

            return items;
        }
    }
}
