namespace GeoForecast.Domain
{
    public class GeoForecastData
    {
        private readonly List<Table> tables = [new Table()];

        public IReadOnlyList<Table> Tables => tables;

        public string? Name { get; set; }

        public void AddTable() => tables.Add(new Table());

        public void RemoveTable(int index)
        {
            if (index < 0 || index >= tables.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (tables.Count is 1)
            {
                return;
            }

            tables.RemoveAt(index);
        }

        public void MoveTable(int curretnIndex, int newIndex)
        {
            if (curretnIndex < 0 || curretnIndex >= tables.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(curretnIndex));
            }

            if (newIndex < 0 || newIndex >= tables.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(newIndex));
            }

            Table table = tables[curretnIndex];

            tables.RemoveAt(curretnIndex);
            tables.Insert(newIndex, table);
        }

        public void Clear()
        {
            tables.Clear();
            tables.Add(new Table());
        }
    }
}