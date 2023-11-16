import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class YoutubeService {

  constructor(private http: HttpClient) {}

  getChannelVideos(): Observable<any> {
    return this.http.get(`${environment.baseUrl}`);
  }

  getVideoTitle(videoURL: string): Observable<any> {
    return this.http.get(`${environment.baseUrl}/${encodeURIComponent(videoURL)}`);
  }

  getMostRecentVideo(): Observable<any> {
    return this.http.get(`${environment.baseUrl}/GetMostRecentVideo`);
  }

  getVideoInformation(videoURL: string): Observable<any> {
    return this.http.get(`${environment.baseUrl}/viewCount?videoURL=${encodeURIComponent(videoURL)}`);
  }
}
