using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Organizations;
using Sabio.Models.Interfaces;
using Sabio.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class OrganizationService : IOrganizationService
    {
        IDataProvider _data = null;

        public OrganizationService(IDataProvider data)
        {
            _data = data;
        }
        public int Create(OrganizationAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Organizations_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {   

                    AddCommonParams(model, col);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;

                    int.TryParse(oId.ToString(), out id);

                    Console.WriteLine("");

                });

            return id;
        }
        public Organization GetOrganizationById(int id)
        {
            string procName = "[dbo].[Organizations_SelectById]";

            Organization organization = null; 

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set) 
            {
                int startingIndex = 0;
                organization = MapSingleOrganization(reader, ref startingIndex);
            });

            return organization;
        }
        public Organization GetOrganizationByCreatedBy(int CreatedBy, int pageIndex, int pageSize) 
        {
            string procName = "[dbo].[Organizations_Select_ByCreatedBy]";

            Organization organization = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@pageIndex", pageIndex);
                parameterCollection.AddWithValue("@pageSize", pageSize);
                parameterCollection.AddWithValue("@CreatedBy", CreatedBy);


            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                organization = MapSingleOrganization(reader, ref startingIndex);
            });

            return organization;
        }
        public Paged<Organization> GetAll(int pageIndex, int pageSize)
        {
            Paged<Organization> pagedList = null;
            List<Organization> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Organizations_SelectAll]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
            { 
            
                int startingIndex = 0;
                Organization aOrganization = MapSingleOrganization(reader, ref startingIndex);
                totalCount = reader.GetSafeInt32(startingIndex++);

                if (totalCount == 0) 
                { 
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Organization>();
                }

                list.Add(aOrganization);
            });
            if(list != null)
            {
                pagedList = new Paged<Organization>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        public void Update(OrganizationUpdateRequest model)
        {
            string procName = "[dbo].[Organizations_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                { 
                    col.AddWithValue("@Id", model.Id);
                    AddCommonParams(model, col);
                }, returnParameters: null);
        }
        public void DeleteOrganizationById(int id)
        {
            string procName = "[dbo].[Organizations_Delete_ById]";
            Organization organizationDelete = null;
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@id", id);
                }, returnParameters: null);
            Console.WriteLine(organizationDelete);
        }
        private static Organization MapSingleOrganization(IDataReader reader, ref int startingIndex)
        {
            Organization aOrganization = new Organization();
            aOrganization.OrganizationType = new LookUp();
            aOrganization.Id = reader.GetSafeInt32(startingIndex++);
            aOrganization.OrganizationType = new LookUp();
            aOrganization.OrganizationType.Id = reader.GetSafeInt32(startingIndex++);
            aOrganization.Name = reader.GetSafeString(startingIndex++);
            aOrganization.Headline = reader.GetSafeString(startingIndex++);
            aOrganization.Description = reader.GetSafeString(startingIndex++);
            aOrganization.Logo = reader.GetSafeString(startingIndex++);
            aOrganization.Location = new Location();
            aOrganization.Location.Id = reader.GetSafeInt32(startingIndex++);
            aOrganization.Location.LocationType = new LookUp();
            aOrganization.Location.LocationType.Id = reader.GetSafeInt32(startingIndex++);
            aOrganization.Location.LocationType.Name = reader.GetSafeString(startingIndex++);
            aOrganization.Location.LineOne = reader.GetSafeString(startingIndex++);
            aOrganization.Location.LineTwo = reader.GetSafeString(startingIndex++);
            aOrganization.Location.City = reader.GetSafeString(startingIndex++);
            aOrganization.Location.State = new State();
            aOrganization.Location.State.Id = reader.GetSafeInt32(startingIndex++);
            aOrganization.Location.State.Name = reader.GetSafeString(startingIndex++);
            aOrganization.Location.State.Code = reader.GetSafeString(startingIndex++);
            aOrganization.Location.Latitude = reader.GetSafeDouble(startingIndex++);
            aOrganization.Location.Longitude = reader.GetSafeDouble(startingIndex++);
            aOrganization.Phone = reader.GetSafeString(startingIndex++);
            aOrganization.SiteUrl = reader.GetSafeString(startingIndex++);
            aOrganization.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aOrganization.DateModified = reader.GetSafeDateTime(startingIndex++);
            aOrganization.CreatedBy = reader.GetSafeInt32(startingIndex++);

            return aOrganization;
        }
        private static void AddCommonParams(OrganizationAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@OrganizationTypeId", model.OrganizationTypeId);
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@Headline", model.Headline);
            col.AddWithValue("@Description", model.Description);
            col.AddWithValue("@Logo", model.Logo);
            col.AddWithValue("@LocationId", model.LocationId);
            col.AddWithValue("@Phone", model.Phone);
            col.AddWithValue("@SiteUrl", model.SiteUrl);
            col.AddWithValue("@CreatedBy", model.CreatedBy);
        }
    }
}
