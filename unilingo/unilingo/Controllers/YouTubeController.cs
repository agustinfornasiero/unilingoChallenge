using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using unilingo.ContextDB;
using unilingo.Interfaces;
using unilingo.Model;
using unilingo.Model.Exceptions;
using unilingo.Services;

namespace unilingo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YouTubeController : ControllerBase
    {
        //Injecting the YouTubeService
        private readonly IYouTubeAPIService _youTubeAPIService;
        private readonly ApplicationDbContext _context;

        public YouTubeController(IYouTubeAPIService youTubeAPIService, ApplicationDbContext context)
        {
            _youTubeAPIService = youTubeAPIService;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetChannelVideos()
        {
            var youTubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyBwU7iggs8HOWej0JfF-GcGthl7kiMhG5A",
                ApplicationName = "YouTubeAPI"
            });

            var searchRequest = youTubeService.Search.List("snippet");
            searchRequest.ChannelId = "UCc4K7bAqpdBP8jh1j9XZAww";
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

            var searchResponse = await searchRequest.ExecuteAsync();

            return Ok(searchResponse);
        }

        [HttpGet("{videoURL}")]
        public ActionResult<string> GetVideoTitle(string videoURL)
        {
            try
            {
                string videoTitle = _youTubeAPIService.GetVideoTitle(videoURL);

                // Decode the URL parameter
                string decodedVideoURL = HttpUtility.UrlDecode(videoURL);

                // Extract video ID from the URL
                string videoId = _youTubeAPIService.ExtractVideoId(decodedVideoURL);

                if (string.IsNullOrEmpty(videoId))
                {
                    // Invalid YouTube video URL, return an error
                    return BadRequest("Invalid or empty YouTube video ID");
                }

                // Save the video title to the database
                var videoInfo = new VideoInformation
                {
                    IdVideo = videoId,
                    Title = videoTitle,
                    CreatedAt = DateTime.Now
                };

                // Your database operations here
                _context.Videos.Add(videoInfo);
                _context.SaveChanges();

                return Ok(videoTitle);
            }
            catch (VideoNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (VideoServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetMostRecentVideo")]
        public ActionResult<string> GetMostRecentVideo()
        {
            var mostRecentVideo = _context.Videos
                .OrderByDescending(v => v.CreatedAt)
                .FirstOrDefault();

            if (mostRecentVideo != null)
            {
                return Ok(mostRecentVideo.Title);
            }
            else
            {
                return NotFound("No recent videos found");
            }
        }

        //Get the ViewCount of a Video

        [HttpGet("viewCount")]
        public IActionResult GetVideoInformation(string videoURL)
        {
            try
            {
                // Decode the URL parameter
                string decodedVideoURL = HttpUtility.UrlDecode(videoURL);

                // Extract video ID from the URL
                string videoId = _youTubeAPIService.ExtractVideoId(decodedVideoURL);

                if (videoId == null)
                {
                    // Invalid YouTube video URL, return an error
                    return BadRequest("Invalid YouTube video URL");
                }

                // Fetch video information from the database
                var videoInfo = _context.Videos.FirstOrDefault(v => v.IdVideo == videoId);

                if (videoInfo == null)
                {
                    // Video not found in the database, return an error
                    return NotFound("Video not found in the database");
                }

                // Update the view count using YouTube API
                int viewCount = _youTubeAPIService.GetVideoViewCount(videoId);

                // Save the updated view count to the database (optional, depending on your requirements)
                videoInfo.ViewCount = viewCount;
                _context.SaveChanges();

                // Return the view count as a number
                return Ok(viewCount);
            }
            catch (VideoNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (VideoServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }

}

