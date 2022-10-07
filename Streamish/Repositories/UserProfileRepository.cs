using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;

namespace Streamish.Repositories
{

    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Email, ImageUrl, DateCreated 
                        FROM UserProfile
                        Order By DateCreated
                       ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        var userProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            userProfiles.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),


                            });
                        }

                        return userProfiles;
                    }
                }
            }
        }



        public UserProfile GetUserProfileByIdWithVideos(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT v.Id AS VideoId, v.Title, v.Description, v.Url, 
                       v.DateCreated AS VideoDateCreated, v.UserProfileId As VideoUserProfileId,

                       up.Id, up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                        
                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId

                        FROM UserProfile up

                       JOIN  Video v ON v.UserProfileId = up.Id
                       LEFT JOIN Comment c on c.VideoId = v.Id
                        WHERE up.Id = @Id
                        ORDER BY  v.DateCreated
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        UserProfile userProfile = null;
                        while (reader.Read())
                        {


                            if (userProfile == null)
                            {
                                userProfile = new UserProfile()
                                {
                                    Id = id,
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated"),
                                    ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                                    Videos = new List<Video>()
                                };

                            };
                            var existingVideoId = DbUtils.GetInt(reader, "VideoId");
                            var existingVideo = userProfile.Videos.Find(v => v.Id == existingVideoId);

                            if (existingVideo == null)
                            {
                                existingVideo = new Video()
                                {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                                    UserProfileId = DbUtils.GetInt(reader, "VideoUserProfileId"),
                                    Comments = new List<Comment>()
                                };
                                userProfile.Videos.Add(existingVideo);

                            };
                            if (DbUtils.IsNotDbNull(reader, "CommentId"))
                            {
                                existingVideo.Comments.Add(new Comment
                                {
                                    Id = DbUtils.GetInt(reader, "CommentId"),
                                    Message = DbUtils.GetString(reader, "Message"),
                                    VideoId = DbUtils.GetInt(reader, "VideoId"),
                                    UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
                                });

                            }
                        }

                        return userProfile;

                    }

                }
            }
        }





        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Name, Email, ImageUrl, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Email, @DateCreated, @ImageUrl)";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }



        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Email = @Email,
                               DateCreated = @DateCreated,
                               ImageUrl = @ImageUrl,                               
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", userProfile.Id);
                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);

                    cmd.ExecuteNonQuery();
                }
            }
        }




        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Video WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

