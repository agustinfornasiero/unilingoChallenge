import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class YoutubeService {
  private readonly baseRoute = environment.settings.apiUrl + '/YouTube';

  constructor(private http: HttpClient) {}

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
}
