// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// LabelModel class contains properties for  
    /// </summary>
    public class LabelModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int LabelId { get; set; }

        [ForeignKey("NoteModel")]
        public int? NoteId { get; set; }
        public virtual NoteModel Note { get; set; }

        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }
        public virtual RegisterModel User { get; set; }
        
        public string Label { get; set; }
    }
}
