using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
  public  class DreamTokenEntity : BaseEntity 
    {
        public String PatientId { get; set; }
        public int? Amount { get; set; }
        public DateTime? RrefreshDate { get; set; }

    }
}
