namespace Demo.Sql.Queries
{
    public static class BookQueries
    {
        public static string AllBook => "SELECT * FROM [Book] (NOLOCK)";

        public static string BookById => "SELECT * FROM [Book] (NOLOCK) WHERE [BookId] = @BookId";

        public static string AddBook =>
            @"INSERT INTO [Book] ([BookInfo], [LastModified]) 
				VALUES (@BookInfo, @LastModified)";

        public static string UpdateBook =>
            @"UPDATE [Book] 
            SET [BookInfo] = @BookInfo, 
				[LastModified] = @LastModified
            WHERE [BookId] = @BookId";

        public static string DeleteBook => "DELETE FROM [Book] WHERE [BookId] = @BookId";
    }
}
