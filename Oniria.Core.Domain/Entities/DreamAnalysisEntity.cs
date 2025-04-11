namespace Oniria.Core.Domain.Entities
{
    public class DreamAnalysisEntity : BaseEntity
    {
        public string Title { get; set; }
        public string EmotionalState { get; set; }
        public string Recommendation { get; set; }
        public string PatternBehaviour { get; set; }

        //nav
        public EmotionalStatesEntity EmotionalStates { get; set; }
    }
}
