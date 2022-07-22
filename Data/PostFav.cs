namespace Real_Estate.Data
{
    public class PostFav
    {
        public int id { get; set; }
        public int postId { get; set; }
        public string? email { get; set; }
        public Post? Post { get; set; }
    }
}
