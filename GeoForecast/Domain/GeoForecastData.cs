    namespace GeoForecast.Domain
    {
        public class GeoForecastData
        {
            private const int DefaultTableCount = 1;
            private const int MinimumTableCount = 1;

            private List<Table> tables = CreateTables(DefaultTableCount);

            public IReadOnlyList<Table> Tables => tables;

            public string? Name { get; set; }

            public void AddTable() => tables.Add(new Table());

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

            public void Clear() => tables = CreateTables(DefaultTableCount);
        
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