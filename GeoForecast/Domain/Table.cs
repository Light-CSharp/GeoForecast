namespace GeoForecast.Domain
{
    /// <summary>
    /// Представляет собой таблицу, хранящую набор столбцов.
    /// </summary>
    public class Table
    {
        private const int DefaultColumnCount = 4;
        private const int MinimumColumnCount = 1;

        private List<Column> columns = CreateColumns(DefaultColumnCount);

        /// <summary>
        /// Получает доступную только для чтения коллекцию столбцов.
        /// </summary>
        public IReadOnlyList<Column> Columns => columns;

        /// <summary>
        /// Получает количество строк в таблице.
        /// </summary>
        public int RowCount => columns[0].Cells.Count;

        /// <summary>
        /// Получает или устанавливает отображаемое наименование таблицы.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Добавляет новый столбец.
        /// </summary>
        public void AddColumn() => columns.Add(new Column());

        /// <summary>
        /// Удаляет столбец по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс удаляемого столбца.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, когда <paramref name="index"/> находится вне допустимого диапазона.
        /// </exception>
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

        /// <summary>
        /// Перемещает столбец на указанную позицию.
        /// </summary>
        /// <param name="currentIndex">Текущий индекс столбца.</param>
        /// <param name="newIndex">Новый индекс столбца.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, когда <paramref name="currentIndex"/> или
        /// <paramref name="newIndex"/> вышел за пределы допустимого диапазона.
        /// </exception>
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

        /// <summary>
        /// Добавляет новую строку в таблицу.
        /// </summary>
        public void AddRow()
        {
            foreach (Column column in columns)
            {
                column.AddCell();
            }
        }

        /// <summary>
        /// Удаляет строку по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс удаляемой строки.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, когда <paramref name="index"/> находится вне допустимого диапазона.
        /// </exception>
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

        /// <summary>
        /// Восстанавливает исходное состояние набора, сбрасывая столбцы до начального количества.
        /// </summary>
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