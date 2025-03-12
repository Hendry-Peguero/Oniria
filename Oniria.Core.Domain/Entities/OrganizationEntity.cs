using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
  public class OrganizationEntity : BaseEntity 
    {
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber  { get; set; }

        public String EmployeeOwnerld { get; set; }
    }
}
