import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from '../models';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  advertisements: Advertisement[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAdvertisements();
  }

  getAdvertisements(): void {
    this.http.get<Advertisement[]>('/api/advertisements')
      .subscribe(res => this.advertisements = res);
  }
}
