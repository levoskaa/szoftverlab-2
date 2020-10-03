import { HttpClient } from '@angular/common/http';
import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from './models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Otthonbazár';
  advertisements: Advertisement[];
  navBarCollapsed: boolean = true;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<Advertisement[]>('/api/advertisements')
      .subscribe(res => this.advertisements = res);
  }

  toggleNavBar() {
    this.navBarCollapsed = !this.navBarCollapsed;
  }
}
