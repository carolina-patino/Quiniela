using Festival.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.LanguageManagement;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas;
using Volo.Abp.OpenIddict;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Gdpr;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.LeptonTheme.Management;

namespace Festival;

[DependsOn(
    typeof(AbpAuditLoggingDomainSharedModule),
    typeof(AbpBackgroundJobsDomainSharedModule),
    typeof(AbpFeatureManagementDomainSharedModule),
    typeof(AbpIdentityProDomainSharedModule),
    typeof(AbpOpenIddictDomainSharedModule),
    typeof(AbpPermissionManagementDomainSharedModule),
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(LanguageManagementDomainSharedModule),
    typeof(SaasDomainSharedModule),
    typeof(TextTemplateManagementDomainSharedModule),
    typeof(AbpGdprDomainSharedModule),
    typeof(AbpGlobalFeaturesModule),
    typeof(BlobStoringDatabaseDomainSharedModule),
	typeof(LeptonThemeManagementDomainSharedModule)
    )]
public class FestivalDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FestivalGlobalFeatureConfigurator.Configure();
        FestivalModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FestivalDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<FestivalResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/Festival");

            options.DefaultResourceType = typeof(FestivalResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Festival", typeof(FestivalResource));
        });
    }
}
