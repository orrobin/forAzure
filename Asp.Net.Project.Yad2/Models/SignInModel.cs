using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Project.Yad2.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required field"), MinLength(8, ErrorMessage = "Min. of 8 characters")]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Password")]
        public string SignInPassword { get; set; }
    }
}
