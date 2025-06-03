import { Component } from "@angular/core";
import { Router, RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { Model as DictionariesModel } from "../../models/dictionary.models/repository.dictionaries.model"
import { Dictionary } from "../../models/dictionary.models/dictionary.model";
import { CommonModule, formatDate } from "@angular/common";
import { LearnStatus } from "../../enums/learnStatus";
import { Model } from "../../models/studyPlan/repository.studyPlan.model";

@Component({
    selector: "studyPlan",
    templateUrl: "./studyPlan.component.html",
    imports: [FormsModule, ReactiveFormsModule, RouterModule, CommonModule]
})

export class StudyPlanComponent {
    formSubmitted: boolean = false
    dictionaries: Dictionary[] = []
    dictionariesCard:string[]=[]
    learnStatuesEnum = LearnStatus

    NotStudiedDictionariesId: [] = []
    //NotStudiedSelected: boolean = false
    InStudyingDictionariesId: [] = []
    //InStudyingSelected: boolean = false
    NeedToRememberDictionariesId: [] = []
    //NeedToRememberSelected: boolean = false
    VeryDifficultDictionariesId: [] = []
    //VeryDifficultSelected: boolean = false
    selectedPlanName:string=""
    selectedPlanValues:Map<string,boolean> = new Map([
        ["NotStudied", false],
        ["InStudying", false],
        ["NeedToRemember", false],
        ["VeryDifficult", false]
    ])
    

    constructor(private model:Model,private dictionariesModel: DictionariesModel,private router: Router) {

        this.dictionariesModel.getDictionariesObservable().subscribe(x => {
            this.dictionaries = x
        })
    }

    selectCardsGroupSubmitForm() {
        this.formSubmitted = true
        if (this.dictionariesCard.length) {
            this.model.dictionariesCardId = this.dictionariesCard
            this.router.navigateByUrl("/cards")
        }
        else{
            this.formSubmitted = false
        }
    }
    selecteDictionaryRowsStyle(selectMode:boolean) {
        return selectMode ? { border: "1px solid red", } : {}
    }

    selecteDictionaryRowsClass(selectMode:boolean) {
        return !selectMode ? {
            "border-secondary-subtle": true
        } : {}
    }

    selecteDictionaryRows(selectMode:boolean,index: number, event: any) {
        if (!selectMode) return
        let htmlElement = event.target as HTMLElement

        let firstHtmlTD: HTMLElement
        if (htmlElement.parentElement?.nodeName == "TR") {
            firstHtmlTD = htmlElement.parentElement.childNodes[0] as HTMLElement
        }
        else {
            firstHtmlTD = htmlElement.parentElement?.parentElement?.childNodes[0] as HTMLElement
        }

        if (firstHtmlTD?.classList.contains('bg-danger')) {
            firstHtmlTD?.classList.replace('bg-danger', 'bg-success')
            if (firstHtmlTD.parentElement) {
                firstHtmlTD.parentElement.style.border = "1px solid green"

                let dic = this.dictionaries[index]
                if (dic?.Id) {
                    this.dictionariesCard.push(dic.Id)
                }
            }
        } else {
            firstHtmlTD?.classList.replace('bg-success', 'bg-danger')
            if (firstHtmlTD.parentElement) {
                firstHtmlTD.parentElement.style.border = "1px solid red"
                let dic = this.dictionaries[index]
                if (dic?.Id) {
                    let index = this.dictionariesCard.indexOf(dic.Id)
                    this.dictionariesCard.splice(index,1)
                }
            }
        }
    }

    getFormatDate(date: Date) {
        return formatDate(date, 'medium', this.getLang());
    }

    getLang() {
        if (navigator.languages !== undefined)
            return navigator.languages[0];
        return navigator.language;
    }

    showWordContext(id:string,index:number){
        let element = document.getElementById(`${id}${index}`);
        if(element != null){
            element.style.display= element.style.display == "none"? "table-row":"none"
        }
    }
}