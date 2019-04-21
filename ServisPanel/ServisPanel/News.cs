namespace ServisPanel
{
    public class News
    {
        public string author;
        public string body;
        public string title;
        public string type;

        public News(string author, string title, string body, string type)
        {
            this.author = author;
            this.title = title;
            this.body = body;
            this.type = type;
        }
    }
}