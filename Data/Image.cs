using System.ComponentModel.DataAnnotations;

namespace Real_Estate.Data
{

    public class Image
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string path { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
