namespace DataAccess.SqlMaps
{
    public static class GamesSql
    {
        public static string SelectGames()
        {
            return $@"
                SELECT
                    G.GameID,
                    G.GameDay,
                    G.GameFormat,
                    G.GameTheme,
                    G.GameLocation,
                    G.MasterFirstName,
                    G.MasterLastName,
                    G.GameCode
                FROM dbo.Game G
            ";
        }

        public static string SelectGameById()
        {
            return $@"
                SELECT
                    G.GameID,
                    G.GameDay,
                    G.GameFormat,
                    G.GameTheme,
                    G.GameLocation,
                    G.MasterFirstName,
                    G.MasterLastName,
                    G.GameCode
                FROM dbo.Game G
                WHERE G.GameID = @id
            ";
        }
    }
}
