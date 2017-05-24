namespace VideoStoreBusinessLayer
{
    public enum MovieGenre
    {
        Action,Comedy,Drama,Horror
    }
    public class Movie 
    {
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public Movie(string title,MovieGenre genre)
        {
            this.Title = title;
            this.Genre = genre;
        }
        public Movie()  { }
    }
}