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
    public class PhotoStoreProcedure : IPhotoRepository
    {
        private SqlConnection _sqlconnection = new SqlConnection(DataAccess.
           Properties.Settings.Default.DbConnectionString);

        public string Create(Photo photo)
        {
            var sqlCommand = new SqlCommand("CreatePhoto", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            sqlCommand.Parameters.AddWithValue("Id", photo.Id);
            sqlCommand.Parameters.AddWithValue("Url", photo.BinaryContent);
            sqlCommand.ExecuteNonQuery();
            _sqlconnection.Close();
            return "";
        }
        public void Delete(Photo photo)
        {

            var sqlCommand = new SqlCommand("DeletePhoto", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            sqlCommand.Parameters.AddWithValue("Id", photo.Id);
            sqlCommand.Parameters.AddWithValue("Url", photo.Url);

            sqlCommand.ExecuteNonQuery();
            _sqlconnection.Close();




        }

      
        public IEnumerable<Photo> ReadAll()
        {
            var photos = new List<Photo>(); // Function result
            var sqlCommand = new SqlCommand("GetAllPhotos", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                var currentPhoto = new Photo();
                currentPhoto.Id = Guid.Parse(reader["Id"].ToString());
                currentPhoto.Url = reader["Url"].ToString();
               

                photos.Add(currentPhoto);




            }
            _sqlconnection.Close();
            return photos;
        }

        public void Update(Photo photo)
        {
            var sqlCommand = new SqlCommand("UpdatePhoto", _sqlconnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlconnection.Open();
            sqlCommand.Parameters.AddWithValue("Id", photo.Id);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", photo.Url);
            sqlCommand.ExecuteNonQuery();
            _sqlconnection.Close();
        }
        public Task<string> CreateAsync(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
