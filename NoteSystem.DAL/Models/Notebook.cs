using NoteSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace NoteSystem.DAL
{
    public class Notebook : IIdentifiable
    {
        [Required(ErrorMessage = "Notebook must contain an identifier")]
        [XmlAttribute]
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessage = "The name must be at least 3 characters long and at most 10")]
        [XmlAttribute]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Changed { get; set; }

        public List<Note> Notes { get; set; }
    }
}
