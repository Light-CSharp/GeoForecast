using GeoForecast.Domain;

namespace GeoForecast.Tests
{
    public class TableTests
    {
        [Fact]
        public void NewTable_HasFourColumns()
        {
            Assert.Equal(4, new Table().Columns.Count);
        }

        [Fact]
        public void AddColumn_IncreasesColumnCount()
        {
            Table table = new();
            int initialCount = table.Columns.Count;

            table.AddColumn();

            Assert.Equal(initialCount + 1, table.Columns.Count);
        }

        [Fact]
        public void RemoveColumn_IndexBelowZero_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Table().RemoveColumn(-1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RemoveColumn_IndexAtOrAboveCount_Throws(int offset)
        {
            Table table = new();
            int invalidIndex = table.Columns.Count + offset;

            Assert.Throws<ArgumentOutOfRangeException>(() => table.RemoveColumn(invalidIndex));
        }

        [Fact]
        public void RemoveColumn_ReducesColumnCount()
        {
            Table table = new();
            table.AddColumn();

            int initialCount = table.Columns.Count;

            table.RemoveColumn(table.Columns.Count - 1);

            Assert.Equal(initialCount - 1, table.Columns.Count);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void MoveColumn_IndexBelowZero_Throws(int currentIndex, int newIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Table().MoveColumn(currentIndex, newIndex));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void MoveColumn_IndexAtOrAboveCount_Throws(int offset)
        {
            Table table = new();
            int invalidIndex = table.Columns.Count + offset;

            Assert.Throws<ArgumentOutOfRangeException>(() => table.MoveColumn(invalidIndex, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => table.MoveColumn(0, invalidIndex));
        }

        [Fact]
        public void MoveColumn_ChangesColumnPosition()
        {
            Table table = new();

            Column firstColumn = table.Columns[0];
            Column secondColumn = table.Columns[1];

            table.MoveColumn(0, 1);

            Assert.Same(secondColumn, table.Columns[0]);
            Assert.Same(firstColumn, table.Columns[1]);
        }

        [Fact]
        public void AddRow_IncreasesRowCount()
        {
            Table table = new();
            int initialRowCount = table.RowCount;

            table.AddRow();

            Assert.Equal(initialRowCount + 1, table.RowCount);
        }

        [Fact]
        public void RemoveRow_IndexBelowZero_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Table().RemoveRow(-1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RemoveRow_IndexAtOrAboveCount_Throws(int offset)
        {
            Table table = new();
            int invalidIndex = table.RowCount + offset;

            Assert.Throws<ArgumentOutOfRangeException>(() => table.RemoveRow(invalidIndex));
        }

        [Fact]
        public void RemoveColumn_WhenOnlyColumnExists_DoesNothing()
        {
            Table table = new();
            for (int i = table.Columns.Count - 1; i > 0; i--)
            {
                table.RemoveColumn(i);
            }

            table.RemoveColumn(0);

            Assert.Single(table.Columns);
        }

        [Fact]
        public void RemoveRow_ReducesRowCount()
        {
            Table table = new();
            int initialRowCount = table.RowCount;

            table.RemoveRow(table.RowCount - 1);

            Assert.Equal(initialRowCount - 1, table.RowCount);
        }

        [Fact]
        public void Reset_RestoresInitialColumnCount()
        {
            Table table = new();
            int initialCount = table.Columns.Count;

            table.AddColumn();
            table.AddColumn();

            table.Reset();

            Assert.Equal(initialCount, table.Columns.Count);
        }
    }
}