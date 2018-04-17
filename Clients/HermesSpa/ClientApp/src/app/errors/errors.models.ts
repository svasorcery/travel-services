import { HttpErrorResponse } from "@angular/common/http";

export class ApiError {
    public code: string = '00000';
    public message: string = 'Во время обработки Вашего запроса возникла проблема.';
    public detail: string = `
        Попробуйте повторить запрос через несколько минут.
        Если ошибка повторилась, обратитесь в Службу поддержки.`;

    constructor(code: number | string, message: string, detail: string) {
        this.code = code.toString();
        this.message = message;
        this.detail = detail;
    }

    public static fromHttpErrorResponse(error: HttpErrorResponse): ApiError {
        return new ApiError(error.status, error.statusText, error.message)
    }
}
