using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
    public class DreamAnalysisEntity : BaseEntity
    {
        public String Tite { get; set; }
        public String EmotionalState { get; set; }
        public String Recomendation { get; set; }
        public String PatternBehaviour { get; set; }

    }
}
