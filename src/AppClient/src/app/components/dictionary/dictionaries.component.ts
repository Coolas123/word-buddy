import { Component } from "@angular/core"
import { FormsModule, ReactiveFormsModule } from "@angular/forms"
import { ActivatedRoute, Router } from "@angular/router"
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { Dictionary } from "../../models/dictionary.models/dictionary.model"
import { Model } from "../../models/dictionary.models/repository.dictionaries.model"
import { RouterModule } from '@angular/router';
import { PagingInfo } from "../../models/dictionary.models/pagingInfo.model"
import { formatDate, registerLocaleData } from "@angular/common"
import localeRu from '@angular/common/locales/ru';
registerLocaleData(localeRu, 'ru');
@Component({
    selector: "dictionaries",
    templateUrl: "./dictionaries.component.html",
    imports: [FormsModule, ReactiveFormsModule, RouterModule],
    styles: ['.ref1:hover .card{border-color: rgb(0 0 0);}'],
})

export class DictionariesComponent {
    dictionaries: Dictionary[] = []
    pagingDictionary: Dictionary[] = []
    pagingInfo: PagingInfo = new PagingInfo(0)

    constructor(private model: Model, private router: Router, private messageService: MessageService, private activeRoute: ActivatedRoute) {
        this.model.getDictionariesObservable().subscribe(dictionaries=>{
            this.dictionaries = dictionaries.sort(function (a, b) {
                return new Date(b.LastViewedAt!).getTime() - new Date(a.LastViewedAt!).getTime();
            });

            this.pagingInfo = new PagingInfo(6, this.dictionaries.length)
            this.pagingDictionary = this.getPagingDictionaries()
        })
    }

    getFormatDate(date: Date) {
        return formatDate(date, 'medium', this.getLang());
    }

    getLang() {
        if (navigator.languages !== undefined)
            return navigator.languages[0];
        return navigator.language;
    }

    getDictionaryTitle(dictionary: Dictionary): string {
        return dictionary.Title.substring(0, Math.min(dictionary.Title.length, 50))
    }

    getDictionaryDescrition(dictionary: Dictionary): string {
        return dictionary.Description.substring(0, Math.min(dictionary.Title.length, 50))
    }

    counter(i: number) {
        return Array.from(Array(i).keys())
    }

    changePage(page: number) {
        this.pagingInfo.currentPage = page
        this.pagingDictionary = this.getPagingDictionaries()
    }

    getPagingDictionaries() {
        let start = this.pagingInfo.currentPage * this.pagingInfo.pageSize
        if (start + this.pagingInfo.pageSize > this.dictionaries.length) {
            return this.dictionaries.slice(start);
        }
        else {
            return this.dictionaries.slice(
                start, start+this.pagingInfo.pageSize);
        }
    }
}
