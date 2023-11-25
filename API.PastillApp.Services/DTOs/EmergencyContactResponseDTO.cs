namespace API.PastillApp.Services.DTOs
{
    public class EmergencyContactResponseDTO
    {
        public string UserMail { get; set; }
        public int EmergencyRequestId { get; set; }
        public bool Accept {  get; set; }
    }
}
