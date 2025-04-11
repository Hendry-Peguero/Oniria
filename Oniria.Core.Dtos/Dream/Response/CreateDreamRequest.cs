namespace Oniria.Core.Dtos.Dream.Request
{
    public class CreateDreamRequest
    {
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string PatientId { get; set; }
        public string DreamAnalysisId { get; set; }
    }
}
