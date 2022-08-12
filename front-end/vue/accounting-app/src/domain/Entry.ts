export interface Entry {
    id: string;
    createdAt: Date;
    modifiedAt: Date;
    deletedAt: Date;
    dateOfPayment: string;
    sum: number;
    description: string;
    isExpense: boolean;
}
