using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace Task2.Models
{
    public class AlbumModelService
    {
        SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        public int UploadAlbums(AlbumMaster objAlbumMaster)
        {
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "INSERT INTO tblGallery(ImageName,Photo) VALUES(@ImageName,@Photo)";
                scmd.Parameters.AddWithValue("@ImageName", objAlbumMaster.ImageName);
                scmd.Parameters.AddWithValue("@Photo", objAlbumMaster.Image);
                scon.Open();
                int status = scmd.ExecuteNonQuery();
                scon.Close();
                return status;
            }
        }

        public IList<AlbumMaster> GetAlbums()
        {
            List<AlbumMaster> objAlbumList = new List<AlbumMaster>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM tblGallery";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    AlbumMaster objAlbumMaster = new AlbumMaster();
                    objAlbumMaster.ImageId = Convert.ToInt32(sdr["ImageId"]);
                    objAlbumMaster.ImageName = sdr["ImageName"].ToString();
                    objAlbumMaster.Image = (byte[])sdr["Photo"];
                    objAlbumList.Add(objAlbumMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objAlbumList.ToList(); ;
            }
        }

        public byte[] GetImageFromDataBase(int id)
        {
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT Photo FROM tblGallery where ImageId=@ImageId";
                scmd.Parameters.AddWithValue("@ImageId", id);
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                AlbumMaster objAlbum = new AlbumMaster();
                while (sdr.Read())
                {
                    objAlbum.Image = (byte[])sdr["Photo"];
                }
                return objAlbum.Image;
            }
        }
    }
}