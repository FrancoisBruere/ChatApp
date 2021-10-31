using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ChatHistory
    {
        [Key]
        [Required]
        public int ChatHistId { get; set; }

        
        public int? FromUserId { get; set; }

        [Required]
        public int ToUserId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public string ConversationId { get; set; }

        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }


        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }

        

    }
}
