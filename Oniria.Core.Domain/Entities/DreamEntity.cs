namespace Oniria.Core.Domain.Entities
{
    public class DreamEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string PatientId { get; set; }
        public string DreamAnalysisId { get; set; }

        //nav
        public PatientEntity Patient { get; set; }
        public DreamAnalysisEntity DreamAnalysis { get; set; }
    }
}
