using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
  public  class DreamTokenEntity : BaseEntity 
    {
        String PatientId { get; set; }
        int? Amount { get; set; }
        DateTime? RrefreshDate { get; set; }

    }
}
