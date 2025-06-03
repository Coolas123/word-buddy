import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, NavigationEnd, Router, RouterModule } from "@angular/router";
import { FormArray, FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CreateCardPlanFormGroup } from "../../models/cardPlan.models/createCardPlanForm.model";
import { Dictionary } from "../../models/dictionary.models/dictionary.model";
import { Model } from "../../models/cardPlan.models/repository.getDictionariesFreeForCardPlan";
import { formatDate } from "@angular/common";
import { CreateCardPlan, GetCardPlan, UpdateCardPlanViewModel } from "../../models/cardPlan.models/CardPlan.model";
import { HttpErrorResponse } from "@angular/common/http";
import { filter } from "rxjs";
import { MessageService } from "../../models/alertMessage.models/alertMessage.service";
import { Message } from "../../models/alertMessage.models/message.model";
import { LearnStatus } from "../../enums/learnStatus";
import { CardBox } from "../../models/cardBox.models/cardBox";
import { Model as CardsModel } from "../../models/cards.models/repository.cards.model";


@Component({
    selector: "cardPlan",
    templateUrl: "./cardPlan.component.html",
    imports: [FormsModule, ReactiveFormsModule, RouterModule]
})
export class CardPlanComponent implements OnInit {
    formSubmitted: boolean = false
    formGroup: CreateCardPlanFormGroup
    dictionaries: Dictionary[] = []
    cardPlans: GetCardPlan[] = []
    learnStatuesEnum = LearnStatus
    editMode: boolean = false

    constructor(private activeRoute: ActivatedRoute, private formBuilder: FormBuilder, private model: Model,
        private router: Router, private messageService: MessageService, private cardsModel: CardsModel
    ) {
        this.formGroup = new CreateCardPlanFormGroup(this.formBuilder, undefined);
    }
    ngOnInit(): void {
        this.loadDictionaries()
        this.model.getDictionariesObservable().subscribe(x => {
            this.dictionaries = x
        })

        this.model.loadCardPlans()
        this.model.getCardPlansObservable().subscribe(x => {
            this.cardPlans = x
        })
    }

    createCardPlanSubmitForm() {
        this.formSubmitted = true
        if (this.formGroup.valid && this.formGroup.value.StoredDictionariesId.length != 0) {
            let newCardPlan = new CreateCardPlan(this.formGroup.value.Title, this.formGroup.value.Description, this.formGroup.value.StoredDictionariesId.filter((x:any)=>!x.stored && x.selected).map((x: any) => x.value.Id))
            this.model.createCardPlan(newCardPlan).subscribe({
                next: (x) => {
                    this.router.navigateByUrl("/").then(x => {
                        this.router.navigateByUrl("/cardPlan")
                    })
                    this.router.events.pipe(
                        filter((event: any) => event instanceof NavigationEnd)
                    ).subscribe(_ => this.messageService.reportMessage(new Message(x)))
                },
                error: (e: HttpErrorResponse) => {
                    this.setCustomErrors(e.error)
                    this.formSubmitted = false
                },
                complete: () => {
                    this.formSubmitted = false
                }
            })
        }
    }

    updateCardPlanSubmitForm() {
        this.formSubmitted = true
        if (this.formGroup.valid && 
            (this.formGroup.value.StoredDictionariesId.filter((x:any)=>x.selected).length != 0 ||
            this.formGroup.value.StoredDictionariesId.filter((x:any)=>x.stored && !x.selected).length != 0)
        ) {

            let updateCardPlan = new UpdateCardPlanViewModel(
                this.formGroup.value.Id,
                this.formGroup.value.Title,
                this.formGroup.value.Description,
                this.formGroup.value.StoredDictionariesId.filter((x:any)=>!x.stored && x.selected).map((x: any) => x.value.Id),
                this.formGroup.value.StoredDictionariesId.filter((x:any)=>x.stored && !x.selected).map((x: any) => x.value.Id))
                this.model.updateCardPlan(updateCardPlan).subscribe({
                next: (x) => {
                    this.router.navigateByUrl("/").then(x => {
                        this.router.navigateByUrl("/cardPlan")
                    })
                    this.router.events.pipe(
                        filter((event: any) => event instanceof NavigationEnd)
                    ).subscribe(_ => this.messageService.reportMessage(new Message(x)))
                },
                error: (e: HttpErrorResponse) => {
                    this.setCustomErrors(e.error)
                    this.formSubmitted = false
                },
                complete: () => {
                    this.formSubmitted = false
                }
            })
        }
    }

    getFreeDictionaries(){
        return this.formGroup.value.StoredDictionariesId.filter((x:any)=>!x.stored)
    }

    changeEditMode(cardPlan: any) {
        this.formGroup = new CreateCardPlanFormGroup(this.formBuilder, cardPlan,this.dictionaries);
        this.editMode = !this.editMode
    }

    redirectToCard(cardPlan: GetCardPlan, cardBox?: CardBox) {
        this.cardsModel.serCardBox(cardBox!)
        this.cardsModel.setDicitonariesId(cardPlan.Dictionaries.map(x => x.Id))
        this.router.navigateByUrl(`cardPlan/${"test"}/${cardPlan?.Id}/${cardBox?.LearnStatus}`);
    }

    getLearnStatusValue(key: any) {
        let index = Object.keys(LearnStatus).findIndex(v => v == key)
        return Object.values(LearnStatus)[index]
    }

    getCardBox(cardPlan: GetCardPlan, ls: string) {
        return cardPlan.CardBoxes.find((x: any) => x.LearnStatus == ls)
    }

    private setCustomErrors(errors: any) {
        for (let i = 0; i < errors.length; i++) {
            var control = this.formGroup.getControls.find(x => x.modelProperty === errors[i].Code)
            if (control) {
                var serverError = errors[i].Code
                control.setErrors({ serverError: errors[i].Message })
                control.markAsDirty()
                control.markAsTouched()
            }
        }
    }

    loadDictionaries() {
        this.model.dictionaresFreeForCardPlanSubject.next("");
    }

    selecteDictionaryRows(index: number, event: any, selectedDic: any) {
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

                selectedDic.selected = true
            }
        } else {
            firstHtmlTD?.classList.replace('bg-success', 'bg-danger')
            if (firstHtmlTD.parentElement) {
                firstHtmlTD.parentElement.style.border = "1px solid red"

                selectedDic.selected = false
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

    changeFormGroup(cardPlan?: GetCardPlan) {
        this.formGroup = new CreateCardPlanFormGroup(this.formBuilder, cardPlan,this.dictionaries);
    }

    showWordContext(id: string, index: number) {
        let element = document.getElementById(`${id}${index}`);
        if (element != null) {
            element.style.display = element.style.display == "none" ? "table-row" : "none"
        }
    }
}