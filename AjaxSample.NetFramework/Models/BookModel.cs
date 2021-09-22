using System.Data.Entity;

#nullable disable

namespace AjaxSample.NetFramework.Models
{
    public partial class BookModel : DbContext
    {
        public BookModel() : base("name=BookModel")
        {}

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                        .HasMany(e => e.Books)
                        .WithMany(e => e.Authors)
                        .Map(m => m.ToTable("Writing").MapLeftKey("AuthorId").MapRightKey("BookCode"));

            modelBuilder.Entity<Book>()
                        .Property(e => e.Code)
                        .IsFixedLength()
                        .IsUnicode(false);

            modelBuilder.Entity<Book>()
                        .Property(e => e.PublisherCode)
                        .IsUnicode(false);

            modelBuilder.Entity<Publisher>()
                        .Property(e => e.Code)
                        .IsUnicode(false);
        }
    }
}
