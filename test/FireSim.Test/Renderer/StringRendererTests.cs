namespace FireSim.Test.Renderer
{
    using FireSim.Renderer;
    using Shouldly;
    using Xunit;

    public class StringRendererTests
    {
        [Fact]
        public void T01_Can_Move_Column()
        {
            var renderer = new StringRenderer();
            string expected = " ";

            bool result = renderer.MoveNextColumn();

            result.ShouldBeTrue();
            renderer.ToString().ShouldBe(expected);
        }

        [Fact]
        public void T02_Can_Move_Row()
        {
            var renderer = new StringRenderer();
            string expected = "\n";

            bool result = renderer.MoveNextRow();

            result.ShouldBeTrue();
            renderer.ToString().ShouldBe(expected);
        }

        [Theory]
        [InlineData(CellState.Empty)]
        [InlineData(CellState.Tree)]
        [InlineData(CellState.Burning)]
        public void T03_Renders_State(CellState state)
        {
            var renderer = new StringRenderer();
            Cell cell = new Cell(0, new GridLocation(0, 0), state, false);
            string expected = state == CellState.Burning ? renderer.Fire :
                state == CellState.Empty ? renderer.Empty :
                renderer.Tree;

            renderer.Render(cell);

            renderer.ToString().ShouldBe(expected);
        }

    }
}
