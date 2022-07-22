using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate.Data
{
    public class PostType
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Column(TypeName ="NVARCHAR")]
        [StringLength(100)]
        [MinLength(2)]
        public string name { get; set; }
    }
}
