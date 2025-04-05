import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'
import {RouterOutlet} from "@angular/router"
import {AlertMessageComponent} from "./components/alertMessage/alertMessage.component"
import { Model as s } from './models/loginUser.models/repository.loginUser.model';

@Component({
  selector: 'app-root',
  imports: [CommonModule,RouterOutlet,AlertMessageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',  
  providers: [s]
})
export class AppComponent {
  title = 'word buddy';
}
