using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;
using System.Data;

namespace TMDb.Repository
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureConnectionString"].ConnectionString);
        public async Task RemoveFileAsync(Guid fileID)
        {
            connection.Open();
            var commandDelete = new SqlCommand(String.Format("DELETE FROM FileStorage WHERE FileID = '{0}'", fileID), connection);
            await commandDelete.ExecuteReaderAsync();
            connection.Close();
        }

        public async Task InsertFileAsync(string imageName, string imagePath)
        {
            var command = new SqlCommand(String.Format("INSERT INTO FileStorage(ImageName, ImagePath) VALUES('{0}','{1}')", imageName, imagePath), connection);
            connection.Open();
            await command.ExecuteReaderAsync();
            connection.Close();
            return;
        }

        public async Task<FileStorage> ReturnFileByIdAsync(Guid fileID)
        {
            FileStorage file;
            var command = new SqlCommand(String.Format("SELECT * FROM FileStorage WHERE FileID = '{0}'", fileID), connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                file = new FileStorage(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }

            reader.Close();
            connection.Close();
            return file;
        }
        public async Task UpdateFileStorageAsync(FileStorage file)
        {
            connection.Open();
            var command = new SqlCommand(String.Format("UPDATE FileStorage SET ImageName = '{0}', ImagePath = '{1}' " +
                "WHERE FileID = '{2}'", file.ImageName, file.ImagePath, file.FileID), connection);
            await command.ExecuteReaderAsync();
            connection.Close();
        }
    }
}
