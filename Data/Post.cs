using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Real_Estate.Data
{

    public class Post
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "البريد الاليكتروني للمعلن")]
        //[EmailAddress(ErrorMessage = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? email { get; set; }

        [Display(Name = "رقم هاتف المعلن")]
        //[Phone]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; } = string.Empty;

        [Display(Name = "الصور")]
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        [NotMapped]
        public List<IFormFile>? ImagesPath { get; set; }

        [Display(Name = "الوصف")]
        public string Discription { get; set; } = string.Empty;

        [Display(Name = "نوع البائع")]
        public string SellerType { get; set; } = string.Empty;


        [Display(Name = "فئة العقار")]
        public string Category { get; set; } = string.Empty;

        [Display(Name ="نوع العقار")]
        public string PropertyType { get; set; } = string.Empty;

        [Display(Name = "الغرض")]
        public string PostFor { get; set; } = string.Empty;


        [Display(Name = "المحافظة")]
        public int govarnateId { get; set; }

        [ForeignKey("govarnateId")]
        public govarnate govarnate { get; set; } = new govarnate();

        [Display(Name = "المنطقة")]
        public int regionId { get; set; }

        [ForeignKey("regionId")]
        public region region { get; set; } = new region();

        [Display(Name = "الشارع")]
        public string Street { get; set; } = string.Empty;

        [Display(Name = "المساحة")]
        public decimal Area { get; set; }

        [Display(Name = "عدد الغرف")]
        public int Rooms { get; set; }

        [Display(Name = "حمامات")]
        public int PathRoom { get; set; }

        [Display(Name = "السعر")]
        public decimal Price { get; set; }
        [NotMapped]
        public int isFav { get; set; }
        [NotMapped]
        public int isReq { get; set; }
        [NotMapped]
        public int isAccepted { get; set; }

    }
}