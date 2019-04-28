namespace ServisPanel
{
    public class News
    {
        public string author;
        public string body;
        public string title;
        public string type;
        public long publicationDate;
        public byte[] image;

        public News(string author, string title, string body, string type, long publicationDate, byte[] image)
        {
            this.author = author;
            this.title = title;
            this.body = body;
            this.type = type;
            this.publicationDate = publicationDate;
            this.image = image;
        }
    }
}