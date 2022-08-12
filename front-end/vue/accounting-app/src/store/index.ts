import Vue from 'vue'
import Vuex from 'vuex'
import { Entry } from '@/domain/Entry';
import { EntryCreate } from '@/domain/EntryCreate';
import { EntryEdit } from '@/domain/EntryEdit';
import { EntriesApi } from '@/services/EntriesApi';
import { AccountApi } from '@/services/AccountApi';
import { LoginDTO } from '@/types/LoginDTO';
import { RegisterDTO } from '@/types/RegisterDTO';
import { FetchResponse } from '@/types/FetchResponse';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
        entries: null as FetchResponse<Entry[]> | null
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        }
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },
        setEntries(state, entries: FetchResponse<Entry[]>) {
            state.entries = entries;
        }
    },
    actions: {
        clearJwt(context): void {
            context.commit('setJwt', null);
        },
        clearEntries(context): void {
            context.commit('setEntries', null);
        },
        async authenticateUser(context, loginDTO: LoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async registerUser(context, registerDTO: RegisterDTO): Promise<boolean> {
            const result = await AccountApi.registerUser(registerDTO);
            await context.dispatch('getEntries');
            console.log('registerUser: ', registerDTO.name);
            return result;
        },
        async getEntries(context): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const entries = await EntriesApi.getAll(context.state.jwt);
                context.commit('setEntries', entries);
            }
        },
        async createEntry(context, entryInfo: EntryCreate): Promise<FetchResponse<string> | undefined> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const result = await EntriesApi.create(entryInfo, context.state.jwt);
                await context.dispatch('getEntries');
                console.log('createEntry: ', entryInfo.description);
                return result;
            }
        },
        async updateEntry(context, entryInfo: EntryEdit): Promise<FetchResponse<string> | undefined> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const result = await EntriesApi.update(entryInfo, context.state.jwt);
                await context.dispatch('getEntries');
                console.log('updateEntry: ', entryInfo.description);
                return result;
            }
        },
        async deleteEntry(context, id: string): Promise<FetchResponse<string> | undefined> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const result = await EntriesApi.delete(id, context.state.jwt);
                await context.dispatch('getEntries');
                console.log('deleteEntry: ', id);
                return result;
            }
        }
    },
    modules: {
    }
})
