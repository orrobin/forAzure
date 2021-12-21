using Entities.ModelsValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class UserModel
    {
        public long Id { get; set; }
        [Column(TypeName = "varchar(50)"), Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(50)"), Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }
        [Column(TypeName = "smalldatetime"), Required(ErrorMessage = "Required field"), BirthDateValidation(ErrorMessage = "1900 > Birthdate > Today")]
        public DateTime BirthDate { get; set; }
        [Column(TypeName = "varchar(50)"), Required(ErrorMessage = "Required field"), EmailAddress]
        public string Email { get; set; }
        [MaxLength(50), Required(ErrorMessage = "Required field")]
        public string UserName { get; set; }
        [MaxLength(50), Required(ErrorMessage = "Required field"), MinLength(8, ErrorMessage = "Min. of 8 characters")]
        public string Password { get; set; }
        [NotMapped, Compare("Password",ErrorMessage ="Password Not Match")]
        public string ValidationPassword { get; set; }
    }
}
