using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class CollaboratorModel
    {
        [Key]
        public int CollaboratorId { get; set; }

        [ForeignKey("NoteModel")]
        public int NoteId { get; set; }
        public virtual NoteModel note { get; set; }
        public string Email { get; set; }
    }
}
