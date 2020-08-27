using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace NoteSystem.DAL.Xml
{
    public class XmlNotebookRepository : ICrudRepository<Notebook>
    {
        public readonly string FileStoragePath;

        private readonly ICollection<Notebook> _notebooks;

        public XmlNotebookRepository(string filePath)
        {
            FileStoragePath = filePath;

            if (File.Exists(filePath))
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var serializer = new XmlSerializer(typeof(HashSet<Notebook>));

                    _notebooks = (HashSet<Notebook>)serializer.Deserialize(fileStream);
                    _notebooks.Tune();
                }
            }
            else _notebooks = new HashSet<Notebook>();
        }

        public void Add(Notebook model)
        {
            model.ValidateModel();

            _notebooks.AddWithUniqueId(model);
        }

        public void Update(Notebook model)
        {
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

            if(finded != default)
            {
                _notebooks.Remove(finded);
                return true;
            }
            return false;
        }

        public void SaveChanges()
        {
            using (var fileStream = File.Create(FileStoragePath))
            {
                var serializer = new XmlSerializer(typeof(HashSet<Notebook>));

                serializer.Serialize(fileStream, _notebooks);
            }
        }

        public IEnumerator<Notebook> GetEnumerator() => _notebooks.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
