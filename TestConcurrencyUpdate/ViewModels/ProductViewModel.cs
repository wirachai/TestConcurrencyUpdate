using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestConcurrencyUpdate.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public byte[] RowVersion { get; set; }
    }
}