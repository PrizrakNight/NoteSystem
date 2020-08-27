using System.Collections.Generic;

namespace NoteSystem.DAL
{
    public interface ICrudRepository<TModel> : IEnumerable<TModel>
    {
        void Add(TModel model);
        void Update(TModel model);
        void SaveChanges();

        bool Remove(TModel model);
    }
}
