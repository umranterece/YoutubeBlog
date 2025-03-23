namespace YoutubeBlog.Web.ResultMessages
{
    public static class Messages
    {
        public static class Article
        {
            public static string Add(string articleTitle)
            {
                return $"{articleTitle} baslikli makale basaryila eklenmistir.";
            }

            public static string Update(string articleTitle)
            {
                return $"{articleTitle} baslikli makale basaryila guncellenmistir.";
            }
            public static string Delete(string articleTitle)
            {
                return $"{articleTitle} baslikli makale basaryila silinmistir.";
            }
        }
    }
}
