using Sabio.Models.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
  public class OrganizationFollowers
    {
       public Organization Organization { get; set; }

        public UserProfileBase Follower { get; set; }

    }
}
