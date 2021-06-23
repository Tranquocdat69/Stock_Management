using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Stock_Management.Models
{
    [Table("tb_user")]
    public partial class TbUser
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Password { get; set; }
        [StringLength(30)]
        public string Temppass { get; set; }
    }
}
