namespace Festival.Permissions;

public static class FestivalPermissions
{
    public const string GroupName = "Festival";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }
	
	public static class Partidos
    {
        public const string Default = GroupName + ".Partidos";
        public const string Create = Default + ".Create";
        public const string CargarResultados = Default + ".CargarResultados";
        public const string Consultar = Default + ".Consultar";
    }

    public static class Apuestas
    {
        public const string Default = GroupName + ".Apuestas";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Ranking = Default + ".Ranking";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
