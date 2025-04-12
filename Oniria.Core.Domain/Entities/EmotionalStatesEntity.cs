namespace Oniria.Core.Domain.Entities
{
    public class EmotionalStatesEntity : BaseEntity
    {
        public string Description { get; set; }

        // nav
        public ICollection<DreamAnalysisEntity> DreamAnalyses { get; set; }
    }
}
