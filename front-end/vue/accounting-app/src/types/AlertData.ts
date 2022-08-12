import { AlertType } from "./AlertType";

export interface AlertData {
    message: string;
    dismissable?: boolean;
    type: AlertType
}
