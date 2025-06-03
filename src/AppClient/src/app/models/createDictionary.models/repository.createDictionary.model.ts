// import { Injectable } from "@angular/core"
// import { RestDataSource } from "../rest.datasource"
// import { UserURLs } from "../../common/gateways"
// import { CreateDictionaryAndRowsCommand } from "./createDictionary.model"
// import { Observable } from "rxjs/internal/Observable"

// @Injectable({
//     providedIn: 'root'
// })
// export class Model {
//     constructor(public dataSource: RestDataSource<CreateDictionaryAndRowsCommand>) {
//     }

//     public saveDictionary(newDictionary: CreateDictionaryAndRowsCommand): Observable<any> {
//         return this.dataSource.saveData(UserURLs.createDictionaryURI(),newDictionary)
//     }
// }