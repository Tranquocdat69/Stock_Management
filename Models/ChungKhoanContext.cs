using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Stock_Management.Models
{
    public partial class ChungKhoanContext : DbContext
    {
        public ChungKhoanContext(DbContextOptions<ChungKhoanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbBangHienThi> TbBangHienThis { get; set; }
        public virtual DbSet<TbUser> TbUsers { get; set; }
        public virtual DbSet<ViewBangDienTu> ViewBangDienTus { get; set; }
        public virtual DbSet<ViewUser> ViewUsers { get; set; }

        //         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //         {
        //             if (!optionsBuilder.IsConfigured)
        //             {
        // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                 optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=ChungKhoan;Trusted_Connection=True;");
        //             }
        //         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TbBangHienThi>(entity =>
            {
                entity.HasKey(e => e.Ma)
                    .HasName("PK__tb_BangH__3214CC9FD286343B");

                entity.Property(e => e.Ma).IsUnicode(false);
            });

            modelBuilder.Entity<ViewBangDienTu>(entity =>
            {
                entity.ToView("viewBangDienTu");

                entity.Property(e => e.Ma).IsUnicode(false);
            });

            modelBuilder.Entity<ViewUser>(entity =>
            {
                entity.ToView("viewUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
