namespace WebApp.StrategyPattern.Models
{
    public class Settings
    {
        public static string ClaimDatabaseType = "databasetype";
        public EDatabaseType DatabaseType;

        public EDatabaseType GetDefaultDatabaseType => EDatabaseType.SqlServer;
    }
}
