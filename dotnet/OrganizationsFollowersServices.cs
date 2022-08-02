using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Organizations;
using Sabio.Models.Requests.OrganizationFollowers;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class OrganizationsFollowersServices : IOrganizationsFollowersServices
    {
        private IDataProvider _data = null;
        private IUserProfileMapper _userProfileMapper = null;

        public OrganizationsFollowersServices(IDataProvider data, IUserProfileMapper userProfileMapper)
        {
            _data = data;
            _userProfileMapper = userProfileMapper;
        }
        public void Add(OrganizationFollowersRequest model)
        {
            string procName = "[dbo].[OrganizationFollowers_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    OrgParams(model, collection);
                },
                returnParameters: null);
        }
        public Paged<OrganizationFollowers> GetByOrganizationId(int OrganizationId, int pageIndex, int pageSize)
        {
           

            string procName = "[dbo].[OrganizationFollowers_SelectById]";
           
            List<OrganizationFollowers> list = null;
            Paged<OrganizationFollowers> pagedList = null;
            int totalCount = 0;
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
                col.AddWithValue("@OrganizationId", OrganizationId);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                OrganizationFollowers user = OrgSingleMapper(reader, ref startingIndex);//OrgSingleMapper(reader, ref startingIndex);

              if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<OrganizationFollowers>();
                }
                list.Add(user);
            }
            );
            if (list != null)
            {
                pagedList = new Paged<OrganizationFollowers>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        private static OrganizationFollowers OrgSingleMapper(IDataReader reader,  ref int startingIndex)
        {
            OrganizationFollowers orgFol = new OrganizationFollowers();
            orgFol.Organization = new Organization();
            orgFol.Follower = new UserProfileBase();
            orgFol.Organization.Id = reader.GetSafeInt32(startingIndex++);
            orgFol.Follower.UserId = reader.GetSafeInt32(startingIndex++);
            orgFol.Follower.FirstName = reader.GetSafeString(startingIndex++);
            orgFol.Follower.LastName = reader.GetSafeString(startingIndex++);
            orgFol.Follower.Mi = reader.GetSafeString(startingIndex++);
            orgFol.Follower.AvatarUrl = reader.GetSafeString(startingIndex++);
          //orgFol.Follower = _userProfileMapper.Map(reader, ref startingIndex);
            return orgFol;
        }
        private static void OrgParams(OrganizationFollowersRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@OrganizationId", model.OrganizationId);
            collection.AddWithValue("@FollowerId", model.FollowerId);
        }
    }
}
