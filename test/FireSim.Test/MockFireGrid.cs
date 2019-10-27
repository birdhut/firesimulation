using System;
using System.Collections.Generic;
using System.Text;

namespace FireSim.Test
{
    class MockFireGrid : IFireGrid
    {
        public MockFireGrid(int height, int width, bool isCompleted)
        {
            Height = height;
            Width = width;
            IsCompleted = isCompleted;
        }


        public int Height { get; }
        public int Width { get; }
        public bool IsCompleted { get; }

        public void Render(IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        public void Spread(ISpreadStrategy newSpreadStrategy = null)
        {
            throw new NotImplementedException();
        }
    }
}
