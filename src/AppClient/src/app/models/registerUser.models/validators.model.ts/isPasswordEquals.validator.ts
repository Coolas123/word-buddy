import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const isPasswordEqualsValidator: ValidatorFn = (
    control: AbstractControl,
  ): ValidationErrors | null => {
    const password = control.parent?.get('Password');
    const confirmPassword = control.parent?.get('ConfirmPassword');
    return password?.value !== confirmPassword?.value ? { passwortMatch: true } : null
  };