namespace CapitalReplacementTask.Models.DTOs
{
    public class CandidateDTO
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string nationality { get; set; }
        public string residence { get; set; }
        public string gender { get; set; }
        public string idNumber { get; set; }
        public List<object> answers { get; set; }
        public bool isDeleted { get; set; }
    }

    public class CandidateResultDTO
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string nationality { get; set; }
        public string residence { get; set; }
        public string gender { get; set; }
        public string idNumber { get; set; }
        public bool isDeleted { get; set; }
    }
}
