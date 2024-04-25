namespace DataAccess.SqlMaps
{
    public static class GamesSql
    {
        public static string SelectAllGames()
        {
            return $@"
                SELECT
                    GameID,
                    GameDay,
                    GameFormat,
                    GameTheme,
                    GameLocation,
                    MasterFirstName,
                    MasterLastName,
                    GameCode
                FROM dbo.Game
            ";
        }

        public static string SelectGameById()
        {
            return $@"
                SELECT
                    GameID,
                    GameDay,
                    GameFormat,
                    GameTheme,
                    GameLocation,
                    MasterFirstName,
                    MasterLastName,
                    GameCode
                FROM dbo.Game
                WHERE GameID = @id
            ";
        }

        public static string UpdateGame()
        {
            return $@"
                UPDATE dbo.Game
                SET
                    GameDay = @GameDay,
                    GameFormat = @GameFormat,
                    GameTheme = @GameTheme,
                    GameLocation = @GameLocation,
                    MasterFirstName = @MasterFirstName,
                    MasterLastName = @MasterLastName,
                    GameCode = @GameCode
                WHERE GameID = @id
            ";
        }

        public static string CreateGame()
        {
            return $@"
                INSERT INTO dbo.Game
                    (
                        GameID
                        GameDay,
                        GameFormat,
                        GameTheme,
                        GameLocation,
                        MasterFirstName,
                        MasterLastName,
                        GameCode
                    )
                    VAL
                    (
                        @GameID
                        @GameDay,
                        @GameFormat,
                        @GameTheme,
                        @GameLocation,
                        @MasterFirstName,
                        @MasterLastName,
                        @GameCode
                    )
            ";
        }
    }
}
