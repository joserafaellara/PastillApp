using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PastillApp.Domain.Entities
{
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? DeviceToken { get; set; }

        [MaxLength(50)]
        public string? UserEmail { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        
        public virtual User? User { get; set; }

    }
}
