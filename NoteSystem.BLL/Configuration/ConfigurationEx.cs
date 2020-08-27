namespace NoteSystem.BLL
{
    public static class ConfigurationEx
    {
        public static DataAccessorConfiguration ConfigureAll(this DataAccessorConfiguration configuration, string storageName)
        {
            return configuration
                .AddStorage($"{storageName}.json", DataSourceType.Json)
                .AddStorage($"{storageName}.xml", DataSourceType.Xml)
                .AddStorage($"{storageName}", DataSourceType.InMemory)
                .AddStorage($@"Data Source={storageName}.db;", DataSourceType.Sqlite);
        }
    }
}
