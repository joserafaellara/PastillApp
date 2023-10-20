using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.PastillApp.Domain.Entities
{
    public class EmergencyContactRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmergencyContactRequestId { get; set; }
        [Required]
        public int UserRequestId {  get; set; }
        public virtual User? UserRequest { get; set; }
        [Required]
        public int UserAnswerId {  get; set; }
        public bool? Accept {  get; set; }

      
   
    }
}
