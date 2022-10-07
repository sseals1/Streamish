SELECT v.Id AS VideoId, v.Title, v.Description, v.Url, 
v.DateCreated AS VideoDateCreated, v.UserProfileId As VideoUserProfileId,

                               up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
                               up.ImageUrl AS UserProfileImageUrl

                                FROM UserProfile up 
                             
                               JOIN Video v ON v.UserProfileId = up.Id
                               
                                ORDER BY  v.DateCreated

                                c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
                                LEFT JOIN Comment c on c.VideoId = v.id