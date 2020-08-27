using Microsoft.EntityFrameworkCore.Internal;
using NoteSystem.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NoteSystem.DAL
{
    internal static class RelationshipAdjuster
    {
        public static void Tune(this Notebook notebook)
        {
            if (notebook.Notes != default)
            {
                foreach (var note in notebook.Notes)
                    note.Notebook = notebook;
            }
        }

        public static void Tune(this IEnumerable<Notebook> notebooks)
        {
            foreach (var notebook in notebooks) notebook.Tune();
        }

        public static void CopyFrom(this Notebook notebook, Notebook target)
        {
            notebook.Name = target.Name;
            notebook.Notes = target.Notes;
            notebook.Changed = target.Changed;
            notebook.Created = target.Created;
        }

        public static void AddWithUniqueId<TModel>(this ICollection<TModel> modelSource, TModel model)
            where TModel : IIdentifiable
        {
            var modelAdded = false;
            var uniqueId = 1;

            do
            {
                if (modelSource.Any(n => n.Id == uniqueId))
                    uniqueId++;
                else
                {
                    model.Id = uniqueId;
                    modelAdded = true;

                    modelSource.Add(model);
                }
            }
            while (!modelAdded);
        }

        public static void ValidateModel(this Notebook notebook)
        {
            var validationContext = new ValidationContext(notebook);
            Validator.ValidateObject(notebook, validationContext);

            if (notebook.Notes != default)
            {
                foreach (var note in notebook.Notes)
                {
                    var context = new ValidationContext(note);
                    Validator.ValidateObject(note, context);
                }
            }
        }
    }
}
