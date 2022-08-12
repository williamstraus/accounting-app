import { LoginDTO } from '../types/LoginDTO';
import Axios from 'axios';
import { RegisterDTO } from '@/types/RegisterDTO';

interface LoginResponse {
    token: string;
    status: string;
}
export abstract class AccountApi {
    private static axios = Axios.create(
        {
            baseURL: "https://accountingappws.azurewebsites.net/api/",
            headers: {
                'Content-Type': 'application/json'
            }
        }
    )

    static async getJwt(loginDTO: LoginDTO): Promise<string | null> {
        const url = "account/login";
        try {
            const response = await this.axios.post<LoginResponse>(url, loginDTO);
            console.log('getJwt response', response);
            if (response.status === 200) {
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async registerUser(registerDTO: RegisterDTO): Promise<boolean> {
        const url = "account/register";
        try {
            const response = await this.axios.post<RegisterDTO>(url, registerDTO);
            if (response.status === 200) {
                console.log('response', response);
                return true;
            }
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return false;
        }
        return false;
    }
}
