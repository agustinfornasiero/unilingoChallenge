using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using System.Web;
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

        public YouTubeController(IYouTubeAPIService youTubeAPIService)
        {
            _youTubeAPIService = youTubeAPIService;
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
    }

}

