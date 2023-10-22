namespace LabelSongsAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewContent { get; set; }
        public string ReviewTitle { get; set;}
        public Reviewer Reviewer { get; set;}
        public Song Song { get; set; }
    }
}
