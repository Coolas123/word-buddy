import { Routes, RouterModule } from '@angular/router';
import { RegisterUserFormComponent } from './components/registerUser/registerUserForm.component';
import { LoginUserFormComponent } from './components/loginUser/loginUserForm.component';
import { UserSettingsFormComponent } from './components/userSettings/userSettings.component';
import { AuthGuard } from './models/auth.guard';
import { GetSettingsResolver } from './models/userSettings.models/setting.resolver';
import { DictionariesComponent } from './components/dictionary/dictionaries.component';
import { DcitionariesResolver } from './models/dictionary.models/dictionaries.resolver';
import { CreateDictionaryFormComponent } from './components/dictionary/createDictionary.component';
import { GetDictionaryComponent } from './components/dictionary/getDictionary.component';
import { GetDcitionaryResolver } from './models/getDictionary.models/getDictionary.resolver';

export const routes: Routes = [
    { path: "users/register", component: RegisterUserFormComponent },
    { path: "users/login", component: LoginUserFormComponent },
    { path: "users/settings", component: UserSettingsFormComponent, canActivate: [AuthGuard], resolve: { model: GetSettingsResolver } },
    { path: "dictionaries/create", component: CreateDictionaryFormComponent, canActivate: [AuthGuard] },
    { path: "dictionaries/:id", component: GetDictionaryComponent, canActivate: [AuthGuard], resolve: { model: GetDcitionaryResolver } },
    { path: "dictionaries", component: DictionariesComponent, canActivate: [AuthGuard], resolve: { model: DcitionariesResolver } },
];

export const routing = RouterModule.forRoot(routes); 
