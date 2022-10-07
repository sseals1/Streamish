SELECT up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
up.ImageUrl AS UserProfileImageUrl,
                           
c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
                      
FROM Comment c 
JOIN UserProfile up ON c.UserProfileId = up.Id
                           
ORDER BY  c.UserProfileId