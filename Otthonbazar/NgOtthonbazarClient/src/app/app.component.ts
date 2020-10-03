import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Otthonbazár';
  navBarCollapsed: boolean = true;

  toggleNavBar() {
    this.navBarCollapsed = !this.navBarCollapsed;
  }
}
