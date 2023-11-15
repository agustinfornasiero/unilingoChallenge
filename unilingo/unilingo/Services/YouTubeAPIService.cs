using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Web;
using unilingo.Interfaces;
using unilingo.Model.Exceptions;

namespace unilingo.Services
{
    public class YouTubeAPIService : IYouTubeAPIService
    {
        private readonly YouTubeService _youTubeService;
        private readonly string _apiKey;
        //private readonly string _apiKey = configuration["YouTubeAPI:ApiKey"];
        private readonly string _applicationName = "YouTubeAPI";
        
        public YouTubeAPIService(IConfiguration configuration)
        {
            _apiKey = configuration["YouTubeAPI:ApiKey"];
            _youTubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = _apiKey,
                ApplicationName = _applicationName
            });
        }

        public string GetVideoTitle(string videoURL)
        {
            try
            {
                string youtubeVideoUrl = videoURL;

                // Decode the URL parameter
                string decodedVideoURL = HttpUtility.UrlDecode(videoURL);

                // Log the decoded URL
                Console.WriteLine($"Decoded Video URL: {decodedVideoURL}");

                // Extract video ID from the URL
                string videoId = ExtractVideoId(decodedVideoURL);

                if (videoId != null)
                {
                    // Create a new YouTubeService
                    var youtubeService = _youTubeService;

                    // Create a videos request
                    var videosRequest = youtubeService.Videos.List("snippet,statistics");
                    videosRequest.Id = videoId;

                    // Execute the request and retrieve the video details
                    var videoResponse = videosRequest.Execute();

                    // Check if there are items in the response
                    if (videoResponse.Items != null && videoResponse.Items.Count > 0)
                    {
                        // Extract the video title from the response
                        string videoTitle = videoResponse.Items[0].Snippet.Title;
                        return videoTitle;
                    }
                    else
                    {
                        // No items found in the response, handle accordingly
                        throw new VideoNotFoundException("Video not found");
                    }
                }
                else
                {
                    // Invalid YouTube video URL, return a default or empty string
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new VideoServiceException($"Error: {ex.Message}");
            }
        }

        // Function to extract video ID from YouTube video URL
        //public string ExtractVideoId(string url)
        //{
        //    try
        //    {
        //        var uri = new Uri(url);

        //        // Attempt to get video ID from the query string
        //        var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
        //        string videoId = queryParams["v"];

        //        // If the video ID is not found in the query string, try to extract it from the path
        //        if (string.IsNullOrEmpty(videoId))
        //        {
        //            // Example: https://www.youtube.com/watch?v=8UVNT4wvIGY
        //            // Extract video ID from the path if it follows the "/watch?v=" pattern
        //            var segments = uri.Segments;
        //            for (int i = 0; i < segments.Length; i++)
        //            {
        //                if (segments[i].Equals("watch/", StringComparison.OrdinalIgnoreCase) && i + 1 < segments.Length)
        //                {
        //                    videoId = segments[i + 1];
        //                    break;
        //                }
        //            }
        //        }

        //        return videoId;
        //    }
        //    catch (UriFormatException)
        //    {
        //        return null;
        //    }
        //}
        //public string ExtractVideoId(string url)
        //{
        //    try
        //    {
        //        var uri = new Uri(url);

        //        // Attempt to get video ID from the query string
        //        var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
        //        string videoId = queryParams["v"];

        //        // If the video ID is not found in the query string, try to extract it from the path
        //        if (string.IsNullOrEmpty(videoId))
        //        {
        //            // Extract video ID from the path
        //            var segments = uri.Segments;
        //            foreach (var segment in segments)
        //            {
        //                // Check for common patterns in the URL path
        //                if (segment.StartsWith("watch/", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    videoId = segment.Trim('/').Substring("watch/".Length);
        //                    break;
        //                }
        //                else if (segment.StartsWith("embed/", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    videoId = segment.Trim('/').Substring("embed/".Length);
        //                    break;
        //                }
        //                else if (segment.StartsWith("youtu.be/", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    videoId = segment.Trim('/').Substring("youtu.be/".Length);
        //                    break;
        //                }
        //            }
        //        }

        //        return videoId;
        //    }
        //    catch (UriFormatException)
        //    {
        //        return null;
        //    }
        //}

        public string ExtractVideoId(string url)
        {
            try
            {
                var uri = new Uri(url);

                // Attempt to get video ID from the query string
                var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
                string videoId = queryParams["v"];

                if (!string.IsNullOrEmpty(videoId))
                {
                    return videoId;
                }

                // If the video ID is not found in the query string, try to extract it from the path
                var segments = uri.Segments;
                foreach (var segment in segments)
                {
                    // Check for common patterns in the URL path
                    if (segment.StartsWith("watch/", StringComparison.OrdinalIgnoreCase) ||
                        segment.StartsWith("embed/", StringComparison.OrdinalIgnoreCase) ||
                        segment.StartsWith("youtu.be/", StringComparison.OrdinalIgnoreCase))
                    {
                        videoId = segment.Trim('/').Substring(segment.IndexOf('/') + 1);
                        return videoId;
                    }
                }

                // If no video ID is found, return an empty string
                return string.Empty;
            }
            catch (UriFormatException)
            {
                return string.Empty;
            }
        }




        public int GetVideoViewCount(string videoURL)
        {
            try
            {
                //string decodedVideoURL = HttpUtility.UrlDecode(videoURL);
                string videoId = videoURL; // ExtractVideoId(decodedVideoURL);

                if (videoId != null)
                {
                    var youtubeService = _youTubeService;
                    var videosRequest = youtubeService.Videos.List("statistics");
                    videosRequest.Id = videoId;

                    var videoResponse = videosRequest.Execute();

                    if (videoResponse.Items != null && videoResponse.Items.Count > 0)
                    {
                        var statistics = videoResponse.Items[0].Statistics;

                        // Check if ViewCount is available in the response
                        if (statistics != null && statistics.ViewCount.HasValue)
                        {
                            int viewCount = Convert.ToInt32(statistics.ViewCount.Value);
                            return viewCount;
                        }
                        else
                        {
                            throw new VideoNotFoundException("View count not available in the response");
                        }
                    }
                    else
                    {
                        throw new VideoNotFoundException("Video not found");
                    }

                }
                else
                {
                    return 0; // Invalid YouTube video URL
                }
            }
            catch (Exception ex)
            {
                throw new VideoServiceException($"Error: {ex.Message}");
            }
        }
    }

    
}
