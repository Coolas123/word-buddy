import { Component, HostListener } from "@angular/core";
import { ActivatedRoute, NavigationEnd, Router, RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { UpdateDictionaryAndRowsCommand, UpdateDictionaryRowCommand } from "../../models/getDictionary.models/getDictionary.model";
import { Model } from "../../models/cards.models/repository.cards.model";
import { MessageService } from "../../models/alertMessage.models/alertMessage.service";
import { Message } from "../../models/alertMessage.models/message.model";
import { filter } from "rxjs";
import { LearnStatus } from "../../enums/learnStatus";
import { UpdateDictionaryRowLearnStatusCommand, UpdateDictionaryRowsLearnStatusCommand } from "../../models/cards.models/cards.model";


@Component({
    selector: "cards",
    templateUrl: "./cards.component.html",
    imports: [FormsModule, ReactiveFormsModule, RouterModule]
})
export class CardsFileComponent {
    formSubmitted: boolean = false
    currentDictionaryAndRowsIndex: number = 0
    currentDicitonaryAndRows: UpdateDictionaryAndRowsCommand = new UpdateDictionaryAndRowsCommand()
    currentRowIndex: number = 0
    currentRow: UpdateDictionaryRowCommand | undefined
    showTranslation: boolean = false
    updateDictionaryRows: UpdateDictionaryRowsLearnStatusCommand = new UpdateDictionaryRowsLearnStatusCommand([])
    learnStatus = LearnStatus
    newLearnStatues: Array<{ index: number, value: string, key: string }> = []
    nextButton: LearnStatus = LearnStatus.None
    backButton: LearnStatus = LearnStatus.None
    stayButton: LearnStatus = LearnStatus.None
    cardBoxLearnStatus: string = ""
    cardMode: string = ""


    constructor(private model: Model, private activeRoute: ActivatedRoute,
        private router: Router, private messageService: MessageService
    ) {
        this.activeRoute.params.subscribe(x => {
            this.cardMode = x["cardMode"]

            if(this.cardMode == "test"){
                this.cardBoxLearnStatus = x["cardBoxStatus"]
            }
            else{
                this.cardBoxLearnStatus = x["learnStatus"]
            }
            this.activeRoute.data.subscribe((x) => {
                if (this.cardMode == "test") {
                    this.model.getCardPlanObservable().subscribe(x => {
                        this.model.getdictionaryCardPlanIdSubject.next(
                            {
                                id: this.model.getCardPlanDicitonariesId()[this.currentDictionaryAndRowsIndex],
                                learnStatus: this.cardBoxLearnStatus
                            })
                    })
                }

                this.model.getDictionaryObservable().subscribe(x => {
                    this.currentDicitonaryAndRows = x
                    this.currentRowIndex = 0
                    this.currentRow = this.currentDicitonaryAndRows.UpdateDictionaryRowsCommand?.DictionaryRows[this.currentRowIndex]
                })
            })
        })
        this.setNewLearnStatues()
        switch (this.cardBoxLearnStatus as string) {
            case ("NotStudied"):
                this.nextButton = LearnStatus.InStudying
                this.stayButton = LearnStatus.NotStudied
                this.backButton = LearnStatus.NotStudied
                break;
            case ("InStudying"):
                this.nextButton = LearnStatus.NeedToRemember
                this.stayButton = LearnStatus.InStudying
                this.backButton = LearnStatus.NotStudied
                break;
            case ("NeedToRemember"):
                this.stayButton = LearnStatus.NeedToRemember
                this.backButton = LearnStatus.InStudying
                this.stayButton = LearnStatus.None
                break;
        }
    }

    @HostListener("window:keydown", ["$event"]) keyEvent(event: any) {
        this.keyEventHandler(event)
    }

    keyEventHandler(e: KeyboardEvent) {
        switch (e.key) {
            case "z":
            case "Z":
                this.changeLearnStatus(this.currentRow!, this.backButton)
                break;
            case "x":
            case "X":
                this.nextRow()
                break;
            case "c":
            case "C":
                this.changeLearnStatus(this.currentRow!, this.nextButton)
                break;
        }
    }

    getDictionaryRowsCount(){
        return this.currentRowIndex < this.currentDicitonaryAndRows.UpdateDictionaryRowsCommand?.DictionaryRows?.length!
    }

    selectUploadFileSubmitForm() {
        this.formSubmitted = true
        if (true) {

            this.formSubmitted = false
        }
    }

    changeTranslationFlag() {
        this.showTranslation = !this.showTranslation
    }

    changeLearnStatus(currentRow: UpdateDictionaryRowCommand, learnStatus: LearnStatus) {
        currentRow.LearnStatus = learnStatus
        this.addDicitonaryRowToUpdate(this.currentRow!)
        this.nextRow()
    }

    addDicitonaryRowToUpdate(row: UpdateDictionaryRowCommand) {
        let r = Object.keys(LearnStatus)[Object.values(LearnStatus).findIndex(x => x == row.LearnStatus)]
        this.updateDictionaryRows?.DictionaryRows
            .push(new UpdateDictionaryRowLearnStatusCommand(row.Id, r, this.cardMode == "test"))
    }

    getLearnStatusByStringKey(key: string) {
        return LearnStatus[key as keyof typeof LearnStatus]
    }

    nextRow(){
        if(this.cardMode == "test"){
            this.nextRowTest()
        }
        else{
            this.nextRowNotTest()
        }
    }

    async nextRowTest() {
        if (this.currentDictionaryAndRowsIndex < this.model.getCardPlanDicitonariesId()?.length) {
            if (this.currentRowIndex + 1 < this.currentDicitonaryAndRows.UpdateDictionaryRowsCommand?.DictionaryRows?.length!) {
                this.getNextRow()
            }
            else {
                this.model.updateLearnStatusDictionaryRows(this.updateDictionaryRows).subscribe(x => {
                    if (this.currentDictionaryAndRowsIndex + 1 < this.model.getCardPlanDicitonariesId()?.length) {
                        this.currentDictionaryAndRowsIndex++
                        this.currentDicitonaryAndRows = new UpdateDictionaryAndRowsCommand()
                        this.currentRow = undefined
                        this.model.getdictionaryCardPlanIdSubject.next(this.model.getCardPlanDicitonariesId()[this.currentDictionaryAndRowsIndex])
                    }
                    else {
                        this.redirect()
                    }
                })
            }
        }
        else {
            this.model.updateLearnStatusDictionaryRows(this.updateDictionaryRows).subscribe(x => {
                this.redirect()
            })
        }
    }

    async nextRowNotTest() {
        if (this.currentRowIndex + 1 < this.currentDicitonaryAndRows.UpdateDictionaryRowsCommand?.DictionaryRows?.length!) {
            this.getNextRow()
        }
        else {
            this.model.updateLearnStatusDictionaryRows(this.updateDictionaryRows).subscribe(x => {
                this.redirect()
            })
        }
    }

    private redirect() {
        this.router.navigateByUrl("/cardPlan")
        this.router.events.pipe(
            filter((event: any) => event instanceof NavigationEnd)
        ).subscribe(_ => this.messageService.reportMessage(new Message(`Слова категории "${LearnStatus[this.cardBoxLearnStatus as keyof typeof LearnStatus]}" успешно повторены`)))
    }

    private getNextRow() {
        this.currentRowIndex++
        this.currentRow = this.currentDicitonaryAndRows.UpdateDictionaryRowsCommand?.DictionaryRows[this.currentRowIndex]
        return this.currentRow
    }

    private setNewLearnStatues() {
        let result = Array<{ index: number, value: string, key: string }>();
        Object
            .keys(LearnStatus)
            .forEach((v, i) => {
                if (isNaN(Number(v)) && i != 0 && i != 4) {
                    result.push({
                        index: i,
                        value: Object.values(LearnStatus)[i],
                        key: v
                    })
                }
            })
        this.newLearnStatues = result;
    }
}