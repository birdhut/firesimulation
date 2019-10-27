namespace FireSim.Test.TwoD
{
    using System;
    using System.Linq;
    using FireSim.TwoD;
    using Shouldly;
    using Xunit;

    public class FireGridTests
    {
        [Theory]
        [InlineData(21, 21)]
        public void T01_Generates_Grid_Rows_And_Columns(int width, int height)
        {
            FireGrid grid = new FireGrid(width, height);

            int rowCount = grid.InternalCells.Select(x => x.Location.Row).Distinct().Count();
            int colCount = grid.InternalCells.Select(x => x.Location.Column).Distinct().Count();

            rowCount.ShouldBe(width);
            colCount.ShouldBe(height);
        }

        [Theory]
        [InlineData(21, 21)]
        public void T02_Generates_Grid_Center_With_Fire(int width, int height)
        {
            FireGrid grid = new FireGrid(width, height);

            int center = (width * height) / 2;
            Cell centerCell = grid.InternalCells[center];

            centerCell.Index.ShouldBe(center);
            centerCell.IsBoundary.ShouldBeFalse();
            centerCell.State.ShouldBe(CellState.Burning);
        }

        [Theory]
        [InlineData(21, 21)]
        public void T03_Generates_Empty_Grid_Boundary(int width, int height)
        {
            int boundaryCount = (width * 2) + ((height - 2) * 2);
            
            FireGrid grid = new FireGrid(width, height);

            var boundary = grid.InternalCells.Where(x => x.IsBoundary).ToList();

            boundary.Count.ShouldBe(boundaryCount);
            boundary.All(x => x.State == CellState.Empty).ShouldBeTrue();
        }

        [Theory]
        [InlineData(21, 21)]
        public void T04_Generates_Trees(int width, int height)
        {
            int boundaryCount = (width * 2) + ((height - 2) * 2);
            int centerCount = 1;
            int expectedTreeCount = (width * height) - boundaryCount - centerCount;

            FireGrid grid = new FireGrid(width, height);

            var treeCount = grid.InternalCells.Count(x => x.State == CellState.Tree);

            treeCount.ShouldBe(expectedTreeCount);
        }

        [Fact]
        public void T05_Performs_Spread()
        {
            FireGrid grid = new FireGrid(21, 21);
            var lastStates = grid.InternalCells.Select(x => x.State);

            while (!grid.IsCompleted)
            {
                grid.Spread();

                var nextStates = grid.InternalCells.Select(x => x.State);
                lastStates.SequenceEqual(nextStates).ShouldBeFalse();
                lastStates = nextStates;
            }
        }

        [Fact]
        public void T05_Performs_Render()
        {
            var counter = new RenderCounter();
            FireGrid grid = new FireGrid(21, 21);

            grid.Render(counter);

            counter.RowRenders.ShouldBe(20);
            counter.ColumnRenders.ShouldBe(420);
            counter.CellRenders.ShouldBe(441);
        }

        private class RenderCounter : IRenderer
        {
            public int ColumnRenders { get; private set; }
            public int RowRenders { get; private set; }
            public int CellRenders { get; private set; }
            public bool MoveNextColumn()
            {
                ColumnRenders += 1;
                return true;
            }

            public bool MoveNextRow()
            {
                RowRenders += 1;
                return true;
            }

            public void Render(Cell cell)
            {
                CellRenders += 1;
            }
        }
    }
}
