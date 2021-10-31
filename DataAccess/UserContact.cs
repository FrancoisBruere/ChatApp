using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserContact
    {
        [Key]
        [Required]
        public int ContactId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int UserId { get; set; }

        public int ContactBelongId { get; set; }

        [ForeignKey("UserId")]
        public User UsersContacts { get; set; }

    }
}
