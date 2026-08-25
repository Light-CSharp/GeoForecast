using GeoForecast.Domain;

namespace GeoForecast.Tests
{
    public class GeoForecastDataTests
    {
        [Fact]
        public void NewGeoForecastData_HasOneTable()
        {
            Assert.Single(new GeoForecastData().Tables);
        }

        [Fact]
        public void AddTable_IncreasesTableCount()
        {
            GeoForecastData geoForecastData = new();
            int initialTableCount = geoForecastData.Tables.Count;

            geoForecastData.AddTable();

            Assert.Equal(initialTableCount + 1, geoForecastData.Tables.Count);
        }

        [Fact]
        public void RemoveTable_IndexBelowZero_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GeoForecastData().RemoveTable(-1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RemoveTable_IndexAtOrAboveCount_Throws(int offset)
        {
            GeoForecastData geoForecastData = new();
            int invalidIndex = geoForecastData.Tables.Count + offset;

            Assert.Throws<ArgumentOutOfRangeException>(() => geoForecastData.RemoveTable(invalidIndex));
        }

        [Fact]
        public void RemoveTable_WhenOnlyTableExists_DoesNothing()
        {
            GeoForecastData geoForecastData = new();
            geoForecastData.RemoveTable(0);

            Assert.Single(geoForecastData.Tables);
        }

        [Fact]
        public void RemoveTable_ReducesTableCount()
        {
            GeoForecastData geoForecastData = new();
            geoForecastData.AddTable();

            int initialCount = geoForecastData.Tables.Count;

            geoForecastData.RemoveTable(geoForecastData.Tables.Count - 1);

            Assert.Equal(initialCount - 1, geoForecastData.Tables.Count);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void MoveTable_IndexBelowZero_Throws(int currentIndex, int newIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GeoForecastData().MoveTable(currentIndex, newIndex));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void MoveTable_IndexAtOrAboveCount_Throws(int offset)
        {
            GeoForecastData geoForecastData = new();
            int invalidIndex = geoForecastData.Tables.Count + offset;

            Assert.Throws<ArgumentOutOfRangeException>(() => geoForecastData.MoveTable(invalidIndex, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => geoForecastData.MoveTable(0, invalidIndex));
        }

        [Fact]
        public void MoveTable_ChangesTablePosition()
        {
            GeoForecastData geoForecastData = new();
            geoForecastData.AddTable();

            Table firstTable = geoForecastData.Tables[0];
            Table secondTable = geoForecastData.Tables[1];

            geoForecastData.MoveTable(0, 1);

            Assert.Same(secondTable, geoForecastData.Tables[0]);
            Assert.Same(firstTable, geoForecastData.Tables[1]);
        }

        [Fact]
        public void Reset_RestoresInitialTableCount()
        {
            GeoForecastData geoForecastData = new();
            int initialCount = geoForecastData.Tables.Count;

            geoForecastData.AddTable();
            geoForecastData.AddTable();

            geoForecastData.Reset();

            Assert.Equal(initialCount, geoForecastData.Tables.Count);
        }
    }
}