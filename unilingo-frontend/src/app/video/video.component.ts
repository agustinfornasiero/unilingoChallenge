import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { YoutubeService } from '../youtubeservice.service';

declare var YT: any; // Declare YT variable

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.css'],
})
export class VideoFormComponent implements OnInit {
  apiKey: string = 'AIzaSyBwU7iggs8HOWej0JfF-GcGthl7kiMhG5A';

  videoURL: string = '';
  videoTitle: string = '';
  mostRecentVideo: string = '';
  viewCount: number = 0;
  audioURL: string = 'https://www.youtube.com/watch?v=yourAudioId';
  spanishTranslation: string = '';

  @ViewChild('playerContainer') playerContainer!: ElementRef;
  private player: any; // Variable to hold the YouTube player

  constructor(private apiService: YoutubeService) {}

  ngOnInit() {
    // Load the YouTube IFrame API
    
  const tag = document.createElement('script');
  tag.src = `https://www.youtube.com/iframe_api?api=${this.apiKey}`;
  document.head.appendChild(tag);

  this.getMostRecent()
  }

  submitForm() {
    this.apiService.getVideoTitle(this.videoURL).subscribe((title) => {
      this.videoTitle = title;
    });

    this.getMostRecent();
  }

  getMostRecent(){    
    this.apiService.getMostRecentVideo().subscribe((video) => {
      this.mostRecentVideo = video;
    });
  }
  checkViewCount() {
    this.apiService.getVideoInformation(this.videoURL).subscribe((count) => {
      this.viewCount = count;
    });
  }

  // playAudio() {
  //   const audioId = this.getVideoId(this.audioURL);
  //   const startSeconds = 30; // Start playing from 30 seconds
  //   const endSeconds = 45; // Stop playing at 45 seconds
    
  //   this.player = new YT.Player(this.playerContainer.nativeElement, {
  //     height: '0',
  //     width: '0',
  //     videoId: audioId,
  //     events: {
  //       onReady: (event: any) => this.onPlayerReady(event),
  //     },
  //     playerVars: {
  //       start: startSeconds,
  //       end: endSeconds,
  //     },
  //   });
  // }
  playAudio() {
    const audioId = this.getVideoId(this.audioURL);
    const startSeconds = 30; // Start playing from 30 seconds
    const endSeconds = 45; // Stop playing at 45 seconds
  
    const iframeSrc = `https://www.youtube.com/embed/${audioId}?start=${startSeconds}&end=${endSeconds}&autoplay=1`;
    
    // You can create an iframe dynamically or use ngIf in the template to conditionally render it.
    const iframe = document.createElement('iframe');
    iframe.src = iframeSrc;
    iframe.width = '0';
    iframe.height = '0';
    document.body.appendChild(iframe);
  }
  
  
  onPlayerReady(event: any) {
    event.target.playVideo();
  }

  getVideoId(url: string): string {
    const videoIdMatches = url.match(
      /(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})/
    );
    return videoIdMatches ? videoIdMatches[1]! : '';
  }

   speakSpanish() {
  //   if (speechSynthesis) {
  //     const speech = new SpeechSynthesisUtterance(this.spanishTranslation);
  //     speech.lang = 'es';
  //     speechSynthesis.speak(speech);
  //   } else {
  //     console.error('Speech synthesis not supported in this browser.');
  //   }
   }

}
