using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteSystem.BLL
{
    public class DataAccessorConfiguration
    {
        public DataSourceType[] SourceTypes => DataSources.Select(source => source.SourceType).ToArray();

        internal readonly ICollection<DataSource> DataSources = new HashSet<DataSource>();

        public DataAccessorConfiguration AddStorage(string source, DataSourceType dataSourceType)
        {
            if (!DataSources.Any(dataSource => dataSource.SourceType == dataSourceType))
            {
                DataSources.Add(new DataSource(source, dataSourceType));
                return this;
            }

            throw new InvalidOperationException("This type of data source has already been added to the configuration");
        }

        public DataAccessorConfiguration RemoveStorage(DataSourceType dataSourceType)
        {
            var finded = DataSources.SingleOrDefault(source => source.SourceType == dataSourceType);

            if (finded != default)
            {
                DataSources.Remove(finded);
                return this;
            }

            throw new KeyNotFoundException("Could not find data source of specified type: " + dataSourceType);
        }
    }
}
