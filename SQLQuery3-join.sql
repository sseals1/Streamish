INSERT INTO UserProfile (Name, Email, ImageUrl, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES ('Joe', 'Email', 'ImageUrl', 10/05/22)