using ContosoStore.Customers;
using ContosoStore.Merchants;
using ContosoStore.Payments;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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

namespace ContosoStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ContosoStoreDbContext : AbpDbContext<ContosoStoreDbContext>, IIdentityDbContext, ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Merchant> Merchants { get; set; }

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

    public ContosoStoreDbContext(DbContextOptions<ContosoStoreDbContext> options): base(options)
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

        builder.Entity<Payment>(b =>
        {
            b.ToTable(ContosoStoreConsts.DbTablePrefix + "Payments", ContosoStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Reference).IsRequired().HasMaxLength(128);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();
        });

        builder.Entity<Customer>(b =>
        {
            b.ToTable(ContosoStoreConsts.DbTablePrefix + "Customers", ContosoStoreConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Email).IsRequired().HasMaxLength(CustomerConsts.MaxEmailLength);
            b.Property(x => x.Name).IsRequired().HasMaxLength(CustomerConsts.MaxNameLength);
            b.HasIndex(x => x.Name);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne<Merchant>().WithMany().HasForeignKey(x => x.MerchantId).IsRequired();
        });

        builder.Entity<Merchant>(b =>
        {
            b.ToTable(ContosoStoreConsts.DbTablePrefix + "Merchants", ContosoStoreConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Email).IsRequired().HasMaxLength(MerchantConsts.MaxBusinessEmailLength);
            b.Property(x => x.BusinessName).IsRequired().HasMaxLength(MerchantConsts.MaxBusinessNameLength);
            b.HasIndex(x => x.BusinessName);
        });
    }
}
