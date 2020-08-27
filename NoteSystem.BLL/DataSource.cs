using Microsoft.EntityFrameworkCore;
using NoteSystem.DAL;
using NoteSystem.DAL.EfCore;
using NoteSystem.DAL.Json;
using NoteSystem.DAL.Xml;
using System;

namespace NoteSystem.BLL
{
    public enum DataSourceType { InMemory, Sqlite, Xml, Json};

    internal sealed class DataSource
    {
        public readonly DataSourceType SourceType;
        public readonly ICrudRepository<Notebook> NotebookRepository;

        public DataSource(string source, DataSourceType dataSourceType)
        {
            SourceType = dataSourceType;

            switch (dataSourceType)
            {
                case DataSourceType.Xml:
                    NotebookRepository = new XmlNotebookRepository(source);
                    break;
                case DataSourceType.Json:
                    NotebookRepository = new JsonNotebookRepository(source);
                    break;
                case DataSourceType.InMemory:

                    var inMemoryOptions = new DbContextOptionsBuilder<NoteSystemDbContext>()
                        .UseInMemoryDatabase(source)
                        .Options;

                    NotebookRepository = new EfCoreNotebookRepository(inMemoryOptions);

                    break;
                case DataSourceType.Sqlite:

                    var sqliteOptions = new DbContextOptionsBuilder<NoteSystemDbContext>()
                        .UseSqlite(source)
                        .Options;

                    NotebookRepository = new EfCoreNotebookRepository(sqliteOptions);

                    break;
                default: throw new InvalidOperationException($"Data source type '{dataSourceType}' is not defined as file");
            }
        }
    }
}
