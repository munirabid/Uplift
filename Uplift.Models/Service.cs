using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Uplift.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Service Name")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Discription")]
        public string LongDescription { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name ="Image")]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int FrequencyId { get; set; }

        [ForeignKey("FrequencyId")]
        public Frequency Frequency { get; set; }
    }
}
