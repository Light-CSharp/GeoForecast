namespace GeoForecast.Domain
{
    public class Column
    {
        private const int DefaultCellCount = 4;
        private const int MinimumCellCount = 1;

        private readonly List<Cell> cells = CreateCells(DefaultCellCount);

        public IReadOnlyList<Cell> Cells => cells;

        public string? Name { get; set; }

        internal void AddCell() => cells.Add(new Cell());

        internal void RemoveCell(int index)
        {
            if (index < 0 || index >= cells.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (cells.Count is MinimumCellCount)
            {
                return;
            }

            cells.RemoveAt(index);
        }

        private static List<Cell> CreateCells(int count)
        {
            List<Cell> cells = new(count);
            for (int i = 0; i < count; i++)
            {
                cells.Add(new Cell());
            }

            return cells;
        }
    }
}