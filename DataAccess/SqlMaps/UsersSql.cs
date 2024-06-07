namespace DataAccess.SqlMaps
{
    public static class UsersSql
    {
        public static string InsertUser()
        {
            return $@"
                INSERT INTO dbo.Users 
                    (
                        Username, 
                        PasswordHash, 
                        Email
                    )
                    VALUES
                    (
                        @Username,
                        @PasswordHash,
                        @Email
                    );
            ";
        }

        public static string SelectUserByUsername()
        {
            return $@"
                SELECT
                    *
                FROM dbo.Users
                WHERE Username = @Username
            ";
        }
    }
}
