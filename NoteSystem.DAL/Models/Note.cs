using NoteSystem.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace NoteSystem.DAL
{
    public class Note : IIdentifiable
    {
        [XmlIgnore]
        [JsonIgnore]
        public int Id { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public int NotebookId { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessage = "The name must be at least 3 characters long and at most 10")]
        [XmlAttribute]
        [Required]
        public string Name { get; set; }

        [StringLength(150, MinimumLength = 3, ErrorMessage = "Note text must be between 3 and 150 characters")]
        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Changed { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public Notebook Notebook { get; set; }
    }
}
