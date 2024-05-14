namespace CapitalReplacementTask.Models.DTOs
{
    public class QuestionsDTO
    {
        public string Id { get; set; }
        public string TypeId { get; set; }
        public string QuestionText { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; }
    }


    public class QuestionTypeDTO
    {
        public string Id { get; set; }
        public string CandidateId { get; set; }
        public string NameOfType { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; }
    }

   
    
}
