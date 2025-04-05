export class UserURLs {
    private static URL="https://localhost:5001"

    private static _register : string ="/user-api/Users/Register" 
    private static _login : string ="/user-api/Users/Login"

    private static _settings : string ="/user-api/Settings"

    private static _dictionaries : string ="/dictionary-api/Dictionaries"
    private static _createDictionary : string ="/dictionary-api/Dictionaries"

    private static _getDictionary : string ="/dictionary-api/Dictionaries"
    private static _updateDictionary : string ="/dictionary-api/Dictionaries"

    private static _GenerateWordContextHub: string ="https://localhost:5031/chathub"
    private static _GenerateWordContext : string = "/ai-api/GenerateWordContext"

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
}