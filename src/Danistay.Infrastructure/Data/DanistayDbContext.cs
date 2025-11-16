using Microsoft.EntityFrameworkCore;
using Danistay.Domain.Entities;
using Danistay.Domain.Enums;

namespace Danistay.Infrastructure.Data
{
    public class DanistayDbContext : DbContext
    {
        public DanistayDbContext(DbContextOptions<DanistayDbContext> options) : base(options)
        {
        }
        
        // DbSets - Tablolar
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentAction> DocumentActions { get; set; }
        
        // E-Commerce DbSets
        public DbSet<Store> Stores { get; set; }
        public DbSet<PlatformIntegration> PlatformIntegrations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPlatform> ProductPlatforms { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                
                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsRequired();
                    
                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsRequired();
                    
                entity.Property(e => e.PasswordHash)
                    .IsRequired();
            });
            
            // Document Configuration
            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsRequired();
                    
                entity.Property(e => e.Description)
                    .HasMaxLength(1000);
                    
                entity.Property(e => e.DocumentType)
                    .HasConversion<int>()
                    .IsRequired();
                    
                entity.Property(e => e.Status)
                    .HasConversion<int>()
                    .IsRequired()
                    .HasDefaultValue(DocumentStatus.Draft);
                    
                entity.Property(e => e.FilePath)
                    .HasMaxLength(500);
                    
                entity.Property(e => e.FileExtension)
                    .HasMaxLength(100);
                    
                // Foreign Key Relationships
                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(u => u.CreatedDocuments)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(d => d.UpdatedByUser)
                    .WithMany(u => u.UpdatedDocuments)
                    .HasForeignKey(d => d.UpdatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // DocumentAction Configuration
            modelBuilder.Entity<DocumentAction>(entity =>
            {
                entity.Property(e => e.ActionType)
                    .HasConversion<int>()
                    .IsRequired();
                    
                entity.Property(e => e.Notes)
                    .HasMaxLength(500);
                    
                entity.Property(e => e.ActionDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                    
                // Foreign Key Relationships
                entity.HasOne(da => da.Document)
                    .WithMany(d => d.DocumentActions)
                    .HasForeignKey(da => da.DocumentId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(da => da.User)
                    .WithMany(u => u.DocumentActions)
                    .HasForeignKey(da => da.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // Store Configuration
            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired();
                    
                entity.HasOne(s => s.User)
                    .WithMany()
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // PlatformIntegration Configuration
            modelBuilder.Entity<PlatformIntegration>(entity =>
            {
                entity.Property(e => e.PlatformType)
                    .HasConversion<int>()
                    .IsRequired();
                    
                entity.Property(e => e.LastSyncStatus)
                    .HasConversion<int>()
                    .HasDefaultValue(SyncStatus.Pending);
                    
                entity.HasOne(pi => pi.Store)
                    .WithMany(s => s.PlatformIntegrations)
                    .HasForeignKey(pi => pi.StoreId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired();
                    
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // Product Configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .IsRequired();
                    
                entity.Property(e => e.BasePrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                entity.Property(e => e.CostPrice)
                    .HasColumnType("decimal(18,2)");
                    
                entity.Property(e => e.StockStatus)
                    .HasConversion<int>()
                    .HasDefaultValue(StockStatus.InStock);
                    
                entity.Property(e => e.PriceStrategy)
                    .HasConversion<int>()
                    .HasDefaultValue(PriceStrategy.Fixed);
                    
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(p => p.Store)
                    .WithMany(s => s.Products)
                    .HasForeignKey(p => p.StoreId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // ProductPlatform Configuration
            modelBuilder.Entity<ProductPlatform>(entity =>
            {
                entity.Property(e => e.PlatformPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                entity.HasOne(pp => pp.Product)
                    .WithMany(p => p.ProductPlatforms)
                    .HasForeignKey(pp => pp.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(pp => pp.PlatformIntegration)
                    .WithMany(pi => pi.ProductPlatforms)
                    .HasForeignKey(pp => pp.PlatformIntegrationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // ProductImage Configuration
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasOne(pi => pi.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(pi => pi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // Order Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsRequired();
                    
                entity.Property(e => e.Status)
                    .HasConversion<int>()
                    .HasDefaultValue(OrderStatus.Pending);
                    
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                entity.Property(e => e.ShippingAmount)
                    .HasColumnType("decimal(18,2)");
                    
                entity.Property(e => e.DiscountAmount)
                    .HasColumnType("decimal(18,2)");
                    
                entity.Property(e => e.TaxAmount)
                    .HasColumnType("decimal(18,2)");
                    
                entity.HasOne(o => o.PlatformIntegration)
                    .WithMany(pi => pi.Orders)
                    .HasForeignKey(o => o.PlatformIntegrationId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasIndex(e => e.OrderNumber).IsUnique();
            });
            
            // OrderItem Configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                entity.Property(e => e.DiscountAmount)
                    .HasColumnType("decimal(18,2)");
                    
                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // StockHistory Configuration
            modelBuilder.Entity<StockHistory>(entity =>
            {
                entity.HasOne(sh => sh.Product)
                    .WithMany(p => p.StockHistories)
                    .HasForeignKey(sh => sh.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // PriceHistory Configuration
            modelBuilder.Entity<PriceHistory>(entity =>
            {
                entity.Property(e => e.PreviousPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                entity.Property(e => e.NewPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                    
                entity.HasOne(ph => ph.Product)
                    .WithMany(p => p.PriceHistories)
                    .HasForeignKey(ph => ph.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // Seed Data
            SeedData(modelBuilder);
        }
        
        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Admin User - şifre: admin123
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@danistay.gov.tr",
                    FirstName = "Admin",
                    LastName = "User",
                    PasswordHash = "$2a$11$nOUIs5kJ7naTuTFkBy1veuK5kVz1uj2.LGnc.0C0N2k7kZU7lJaHK", // admin123
                    Department = "Bilgi İşlem",
                    Position = "Sistem Yöneticisi",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new User
                {
                    Id = 2,
                    Username = "user1",
                    Email = "user1@danistay.gov.tr",
                    FirstName = "Ahmet",
                    LastName = "Yılmaz",
                    PasswordHash = "$2a$11$6BVvsQQK3w34o67dIFjFaOKfOqw3YBZshHjqPDXgTG4T2UdEEINXG", // user123
                    Department = "Hukuk İşleri",
                    Position = "Hukuk Müşaviri",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "admin"
                }
            );
            
            // Sample Documents
            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    Id = 1,
                    Title = "2025/1 Sayılı Daire Kararı",
                    Description = "İdari yargı uygulamasına ilişkin önemli karar",
                    DocumentType = DocumentType.Karar,
                    Status = DocumentStatus.Published,
                    CreatedByUserId = 1,
                    CreatedAt = new DateTime(2025, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "admin"
                },
                new Document
                {
                    Id = 2,
                    Title = "Personel Duyurusu - Yıl Sonu Tatili",
                    Description = "2025 yıl sonu tatil günleri hakkında duyuru",
                    DocumentType = DocumentType.Duyuru,
                    Status = DocumentStatus.Pending,
                    CreatedByUserId = 2,
                    CreatedAt = new DateTime(2025, 2, 1, 14, 30, 0, DateTimeKind.Utc),
                    CreatedBy = "user1"
                }
            );
        }
        
        // Audit için override
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
                
            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;
                
                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
