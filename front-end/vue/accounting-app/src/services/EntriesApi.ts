import Axios from 'axios';
import { Entry } from '@/domain/Entry';
import { EntryCreate } from '@/domain/EntryCreate';
import { EntryEdit } from '@/domain/EntryEdit';
import { FetchResponse } from '@/types/FetchResponse';

export abstract class EntriesApi {
    private static axios = Axios.create(
        {
            baseURL: "https://accountingappws.azurewebsites.net/api/entries/",
            headers: {
                'Content-Type': 'application/json'
            }
        }
    )

    static async getAll(jwt: string): Promise<FetchResponse<Entry[]>> {
        const url = "";
        try {
            const response = await this.axios.get<Entry[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('getAll response', response);
            if (response.status === 200) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(error)
            };
        }
    }

    static async get(id: string, jwt: string): Promise<FetchResponse<Entry[]>> {
        const url = "" + id;
        try {
            const response = await this.axios.get<Entry[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('getAll response', response);
            if (response.status === 200) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(error)
            };
        }
    }

    static async delete(id: string, jwt: string): Promise<FetchResponse<string>> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<Entry>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return {
                    statusCode: response.status
                };
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(error)
            };
        }
    }

    static async create(entryInfo: EntryCreate, jwt: string): Promise<FetchResponse<string>> {
        const url = "";
        try {
            const response = await this.axios.post<EntryCreate>(url, JSON.stringify(entryInfo), { headers: { Authorization: ('Bearer ' + jwt), "Content-Type": "application/json" } })
            if (response.status >= 200 && response.status < 300) {
                console.log('response', response);
                return {
                    statusCode: response.status
                };
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(error)
            };
        }
    }

    static async update(entryInfo: EntryEdit, jwt: string): Promise<FetchResponse<string>> {
        const url = "" + entryInfo.id;

        try {
            const response = await this.axios.put<EntryEdit>(url, JSON.stringify(entryInfo), { headers: { Authorization: ('Bearer ' + jwt), "Content-Type": "application/json" } })
            if (response.status >= 200 && response.status < 300) {
                console.log('response', response);
                return {
                    statusCode: response.status
                };
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(error)
            };
        }
    }
}
