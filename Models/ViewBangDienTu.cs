using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Stock_Management.Models
{
    [Keyless]
    public partial class ViewBangDienTu
    {
        [Required]
        [StringLength(10)]
        public string Ma { get; set; }
        [Column("TC")]
        public double Tc { get; set; }
        public double Tran { get; set; }
        public double San { get; set; }
    }
}
