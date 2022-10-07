using System;
using Microsoft.AspNetCore.Mvc;
using Streamish.Repositories;
using Streamish.Models;

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }

        //[HttpGet("GetAllWithComments")]
        //public IActionResult GetAllWithComments()
        //{
        //    var userProfiles = _userProfileRepository.GetAllWithComments();
        //    return Ok(userProfiles);


        //    }
        //    [HttpGet("GetVideoIdWithComments")]
        //    public IActionResult GetVideoByIdWithComments(int id)
        //    {
        //        var video = _videoRepository.GetVideoByIdWithComments(id);
        //        if (video == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(video);
        //    }


        [HttpGet("GetUserProfileByIdWithVideos/{id}")]
        public IActionResult Get(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileByIdWithVideos(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        [HttpPost]
        public IActionResult Post(UserProfile userProfile)
        {
            userProfile.DateCreated = DateTime.Now;
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction("Get", new { id = userProfile.Id }, userProfile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(userProfile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
    }
}

