import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class YoutubeService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getChannelVideos(): Observable<any> {
    return this.http.get(`${this.baseUrl}`);
  }

  getVideoTitle(videoURL: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/${encodeURIComponent(videoURL)}`);
  }

  getMostRecentVideo(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetMostRecentVideo`);
  }

  getVideoInformation(videoURL: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/viewCount?videoURL=${encodeURIComponent(videoURL)}`);
  }
}
