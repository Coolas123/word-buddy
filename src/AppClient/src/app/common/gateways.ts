import { LearnStatus } from "../enums/learnStatus"

export class UserURLs {
    private static URL="https://localhost:5001"

    private static _register : string ="/user-api/Users/Register" 
    private static _login : string ="/user-api/Users/Login"

    private static _settings : string ="/user-api/Settings"

    private static _dictionaries : string ="/dictionary-api/Dictionaries"
    private static _createDictionary : string ="/dictionary-api/Dictionaries"

    private static _getDictionary : string ="/dictionary-api/Dictionaries"
    private static _updateDictionary : string ="/dictionary-api/Dictionaries"
    private static _updateLearnStatusDictionaryRows:string="/dictionary-api/Dictionaries/UpdateLearnStatus"

    private static _GenerateWordContextHub: string ="https://localhost:5031/chathub"
    private static _GenerateWordContext : string = "/ai-api/GenerateWordContext"

    private static _moveToAnotherDicitonary : string = "/dictionary-api/Dictionaries/MoveToAnotherDictionary"

    private static _getFreeDictionariesForCardPlan:string = "/dictionary-api/Dictionaries/GetFreeDicitonariesForCardPlan"
    private static _getDictionaryWithRowsByCardPlanIdAndLearnStatus:string = "/dictionary-api/CardPlans"
    private static _getDictionaryWithRowsByLearnStatus:string = "/dictionary-api/Dictionaries"
    private static _getDictionaryViewModelWithRowsViewModel:string = "/dictionary-api/Dictionaries/GetDictionaryViewModelWithRowsViewModel"

    private static _createCardPlan:string = "/dictionary-api/CardPlans"
    private static _getCardPlans:string = "/dictionary-api/CardPlans/GetCardPlans"
    private static _getCardPlanWithDictionariesAndCardBoxes:string = "/dictionary-api/CardPlans/GetCardPlanWithDictionariesAndCardBoxes"
    //private static _getCardPlanDictionariesId:string = "/dictionary-api/CardPlans/GetDictionariesId"
    private static _updateCardPlan:string = "/dictionary-api/CardPlans"
    private static _getCardPlanWithDictionaries:string = "/dictionary-api/CardPlans/GetCardPlanWithDictionaries"


    private static _saveGeneratedTextHistory:string = "/dictionary-api/GeneratedTextHistory"
    private static _getGeneratedTextHistories:string = "/dictionary-api/GeneratedTextHistory"

    static registerURI(): string{
        return this.URL.concat(this._register)
    }

    static loginURI(): string{
        return this.URL.concat(this._login)
    }

    static settingsURI(): string{
        return this.URL.concat(this._settings)
    }

    static dictionariesURI(): string{
        return this.URL.concat(this._dictionaries)
    }

    static createDictionaryURI(): string{
        return this.URL.concat(this._createDictionary)
    }

    static getDictionaryURI(dictionaryId: string): string{
        return this.URL.concat(`${this._getDictionary}/${dictionaryId}`)
    }

    static updateDictionaryURI(): string{
        return this.URL.concat(this._updateDictionary)
    }
    
    static generateWordContextURI(): string{
        return this.URL.concat(this._GenerateWordContext)
    }

    static generateWordContextHubUri(): string{
        return this._GenerateWordContextHub
    }

    static moveToAnotherDicitonary(): string{
        return this.URL.concat(this._moveToAnotherDicitonary)
    }
    
    static getFreeDictionariesForCardPlan():string{
        return this.URL.concat(this._getFreeDictionariesForCardPlan)
    }

    static createCardPlan():string{
        return this.URL.concat(this._createCardPlan)
    }

    static getCardPlans():string{
        return this.URL.concat(this._getCardPlans)
    }

    static getCardPlanWithDictionariesAndCardBoxes():string{
        return this.URL.concat(this._getCardPlanWithDictionariesAndCardBoxes)
    }

    // static getCardPlanDictionariesId(cardPlanId:string, cardBoxStatus:string):string{
    //     return this.URL.concat(`${this._getDictionaryWithRowsByCardPlanIdAndLearnStatus}/${cardPlanId}/${cardBoxStatus}`)
    // }

    static updateDictionaryRows():string{
        return this.URL.concat(this._updateLearnStatusDictionaryRows)
    }
    
    static updateCardPlan():string{
        return this.URL.concat(this._updateCardPlan)
    }

    static getDictionariesIdByCardPlanIdAndLearnStatus(cardPlanId:string, cardBoxStatus:string):string{
        return this.URL.concat(`${this._getDictionaryWithRowsByCardPlanIdAndLearnStatus}/${cardPlanId}/${cardBoxStatus}`)
    }

    static getDictionaryWithRowsByLearnStatus(dictionaryId:string, learnStatus:string):string{
        return this.URL.concat(`${this._getDictionaryWithRowsByLearnStatus}/${dictionaryId}/${learnStatus}`)
    }

    static getDictionaryViewModelWithRowsViewModel(dictionaryId:string):string{
        return this.URL.concat(`${this._getDictionaryViewModelWithRowsViewModel}/${dictionaryId}`)
    }

    static saveGeneratedTextHistory():string{
        return this.URL.concat(this._saveGeneratedTextHistory)
    }

    static getGeneratedTextHistories():string{
        return this.URL.concat(`${this._getGeneratedTextHistories}`)
    }
}