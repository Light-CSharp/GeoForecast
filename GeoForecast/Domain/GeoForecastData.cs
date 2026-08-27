namespace GeoForecast.Domain
{
    /// <summary>
    /// Представляет собой набор таблиц геологических прогнозов.
    /// </summary>
    public class GeoForecastData
    {
        private const int DefaultTableCount = 1;
        private const int MinimumTableCount = 1;

        private List<Table> tables = CreateTables(DefaultTableCount);

        /// <summary>
        /// Получает доступную только для чтения коллекцию таблиц.
        /// </summary>
        public IReadOnlyList<Table> Tables => tables;

        /// <summary>
        /// Получает или устанавливает отображаемое наименование набора прогнозов.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Добавляет новую таблицу.
        /// </summary>
        public void AddTable() => tables.Add(new Table());

        /// <summary>
        /// Удаляет таблицу по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс удаляемой таблицы.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, когда <paramref name="index"/> находится вне допустимого диапазона.
        /// </exception>
        public void RemoveTable(int index)
        {
            if (index < 0 || index >= tables.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (tables.Count is MinimumTableCount)
            {
                return;
            }

            tables.RemoveAt(index);
        }

        /// <summary>
        /// Перемещает таблицу на указанную позицию.
        /// </summary>
        /// <param name="currentIndex">Текущий индекс таблицы.</param>
        /// <param name="newIndex">Новый индекс таблицы.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, когда <paramref name="currentIndex"/> или
        /// <paramref name="newIndex"/> вышел за пределы допустимого диапазона.
        /// </exception>
        public void MoveTable(int currentIndex, int newIndex)
        {
            if (currentIndex < 0 || currentIndex >= tables.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(currentIndex));
            }

            if (newIndex < 0 || newIndex >= tables.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(newIndex));
            }

            Table table = tables[currentIndex];

            tables.RemoveAt(currentIndex);
            tables.Insert(newIndex, table);
        }

        /// <summary>
        /// Восстанавливает исходное состояние набора, сбрасывая таблицы до начального количества.
        /// </summary>
        public void Reset() => tables = CreateTables(DefaultTableCount);

        private static List<Table> CreateTables(int count)
        {
            List<Table> tables = new(count);
            for (int i = 0; i < count; i++)
            {
                tables.Add(new Table());
            }

            return tables;
        }
    }
}