import { Routes, RouterModule } from '@angular/router';
import { RegisterUserFormComponent } from './components/registerUser/registerUserForm.component';
import { LoginUserFormComponent } from './components/loginUser/loginUserForm.component';
import { UserSettingsFormComponent } from './components/userSettings/userSettings.component';
import { AuthGuard } from './models/auth.guard';
import { GetSettingsResolver } from './models/userSettings.models/setting.resolver';
import { DictionariesComponent } from './components/dictionary/dictionaries.component';
import { DcitionariesResolver } from './models/dictionary.models/dictionaries.resolver';
import { GetDictionaryComponent } from './components/dictionary/getDictionary.component';
import { GetDcitionaryResolver } from './models/getDictionary.models/getDictionary.resolver';
import { HomeComponent } from './components/home/home.component';
import { CardsFileComponent } from './components/cards/cards.component';
import { CardPlanComponent } from './components/CardPlan/cardPlan.component';
import { CreateDictionaryFormComponent } from './components/dictionary/CreateDictionary.component';
import { CardPlansResolver } from './models/cardPlan.models/cardPlans.resolver';
import { GetCardsResolver } from './models/cards.models/cards.resolver';
import { getDictionaryCanDeactivateGuard } from './models/getDictionary.models/getDictionaey.canDeactivate';
import { GenerateWordContextComponent } from './components/ai/generateWordContext.component';
import { aiResolver } from './models/ai.models/ai.resolver';


export const routes: Routes = [
    { path: "users/register", component: RegisterUserFormComponent },
    { path: "users/login", component: LoginUserFormComponent },
    { path: "users/settings", component: UserSettingsFormComponent, canActivate: [AuthGuard], resolve: { model: GetSettingsResolver } },
    { path: "dictionaries/create", component: CreateDictionaryFormComponent, canActivate: [AuthGuard] },
    { path: "dictionaries/:id", component: GetDictionaryComponent, canActivate: [AuthGuard], canDeactivate:[getDictionaryCanDeactivateGuard] ,resolve: { model: GetDcitionaryResolver } },
    { path: "dictionaries", component: DictionariesComponent, canActivate: [AuthGuard], resolve: { model: DcitionariesResolver } },
    { path: "cardPlan/:cardMode/:cardPlanId/:cardBoxStatus", component: CardsFileComponent, canActivate: [AuthGuard], resolve:{model:GetCardsResolver} },
    { path: "cardPlan", component: CardPlanComponent, canActivate: [AuthGuard], resolve:{model:CardPlansResolver}},
    { path: "cards/:cardMode/:dictionaryId/:learnStatus", component: CardsFileComponent, canActivate: [AuthGuard], resolve:{model:GetCardsResolver} },
    { path: "ai", component: GenerateWordContextComponent, canActivate: [AuthGuard], resolve:{model:aiResolver} },
    { path: "", component:HomeComponent},
    { path: "**", redirectTo:"/"}
];  

export const routing = RouterModule.forRoot(routes); 
