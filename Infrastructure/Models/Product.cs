﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "MSRP List Price")]
        [Range(00.01, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(00.01, 10000)]
        [Display(Name = "Price Qty 1-5")]
        public double UnitPrice { get; set; }

        [Required]
        [Range(00.01, 10000)]
        [Display(Name = "Price Qty 6-11")]
        public double HalfDozenPrice { get; set; }

        [Required]
        [Range(00.01, 10000)]
        [Display(Name = "Price Qty 12 or More")]
        public double DozenPrice { get; set; }

        [Required]
        public string? Size { get; set; }

        [Required]
        public string? UPC { get; set; }

        public string? ImageURL { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }

        //Navigation Properties

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer? Manufacturer { get; set; }
    }
}