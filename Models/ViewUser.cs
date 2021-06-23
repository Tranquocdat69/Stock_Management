using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Stock_Management.Models
{
    [Keyless]
    public partial class ViewUser
    {
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
    }
}
