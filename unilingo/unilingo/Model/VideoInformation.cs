using System.ComponentModel.DataAnnotations;

namespace unilingo.Model
{
    public class VideoInformation
    {
        [Key]
        public string IdVideo { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewCount { get; set; }

        public VideoInformation()
        {
            IdVideo = Guid.NewGuid().ToString(); // Generate a unique ID for the video
        }
    }
}
