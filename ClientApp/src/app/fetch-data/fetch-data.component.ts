import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public feedStories: FeedStories[];
  private interval;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.refreshData(http, baseUrl);
    this.interval = setInterval(() => {
      this.refreshData(http, baseUrl);
    }, 60000); // Refresh the feed every minute

  }

  refreshData(http: HttpClient, baseUrl: string) {
    console.log('Data Refreshed');
    http.get<FeedStories[]>(baseUrl + 'NewsFeed').subscribe(result => {
      this.feedStories = result;
    }, error => console.error(error));

  }
}


interface FeedStories {
  id: number;
  author: string;
  title: string;
  commentCount: number;
  url: string;
  baseUrl: string;
  published: string;
}
