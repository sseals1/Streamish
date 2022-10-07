﻿using System;
using Microsoft.AspNetCore.Mvc;
using Streamish.Repositories;
using Streamish.Models;

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        [HttpGet("searchVideos")]
        public IActionResult Search(string title, bool sortDesc)
        {
            return Ok(_videoRepository.Search(title, sortDesc));
        }

        [HttpGet("hottest")]
        public IActionResult Hottest(DateTime dateCreated)
        {
            return Ok(_videoRepository.Hottest(dateCreated));
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_videoRepository.GetAll());
        }

        [HttpGet("GetAllWithComments")]
        public IActionResult GetAllWithComments()
        {
            var videos = _videoRepository.GetAllWithComments();
            return Ok(videos);


        }[HttpGet("GetVideoIdWithComments")]
        public IActionResult GetVideoByIdWithComments(int id)
        {
            var video = _videoRepository.GetVideoByIdWithComments(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _videoRepository.GetById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpPost]
        public IActionResult Post(Video video)
        {
            _videoRepository.Add(video);
            return CreatedAtAction("Get", new { id = video.Id }, video);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            _videoRepository.Update(video);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _videoRepository.Delete(id);
            return NoContent();
        }
    }
}