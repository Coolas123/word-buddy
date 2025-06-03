import { Component, importProvidersFrom } from '@angular/core';
import { CommonModule } from '@angular/common'
import {RouterOutlet} from "@angular/router"
import {AlertMessageComponent} from "./components/alertMessage/alertMessage.component"
import { Model as s } from './models/loginUser.models/repository.loginUser.model';
import { HeaderComponent } from './components/header/header.component';
import { AlertOptionsMessageComponent } from './components/alertOptionsMessage/alertOptionsMessage.component';

@Component({
  selector: 'app-root',
  imports: [CommonModule,RouterOutlet,AlertMessageComponent, HeaderComponent,AlertOptionsMessageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',  
  providers: [s]
})
export class AppComponent {
  title = 'word buddy';
}
