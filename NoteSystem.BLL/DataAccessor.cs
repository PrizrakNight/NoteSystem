using AutoMapper;
using NoteSystem.BLL.Dto;
using NoteSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteSystem.BLL
{
    public static class DataAccessor
    {
        public static DataAccessorConfiguration Configuration { get; set; }

        private static IMapper Mapper
        {
            get
            {
                if (_mapper == default)
                {
                    var configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Notebook, NotebookDto>().ReverseMap();
                        cfg.CreateMap<Note, NoteDto>().ReverseMap();
                    });

                    _mapper = new Mapper(configuration);
                }

                return _mapper;
            }
        }

        private static IMapper _mapper;

        public static DataAccessorConfiguration SetDefaultConfiguration(string storageName = "Notebooks")
        {
            return Configuration = new DataAccessorConfiguration().AddStorage(storageName, DataSourceType.Xml);
        }

        public static NotebookDto[] GetNotebooks(DataSourceType dataSourceType)
        {
            var dataSource = GetDataSource(dataSourceType);

            if (dataSource != default)
            {
                var models = dataSource.NotebookRepository.ToArray();

                return Mapper.Map<NotebookDto[]>(models);
            }

            throw new InvalidOperationException($"Data source '{dataSourceType}' is missing in configurations");
        }

        public static void UpdateNotebook(NotebookDto notebookDto, DataSourceType dataSourceType)
        {
            var dataSource = GetDataSource(dataSourceType);
            var notebookModel = Mapper.Map<Notebook>(notebookDto);

            dataSource.NotebookRepository.Update(notebookModel);
            dataSource.NotebookRepository.SaveChanges();
        }

        public static void UpdateNotebooks(IEnumerable<NotebookDto> notebooks, DataSourceType dataSourceType)
        {
            var dataSource = GetDataSource(dataSourceType);
            var notebookModels = _mapper.Map<Notebook[]>(notebooks);

            for (int i = 0; i < notebookModels.Length; i++)
                dataSource.NotebookRepository.Update(notebookModels[i]);

            dataSource.NotebookRepository.SaveChanges();
        }

        public static void RemoveNotebook(NotebookDto notebookDto, DataSourceType dataSourceType)
        {
            var dataSource = GetDataSource(dataSourceType);
            var notebookModel = Mapper.Map<Notebook>(notebookDto);

            dataSource.NotebookRepository.Remove(notebookModel);
            dataSource.NotebookRepository.SaveChanges();
        }

        public static void RemoveNotebooks(IEnumerable<NotebookDto> notebooks, DataSourceType dataSourceType)
        {
            var dataSource = GetDataSource(dataSourceType);
            var notebookModels = _mapper.Map<Notebook[]>(notebooks);

            for (int i = 0; i < notebookModels.Length; i++)
                dataSource.NotebookRepository.Remove(notebookModels[i]);

            dataSource.NotebookRepository.SaveChanges();
        }

        public static void AddNotebook(NotebookDto notebookDto, DataSourceType dataSourceType)
        {
            var dataSource = GetDataSource(dataSourceType);
            var notebookModel = Mapper.Map<Notebook>(notebookDto);

            dataSource.NotebookRepository.Add(notebookModel);
            dataSource.NotebookRepository.SaveChanges();

            notebookDto.Id = notebookModel.Id;
        }

        public static void AddNotebooks(IEnumerable<NotebookDto> notebooks, DataSourceType dataSourceType)
        {
            var dataSource = GetDataSource(dataSourceType);
            var notebookModels = _mapper.Map<Notebook[]>(notebooks);
            var notebooksDto = notebooks.ToArray();

            for (int i = 0; i < notebookModels.Length; i++)
                dataSource.NotebookRepository.Add(notebookModels[i]);

            dataSource.NotebookRepository.SaveChanges();

            for (int i = 0; i < notebooksDto.Length; i++)
                notebooksDto[i].Id = notebookModels[i].Id;
        }

        private static DataSource GetDataSource(DataSourceType dataSourceType)
        {
            if (Configuration == default)
                throw new InvalidOperationException("DataAccessor has no configuration");

            var dataSource = Configuration.DataSources.SingleOrDefault(source => source.SourceType == dataSourceType);

            if (dataSource != default)
                return dataSource;

            throw new InvalidOperationException($"Data source '{dataSourceType}' is missing in configurations");
        }
    }
}
