using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DataAccess.Repositories
{
    public class ProfileStoredProcedureRepository : IProfileRepository
    {
        private SqlConnection _sqlconnection = new SqlConnection(DataAccess.
           Properties.Settings.Default.DbConnectionString);

        public void Create(Profile profile)
        {
            var sqlCommand = new SqlCommand("CreateProfile", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            sqlCommand.Parameters.AddWithValue("Id", profile.Id);
            sqlCommand.Parameters.AddWithValue("Name", profile.Name);
            sqlCommand.Parameters.AddWithValue("Birthday", profile.Birthday);
            sqlCommand.Parameters.AddWithValue("Adress", profile.Address);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", profile.PhotoUrl);
            sqlCommand.Parameters.AddWithValue("Country", profile.Country);
            sqlCommand.ExecuteNonQuery();
            _sqlconnection.Close();
        }

        public void Delete(Guid post)
        {

            var sqlCommand = new SqlCommand("DeleteProfile", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            sqlCommand.Parameters.AddWithValue("Id", post);

            sqlCommand.ExecuteNonQuery();
            _sqlconnection.Close();




        }

        public Profile Read(Guid post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Profile> ReadAll()
        {
            var profiles = new List<Profile>(); // Function result
            var sqlCommand = new SqlCommand("GetAllProfiles", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                var currentProfile = new Profile();
                currentProfile.Id = Guid.Parse(reader["Id"].ToString());
                currentProfile.Name = reader["Name"].ToString();
                currentProfile.Birthday = DateTime.Parse(reader["PhotoUrl"].ToString());
                
                profiles.Add(currentProfile);




            }
            _sqlconnection.Close();
            return profiles;
        }

        public void Update(Profile profile)
        {
            var sqlCommand = new SqlCommand("UpdateProfile", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            sqlCommand.Parameters.AddWithValue("Id", profile.Id);
            sqlCommand.Parameters.AddWithValue("Name", profile.Name);
            sqlCommand.Parameters.AddWithValue("Birthday", profile.Birthday);
            sqlCommand.Parameters.AddWithValue("Adress", profile.Address);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", profile.PhotoUrl);
            sqlCommand.Parameters.AddWithValue("Country", profile.Country);
            sqlCommand.ExecuteNonQuery();
            _sqlconnection.Close();
        }
    }
}
