using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace NoteSystem.DAL.Json
{
    public class JsonNotebookRepository : ICrudRepository<Notebook>
    {
        public readonly string FileStoragePath;

        private readonly ICollection<Notebook> _notebooks;
        private readonly JsonSerializerOptions _serializerOptions;

        public JsonNotebookRepository(string fileStoragePath)
        {
            _serializerOptions = new JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = true };

            FileStoragePath = fileStoragePath;

            if (File.Exists(fileStoragePath))
            {
                var jsonString = File.ReadAllText(fileStoragePath);

                _notebooks = JsonSerializer.Deserialize<HashSet<Notebook>>(jsonString, _serializerOptions);
                _notebooks.Tune();
            }
            else _notebooks = new HashSet<Notebook>();
        }

        public void Add(Notebook model)
        {
            model.ValidateModel();

            _notebooks.AddWithUniqueId(model);
        }

        public void SaveChanges()
        {
            var jsonString = JsonSerializer.Serialize(_notebooks, _serializerOptions);

            File.WriteAllText(FileStoragePath, jsonString);
        }

        public void Update(Notebook model)
        {
            model.ValidateModel();

            var finded = _notebooks.SingleOrDefault(notebook => notebook.Id == model.Id);

            if (finded != default)
            {
                finded.CopyFrom(model);
                finded.Changed = DateTime.Now;
            }
            else throw new InvalidOperationException($"Could not find model with Id '{model.Id}'");
        }

        public bool Remove(Notebook model)
        {
            var finded = _notebooks.SingleOrDefault(notebook => notebook.Id == model.Id);

            if (finded != default)
            {
                _notebooks.Remove(finded);
                return true;
            }
            return false;
        }

        public IEnumerator<Notebook> GetEnumerator() => _notebooks.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
