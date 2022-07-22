namespace Real_Estate.Data
{
    public class postRequest
    {
        public int id { get; set; }
        public string? custEmail { get; set; }
        public int postId { get; set; }
        public Post? post { get; set; }
        public int isAccept { get; set; }
    }
}
