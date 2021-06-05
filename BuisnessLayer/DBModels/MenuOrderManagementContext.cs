using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BuisnessLayer.DBModels
{
    public partial class MenuOrderManagementContext : DbContext
    {
        public MenuOrderManagementContext()
        {
        }

        public MenuOrderManagementContext(DbContextOptions<MenuOrderManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblMenu> TblMenus { get; set; }
        public virtual DbSet<TblMenuType> TblMenuTypes { get; set; }
        public virtual DbSet<TblVendorList> TblVendorLists { get; set; }

        public virtual DbSet<tblUser> tblUsers { get; set; }

        public virtual DbSet<tblRole> tblRoles { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=DESKTOP-MV64S7M\\SQLEXPRESS;Database=MenuOrderManagement;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK__tblMenu__C99ED2307583CC6C");

                entity.ToTable("tblMenu");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.MenuItem)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.MenuType)
                    .WithMany(p => p.TblMenus)
                    .HasForeignKey(d => d.MenuTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMenuType_MenuTypeId");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.TblMenus)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMenu_VendorId");
            });

            modelBuilder.Entity<TblMenuType>(entity =>
            {
                entity.HasKey(e => e.MenuTypeId)
                    .HasName("PK__tblMenuT__8E7B2D6AAD34CA44");

                entity.ToTable("tblMenuType");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MenuTypeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ImagePath).HasMaxLength(500);
            });

            modelBuilder.Entity<TblVendorList>(entity =>
            {
                entity.HasKey(e => e.VendorId)
                    .HasName("PK__tblVendo__FC8618F3EE5C5634");

                entity.ToTable("tblVendorList");

                entity.Property(e => e.VendorDescription).HasMaxLength(500);

                entity.Property(e => e.VendorImgLink).HasMaxLength(1000);

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.ToTable("tblUser");
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Nickname).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PictureLocation).HasMaxLength(2000);
                entity.HasOne<tblRole>(e => e.tblRole).WithMany(d => d.tblUsers).HasForeignKey(e => e.RoleId);

            });

            modelBuilder.Entity<tblRole>(entity => 
            {
                entity.HasKey(e => e.RoleId);
                entity.ToTable("tblRole");
                entity.Property(e => e.Rolename).IsRequired().HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
