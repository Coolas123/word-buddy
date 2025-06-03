import { Component, OnDestroy } from "@angular/core"
import { FormBuilder, FormsModule, ReactiveFormsModule } from "@angular/forms"
import { ActivatedRoute, Router } from "@angular/router"
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { AiService } from "../../models/ai.models/generateWordContext.repository"
import { AiFormGroup } from "../../models/ai.models/AiForm.model"
import { Model } from "../../models/ai.models/repository.model"
import { GenerateTextQuery } from "../../models/ai.models/GenerateTextQuery"
import { LearnStatus } from "../../enums/learnStatus"
import { Message } from "../../models/alertMessage.models/message.model"

@Component({
    selector: "ai",
    templateUrl: "./generateWordContext.component.html",
    imports: [FormsModule, ReactiveFormsModule],
    providers: []
})

export class GenerateWordContextComponent implements OnDestroy {
    formSubmitted: boolean = false
    formGroup: AiFormGroup
    selectedWord: string[] = []
    newLearnStatues: Array<{ index: number, value: string, key: string }> = []
    selectedLearnStatus: string = ""
    mode:string=""
    answers:string=""
    isShowAnswers:boolean = false
    isTestButton:boolean= false

    textCreatContent: string = ""
    resultGenerate: string = ""

    constructor(private model: Model, private aiService: AiService, private router: Router, private messageService: MessageService, activeRoute: ActivatedRoute, private fromBuilder: FormBuilder) {
        this.formGroup = new AiFormGroup(this.fromBuilder);
        this.setNewLearnStatues()
    }
    ngOnDestroy(): void {
        this.aiService.removeConnection()
    }

    submitForm() {
        this.formSubmitted = true
        if (this.formGroup.valid) {

        }
        else this.formSubmitted = false
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

    generateText() {
        if(this.selectedWord.length == 0) {
            this.messageService.reportMessage(new Message("Выберите слова"))
            return
        }
        this.resultGenerate=""
        this.isTestButton = false
        this.isShowAnswers = false
        this.answers = ""
        let prompt = "Generate a coherent and meaningful text based on the following words. The number of sentences must exactly match the number of words provided. Each sentence must include one of the given words in its original form (unchanged).Words: "+this.selectedWord.toString()+".Ensure the text flows naturally—avoid simply listing the words. " + this.textCreatContent ==""? "": "About: "+this.textCreatContent + "return only new text"
        this.aiService.generateWordContext(new GenerateTextQuery(prompt))
        this.aiService.getWordContextGeneratedObservable().subscribe(x => {
            this.resultGenerate += x
        })
    }

    generateTest() {
        if(this.selectedWord.length == 0) {
            this.messageService.reportMessage(new Message("Выберите слова"))
            return 
        }
        this.isTestButton = true
        this.answers =(this.shuffle(this.selectedWord)).map((x:any,i:number)=>`${i+1}. ${x} `).toString() 
        let prompt = "Generate one sentence for each of the following words. In each sentence, use the corresponding word, but replace it with an ellipsis '...' (three dots) so that the word is omitted. Each resulting sentence should be numbered and start on a new line.List of words: " + this.answers + " For example Word: 'forest' 1. We walked through the dense ... all day \\r\\n. Each sentence should be numbered. Add: '\\r\\n' in the end of every sentense."
        this.aiService.generateWordContext(new GenerateTextQuery(prompt))
        this.aiService.getWordContextGeneratedObservable().subscribe(x => {
            this.resultGenerate += x
        })
    }

    saveContext(){
        this.aiService.getGeneratedWordContextObservable().subscribe(x=>{
            if(this.resultGenerate !="")
                this.model.saveContext(this.resultGenerate)
        })
    }

    ask(){
        this.resultGenerate=""
        this.isTestButton = false
        this.isShowAnswers = false
        if(this.textCreatContent == "") return
        let prompt = "answer, or correct and explain the sentense: "+ this.textCreatContent
        this.aiService.generateWordContext(new GenerateTextQuery(prompt))
        this.aiService.getWordContextGeneratedObservable().subscribe(x => {
            this.resultGenerate += x
        })
    }

    getSavedContests(){
        return this.model.savedContexts
    }

    loadContexts(){
        this.model.loadSavedContexts()
    }

    showAnswers(){
        this.isShowAnswers = !this.isShowAnswers
    }

    selectLearnStatus(event: Event) {
        let htmlElment = event.target as HTMLInputElement
        this.selectedLearnStatus = htmlElment.value
    }

    selectWord(event: any, row: any) {
        this.selectedWord.push(row.WordText)
    }

    deleteWord(index: number) {
        this.selectedWord.splice(index, 1)
    }

    selectDictionary(event: Event) {
        let htmlElment = event.target as HTMLInputElement
        this.model.loadDictionary(htmlElment.value)
    }

    getDictionaries() {
        return this.model.dictionaries
    }

    getSelectedDictionary() {
        return this.model.selectedDictionary
    }

    private setNewLearnStatues() {
        let result = Array<{ index: number, value: string, key: string }>();
        Object
            .keys(LearnStatus)
            .forEach((v, i) => {
                if (isNaN(Number(v)) && i != 0) {
                    result.push({
                        index: i,
                        value: Object.values(LearnStatus)[i],
                        key: v
                    })
                }
            })
        this.newLearnStatues = result;
    }

    shuffle(srcArray:any) {
        let array = srcArray
        var currentIndex = array.length, temporaryValue, randomIndex;

        // While there remain elements to shuffle...
        while (0 !== currentIndex) {

            // Pick a remaining element...
            randomIndex = Math.floor(Math.random() * currentIndex);
            currentIndex -= 1;

            // And swap it with the current element.
            temporaryValue = array[currentIndex];
            array[currentIndex] = array[randomIndex];
            array[randomIndex] = temporaryValue;
        }
        console.log(array)
        return array;
    }
}