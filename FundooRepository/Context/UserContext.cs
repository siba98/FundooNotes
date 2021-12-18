

namespace FundooRepository.Context
{
    using FundooModels;
    using Microsoft.EntityFrameworkCore;

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<NoteModel> Note { get; set; }
        public DbSet<CollaboratorModel> Collaborator { get; set; }
        public DbSet<LabelModel> Labels { get; set; }

    }
}
