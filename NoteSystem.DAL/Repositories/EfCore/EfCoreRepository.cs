using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NoteSystem.DAL.EfCore
{
    public class EfCoreNotebookRepository : ICrudRepository<Notebook>, IDisposable
    {
        private readonly NoteSystemDbContext _noteSystemDbContext;

        public EfCoreNotebookRepository(DbContextOptions contextOptions)
        {
            _noteSystemDbContext = new NoteSystemDbContext(contextOptions);
        }

        public EfCoreNotebookRepository(NoteSystemDbContext noteSystemDbContext)
        {
            _noteSystemDbContext = noteSystemDbContext;
        }

        public void Add(Notebook model)
        {
            model.Id = 0;
            model.ValidateModel();

            _noteSystemDbContext.Notebooks.Add(model);
        }

        public void Update(Notebook model)
        {
            var finded = _noteSystemDbContext.Notebooks.Find(model.Id);

            if(finded != default)
            {
                finded.Name = model.Name;
                finded.Changed = model.Changed;
                finded.Created = model.Created;
                finded.Notes = model.Notes;
            }
        }

        public IEnumerator<Notebook> GetEnumerator()
        {
            return _noteSystemDbContext.Notebooks.Include(notebook => notebook.Notes).AsEnumerable().GetEnumerator();
        }

        public bool Remove(Notebook model)
        {
            _noteSystemDbContext.Entry(model).State = EntityState.Deleted;

            return true;
        }

        public void SaveChanges()
        {
            _noteSystemDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _noteSystemDbContext.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
