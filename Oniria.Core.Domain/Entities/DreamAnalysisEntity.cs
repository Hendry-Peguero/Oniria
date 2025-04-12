namespace Oniria.Core.Domain.Entities
{
    public class DreamAnalysisEntity : BaseEntity
    {
        public string Title { get; set; }
        public string DreamId { get; set; }
        public string EmotionalStateId { get; set; }
        public string Recommendation { get; set; }
        public string PatternBehaviour { get; set; }

        //nav
        public DreamEntity Dream { get; set; }
        public EmotionalStatesEntity EmotionalState { get; set; }
    }
}
