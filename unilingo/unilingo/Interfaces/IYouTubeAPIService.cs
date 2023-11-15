namespace unilingo.Interfaces
{
    public interface IYouTubeAPIService
    {
        //Get VideoTitle Method
        public string GetVideoTitle(string videoURL);

        // Extract video ID from YouTube video URL
        public string ExtractVideoId(string url);

        //Get the CountViews of a Video
        public int GetVideoViewCount(string videoURL);

    }
}
