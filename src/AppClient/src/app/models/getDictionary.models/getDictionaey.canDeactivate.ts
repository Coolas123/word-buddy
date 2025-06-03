import { Injectable } from "@angular/core";
import { GetDictionaryComponent } from "../../components/dictionary/getDictionary.component";
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot } from "@angular/router";
import { Observable, Subject } from "rxjs";
import { MessageOptionsService } from "../alertOptionsMessage.models/alertOptionsMessage.service";
import { Message } from "../alertMessage.models/message.model";

@Injectable({
    providedIn: 'root'
  })
export class getDictionaryCanDeactivateGuard implements CanDeactivate<GetDictionaryComponent>{
    constructor(private messages: MessageOptionsService
        ) { }

    canDeactivate(component: GetDictionaryComponent, route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | boolean {
        if (component.formChanged) {
                let subject = new Subject<boolean>();

                this.messages.reportMessage(new Message("Не сохранять настройки?"),subject)
                
                return subject;
            }
        return true;
    }
}