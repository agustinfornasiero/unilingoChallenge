import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class YoutubeService {
  private readonly baseRoute = environment.settings.apiUrl + '/YouTube';

  constructor(private http: HttpClient,
              //private translate: TranslateService
              ) {//translate.setDefaultLang('en'); 
              }

  getChannelVideos(): Observable<any> {
    return this.http.get(`${this.baseRoute}`);
  }

  getVideoTitle(videoURL: string): Observable<any> {
    return this.http.get(`${this.baseRoute}/${encodeURIComponent(videoURL)}`);
  }

  getMostRecentVideo(): Observable<any> {
    return this.http.get(`${this.baseRoute}/GetMostRecentVideo`);
  }

  getVideoInformation(videoURL: string): Observable<any> {
    return this.http.get(`${this.baseRoute}/viewCount?videoURL=${encodeURIComponent(videoURL)}`);
  }

   translateAudio() {
  //   const apiKey = 'GOOGLE_TRANSLATE_API_KEY';
  //   const endpoint = 'https://translation.googleapis.com/language/translate/v2';
  
  //   const audioSegment = 'Hello, this is a translation of the audio segment.';
  //   const targetLanguage = 'es'; // Spanish
  
  //   // Make a request to Google Translate API
  //   const url = `${endpoint}?q=${encodeURIComponent(audioSegment)}&target=${targetLanguage}&key=${apiKey}`;
  
  //   this.http.get(url).subscribe((response: any) => {
  //     if (response && response.data && response.data.translations) {
  //       this.spanishTranslation = response.data.translations[0].translatedText;
  //     } else {
  //       console.error('Error translating audio:', response);
  //     }
  //   });
   }
}
