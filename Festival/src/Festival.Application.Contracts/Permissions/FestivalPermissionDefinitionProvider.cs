using Festival.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Festival.Permissions;

public class FestivalPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FestivalPermissions.GroupName);

        myGroup.AddPermission(FestivalPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(FestivalPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

		var partidosPermission = myGroup.AddPermission(FestivalPermissions.Partidos.Default, L("partidos"));
        partidosPermission.AddChild(FestivalPermissions.Partidos.Create, L("Permission:CrearPartidos"));
        partidosPermission.AddChild(FestivalPermissions.Partidos.CargarResultados, L("Permission:CargarResultados"));
        partidosPermission.AddChild(FestivalPermissions.Partidos.Consultar, L("Permission:ConsultarPartidos"));

        var apuestasPermission = myGroup.AddPermission(FestivalPermissions.Apuestas.Default, L("Apuestas"));
        apuestasPermission.AddChild(FestivalPermissions.Apuestas.Create, L("Permission:CrearApuestas"));
        apuestasPermission.AddChild(FestivalPermissions.Apuestas.Edit, L("Permission:EditarApuestas"));
        apuestasPermission.AddChild(FestivalPermissions.Apuestas.Ranking, L("Permission:Ranking"));
        apuestasPermission.AddChild(FestivalPermissions.Apuestas.Delete, L("Permission:Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(FestivalPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FestivalResource>(name);
    }
}
