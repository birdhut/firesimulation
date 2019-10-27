namespace FireSim.Test.TwoD
{
    using FireSim.TwoD;
    using Shouldly;
    using Xunit;

    public class IFireGrid2DExtensionTests
    {
        [Theory]
        [InlineData(7, 7, 23, 3, 2)]
        public void T01_Identifies_Row_And_Column(int height, int width, int index, int row, int column)
        {
            var grid = new MockFireGrid(height, width, false);
            GridLocation expectedLocation = new GridLocation(row, column);

            var location = grid.GetGridLocation(index);

            location.ShouldBe(expectedLocation);
        }

        [Theory]
        [InlineData(7, 7, 3, 2, 23)]
        public void T02_Identifies_Index(int height, int width, int row, int column, int expectedindex)
        {
            var grid = new MockFireGrid(height, width, false);

            var index = grid.GetIndex(row, column);

            index.ShouldBe(expectedindex);
        }

        [Theory]
        [InlineData(7, 7, 0, 0, true)]
        public void T03_Identifies_Boundary(int height, int width, int row, int column, bool expectBoundary)
        {
            var grid = new MockFireGrid(height, width, false);

            var isBoundary = grid.IsBorder(row, column);

            isBoundary.ShouldBe(expectBoundary);
        }
    }
}
