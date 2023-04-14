using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Acme.BookStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BookStoreDbContext :
    AbpDbContext<BookStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Book> Books { get; set; } // 於BookStore資料庫 新增 Books資料表
    public DbSet<Author> Authors { get; set; } // 於BookStore資料庫 新增 Authors資料表

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(BookStoreConsts.DbTablePrefix + "YourEntities", BookStoreConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        /* 這段Code需要問Rock or Cara 背後是執行甚麼動作 */
        // ↓↓↓
        builder.Entity<Book>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Books",  // <== Class BookStoreConsts 為 專案 BookStore定義常數的Class 
                BookStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props // 方法优雅的配置/映射继承的属性,应对所有的实体使用它 <== 不懂是甚麼意思
            b.Property(x => x.Name).IsRequired().HasMaxLength(128); // Book 的 Name 屬性 為必填 且最大長度限制為128
            b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
            // OR Mapping
            // Original / Object 
            // Related 關聯
            // Mapping 
            // 將資料庫裡的資料欄位 對照類別的屬性 動作為ORMapping 
            // Convention 慣例配置
        });

        builder.Entity<Author>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Authors",
                BookStoreConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(AuthorConsts.MaxNameLength);
            b.HasIndex(x => x.Name); // 使用Name屬性 做為索引
        });

    }
}
