using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
    public class DreamAnalysisEntity : BaseEntity
    {
        public string Tite { get; set; }
        public string EmotionalState { get; set; }
        public string Recomendation { get; set; }
        public string PatternBehaviour { get; set; }

    }
}
