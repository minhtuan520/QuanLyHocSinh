using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuanLyHocSinh.DAL.Models.Account
{
    public class Account : IdentityUser
    {
        [Key,Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool AccountType { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
