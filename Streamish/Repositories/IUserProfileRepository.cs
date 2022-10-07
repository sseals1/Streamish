using Streamish.Models;
using System.Collections.Generic;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        void Delete(int id);
        List<UserProfile> GetAll();
        //List<UserProfile> GetAllWithComments();
        UserProfile GetUserProfileByIdWithVideos(int id);
        //UserProfile GetVideoByIdWithComments(int id);
        void Update(UserProfile userProfile);
    }
}