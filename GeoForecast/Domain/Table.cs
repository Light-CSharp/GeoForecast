namespace GeoForecast.Domain
{
    public class Table
    {
        private const int DefaultColumnCount = 4;
        private const int MinimumColumnCount = 1;

        private List<Column> columns = CreateColumns(DefaultColumnCount);

        public IReadOnlyList<Column> Columns => columns;

        public int RowCount => columns[0].Cells.Count;

        public string? Name { get; set; }

        public void AddColumn() => columns.Add(new Column());

        public void RemoveColumn(int index)
        {
            if (index < 0 || index >= columns.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (columns.Count is MinimumColumnCount)
            {
                return;
            }

            columns.RemoveAt(index);
        }

        public void MoveColumn(int currentIndex, int newIndex)
        {
            if (currentIndex < 0 || currentIndex >= columns.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(currentIndex));
            }

            if (newIndex < 0 || newIndex >= columns.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(newIndex));
            }

            Column column = columns[currentIndex];

            columns.RemoveAt(currentIndex);
            columns.Insert(newIndex, column);
        }

        public void AddRow()
        {
            foreach (Column column in columns)
            {
                column.AddCell();
            }
        }

        public void RemoveRow(int index)
        {
            if (index < 0 || index >= RowCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index is 0)
            {
                return;
            }

            foreach (Column column in columns)
            {
                column.RemoveCell(index);
            }
        }

        public void Reset() => columns = CreateColumns(DefaultColumnCount);

        private static List<Column> CreateColumns(int count)
        {
            List<Column> columns = new(count);
            for (int i = 0; i < count; i++)
            {
                columns.Add(new Column());
            }

            return columns;
        }
    }
}