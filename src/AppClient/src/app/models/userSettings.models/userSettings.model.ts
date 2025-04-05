import { Country } from "../../enums/country";

export class UserSettings{
    constructor(
        public UserName?: string,
        public Email?: string,
        public Country?: Country,
        public Password: string="",
        public ConfirmPassword: string="",
    ){}
}