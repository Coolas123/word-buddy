import {Country} from "../../enums/country";

export class RegisterUser{
    constructor(public userName?: string,
        public country?: Country,
        public password?: string,
        public confirmPassword?: string,
        public email?: string
    ){}
}