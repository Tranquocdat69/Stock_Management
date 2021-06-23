using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Stock_Management.Models
{
    [Table("tb_BangHienThi")]
    public partial class TbBangHienThi
    {
        [Key]
        [Required(ErrorMessage = "Mã không được để trống")]
        [StringLength(10)]
        public string Ma { get; set; }
        [Required]
        [Column("TC")]
        public double Tc { get; set; }
        [Required]
        public double Tran { get; set; }
        [Required]
        public double San { get; set; }
        public double? MuaG3 { get; set; }
        [Column("MuaKL3")]
        public int? MuaKl3 { get; set; }
        public double? MuaG2 { get; set; }
        [Column("MuaKL2")]
        public int? MuaKl2 { get; set; }
        public double? MuaG1 { get; set; }
        [Column("MuaKL1")]
        public int? MuaKl1 { get; set; }
        public double? KhopLenhGia { get; set; }
        [Column("KhopLenhKL")]
        public int? KhopLenhKl { get; set; }
        public double? TileTangGiam { get; set; }
        public double? BanG1 { get; set; }
        [Column("BanKL1")]
        public int? BanKl1 { get; set; }
        public double? BanG2 { get; set; }
        [Column("BanKL2")]
        public int? BanKl2 { get; set; }
        public double? BanG3 { get; set; }
        [Column("BanKL3")]
        public int? BanKl3 { get; set; }
        [Column("TongKL")]
        public int? TongKl { get; set; }
        public double? MoCua { get; set; }
        public double? CaoNhat { get; set; }
        public double? ThapNhat { get; set; }
        [Column("NNMua")]
        public int? Nnmua { get; set; }
        [Column("NNBan")]
        public int? Nnban { get; set; }
        public int? Room { get; set; }
    }
}
