namespace GeoForecast.Domain
{
    /// <summary>
    /// Представляет собой столбец таблицы, состоящий из набора ячеек.
    /// </summary>
    public class Column
    {
        private const int DefaultCellCount = 4;
        private const int MinimumCellCount = 1;

        private readonly List<Cell> cells = CreateCells(DefaultCellCount);

        /// <summary>
        /// Получает доступную только для чтения коллекцию ячеек.
        /// </summary>
        public IReadOnlyList<Cell> Cells => cells;

        /// <summary>
        /// Получает или устанавливает отображаемое наименование столбца.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Добавляет новую ячейку.
        /// </summary>
        internal void AddCell() => cells.Add(new Cell());

        /// <summary>
        /// Удаляет ячейку по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс удаляемой ячейки.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, когда <paramref name="index"/> находится вне допустимого диапазона.
        /// </exception>
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