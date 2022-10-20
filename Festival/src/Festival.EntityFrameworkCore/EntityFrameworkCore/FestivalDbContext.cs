using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Festival.Equipos;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Festival.Partidos;
using Festival.Apuestas;
using Festival.Predicciones;

namespace Festival.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class FestivalDbContext :
    AbpDbContext<FestivalDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<Partido> Partidos { get; set; }

    public DbSet<Apuesta> Apuestas { get; set; }

    public DbSet<Prediccion> Prediccion { get; set; }


    public FestivalDbContext(DbContextOptions<FestivalDbContext> options)
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
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(FestivalConsts.DbTablePrefix + "YourEntities", FestivalConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Equipo>(b =>
        {
            b.ToTable(FestivalConsts.DbTablePrefix + "Equipos",
                FestivalConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Pais).IsRequired().HasMaxLength(EquipoConsts.PaisMaxLength);
            b.Property(x => x.Siglas).IsRequired().HasMaxLength(EquipoConsts.SiglasMaxLength);
            b.Property(x => x.Grupo).IsRequired().HasMaxLength(EquipoConsts.GrupoMaxLength);
        });

        builder.Entity<Partido>(b =>
        {
            b.ToTable(FestivalConsts.DbTablePrefix + "Partidos",
                FestivalConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Tenant).IsRequired();
            b.Property(x => x.FechaPartido).IsRequired();
            //b.HasOne(x => x.EquipoA).WithMany().IsRequired();
            //b.HasOne(x => x.EquipoB).WithMany().IsRequired();
            //b.HasOne(p => p.EquipoA).WithMany().HasForeignKey(x => x.EquipoAId).IsRequired();
            //b.HasOne(p => p.EquipoB).WithMany().HasForeignKey(x => x.EquipoBId).IsRequired();
        });

        builder.Entity<Apuesta>(b =>
        {
            b.ToTable(FestivalConsts.DbTablePrefix + "Apuestas",
                FestivalConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Alias).IsRequired().HasMaxLength(ApuestaConsts.AliasMaxLength);
            b.Property(x => x.Tenant).IsRequired();
            b.Property(x => x.EstaPagada);
        });

        builder.Entity<Prediccion>(b =>
        {
            b.ToTable(FestivalConsts.DbTablePrefix + "Predicciones",
                FestivalConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.PrediccionResultadoEquipoA).IsRequired();
            b.Property(x => x.PrediccionResultadoEquipoB).IsRequired();
       });
    }
}
