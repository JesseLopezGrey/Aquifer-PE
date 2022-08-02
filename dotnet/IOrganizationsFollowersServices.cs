using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.OrganizationFollowers;

namespace Sabio.Services.Interfaces
{
    public interface IOrganizationsFollowersServices
    {
        void Add(OrganizationFollowersRequest model);

        public Paged<OrganizationFollowers> GetByOrganizationId(int OrganizationId, int pageIndex, int pageSize);
    }

   
}