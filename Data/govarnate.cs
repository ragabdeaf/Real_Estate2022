using System.ComponentModel.DataAnnotations;

namespace Real_Estate.Data
{
    public class govarnate
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string? Name { get; set; } = string.Empty;

        public ICollection<Post>? Posts { get; set; }


    }
}
