using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
  public class DreamEntity : BaseEntity
    {
        public String Tite { get; set; }
        public String Prompt { get; set; }
        public String PatientId { get; set; }
        public String DreamAnalysisId { get; set; }
    }
}
