// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserContext.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------


namespace FundooRepository.Context
{
    using FundooModels;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// UserContext class to connect with database
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the UserContext class
        /// </summary>
        /// <param name="options">options parameter for DbContextOptions</param>
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// gets or sets in FundooNote database table Users according to (RegisterModel)
        /// </summary>
        public DbSet<RegisterModel> Users { get; set; }

        /// <summary>
        /// gets or sets in FundooNote database table Note according to (NoteModel)  
        /// </summary>
        public DbSet<NoteModel> Note { get; set; }

        /// <summary>
        /// gets or sets in FundooNote database table Collaborator according to (CollaboratorModel)  
        /// </summary>
        public DbSet<CollaboratorModel> Collaborator { get; set; }

        /// <summary>
        /// gets or sets in FundooNote database table Labels according to (LabelModel)  
        /// </summary>
        public DbSet<LabelModel> Labels { get; set; }
    }
}
