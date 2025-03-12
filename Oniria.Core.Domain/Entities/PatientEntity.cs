using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
    public class PatientEntity: BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string GenderId { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
    }
}
