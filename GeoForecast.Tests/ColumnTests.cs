using GeoForecast.Domain;

namespace GeoForecast.Tests
{
    public class ColumnTests
    {
        [Fact]
        public void NewColumn_HasFourCells()
        {
            Assert.Equal(4, new Column().Cells.Count);
        }

        [Fact]
        public void AddCell_IncreasesCellCount()
        {
            Column column = new();
            int initialCellCount = column.Cells.Count;

            column.AddCell();

            Assert.Equal(initialCellCount + 1, column.Cells.Count);
        }

        [Fact]
        public void RemoveCell_IndexBelowZero_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Column().RemoveCell(-1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RemoveCell_IndexAtOrAboveCount_Throws(int offset)
        {
            Column column = new();
            int invalidIndex = column.Cells.Count + offset;

            Assert.Throws<ArgumentOutOfRangeException>(() => column.RemoveCell(invalidIndex));
        }

        [Fact]
        public void RemoveCell_WhenOnlyCellExists_DoesNothing()
        {
            Column column = new();
            for (int i = column.Cells.Count - 1; i > 0; i--)
            {
                column.RemoveCell(i);
            }

            column.RemoveCell(0);

            Assert.Single(column.Cells);
        }

        [Fact]
        public void RemoveCell_ReducesCellCount()
        {
            Column column = new();
            column.AddCell();

            int initialCount = column.Cells.Count;

            column.RemoveCell(column.Cells.Count - 1);

            Assert.Equal(initialCount - 1, column.Cells.Count);
        }
    }
}