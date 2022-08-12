<template>
    <body>
        <link
        href="https://cdn.jsdelivr.net/gh/williamstraus/scripts@ed8370ed7a5e1804ffdb86988f76d9cc1205e473/entries.css"
        rel="stylesheet" type="text/css" />
        <div class="container-3 w-container">
            <div
                data-current="Earnings"
                data-easing="ease"
                data-duration-in="300"
                data-duration-out="100"
                class="w-tabs"
            >
                <div class="tabs-menu w-tab-menu">
                    <a
                        data-w-tab="Earnings"
                        class="w-inline-block w-tab-link w--current"
                    >
                        <div>Earnings</div>
                    </a>
                    <a data-w-tab="Expenses" class="w-inline-block w-tab-link">
                        <div>Expenses</div>
                    </a>
                    <a data-w-tab="All" class="w-inline-block w-tab-link">
                        <div>All</div>
                    </a>
                </div>
                <div class="w-tab-content">
                    <div data-w-tab="Earnings" class="w-tab-pane w--tab-active">
                        <h1 v-if="alertExists == true" style="color: red">
                            {{ alert.message }}
                        </h1>
                        <table class="table">
                            <a
                                @click.prevent="showEarningsCreateRow()"
                                type="button"
                                class="btn btn-danger change-button"
                                style="color: blue"
                                href="#"
                                >Create new</a
                            >
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Description</th>
                                    <th>Sum</th>
                                    <th>Type</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="entry in entries">
                                    <template v-if="!entry.isExpense">
                                        <tr
                                            class="ordinary-row"
                                            :key="entry.id"
                                        >
                                            <td>{{ entry.dateOfPayment }}</td>
                                            <td>{{ entry.description }}</td>
                                            <td>{{ entry.sum }}€</td>
                                            <td>{{ entry.end }}</td>
                                            <td>Income</td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        showEditRow(
                                                            $event,
                                                            entry
                                                        )
                                                    "
                                                    type="button"
                                                    class="
                                                        btn btn-danger
                                                        change-button
                                                    "
                                                    style="color: blue"
                                                    href
                                                    >Change</a
                                                >
                                            </td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        deleteOnClick(entry)
                                                    "
                                                    type="button"
                                                    class="
                                                        btn btn-danger
                                                        change-button
                                                    "
                                                    style="color: blue"
                                                    href
                                                    >Delete</a
                                                >
                                            </td>
                                        </tr>
                                        <tr
                                            hidden
                                            class="edit-row"
                                            :key="entry.id"
                                        >
                                            <td>
                                                <input
                                                    v-model="
                                                        entryInfo.dateOfPayment
                                                    "
                                                    type="text"
                                                    class="w-input"
                                                    name="dateOfPayment"
                                                    :placeholder="
                                                        entry.dateOfPayment
                                                    "
                                                    id="dateOfPayment"
                                                    required
                                                />
                                            </td>
                                            <td>
                                                <input
                                                    v-model="
                                                        entryInfo.description
                                                    "
                                                    type="text"
                                                    class="w-input"
                                                    name="description"
                                                    :placeholder="
                                                        entry.description
                                                    "
                                                    id="description"
                                                    required
                                                />
                                            </td>
                                            <td>
                                                <input
                                                    v-model="entryInfo.sum"
                                                    type="number"
                                                    class="w-input"
                                                    name="sum"
                                                    :placeholder="entry.sum"
                                                    id="sum"
                                                    required
                                                />
                                            </td>
                                            <td>
                                                <select
                                                    @change.prevent="
                                                        checkTypeOfEntry()
                                                    "
                                                    v-model="typeOfEntry"
                                                    name="typeOfEntry"
                                                    id="typeOfEntry"
                                                >
                                                    <option
                                                        value="expense"
                                                        selected
                                                    >
                                                        Expense
                                                    </option>
                                                    <option value="income">
                                                        Income
                                                    </option>
                                                </select>
                                            </td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        updateEntry(entry);
                                                        hideEditRow($event);
                                                    "
                                                    type="button"
                                                    class="btn btn-danger"
                                                    style="color: blue"
                                                    href
                                                    >Confirm</a
                                                >
                                            </td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        hideEditRow($event)
                                                    "
                                                    type="button"
                                                    class="btn btn-danger"
                                                    style="color: blue"
                                                    href
                                                    >Cancel</a
                                                >
                                            </td>
                                        </tr>
                                    </template>
                                </template>
                                <tr hidden id="earnings-create-row">
                                    <td>
                                        <input
                                            v-model="
                                                entryCreateInfo.dateOfPayment
                                            "
                                            type="text"
                                            class="w-input"
                                            name="dateOfPayment"
                                            placeholder="01.01.2011"
                                            id="dateOfPayment"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <input
                                            v-model="
                                                entryCreateInfo.description
                                            "
                                            type="text"
                                            class="w-input"
                                            name="description"
                                            placeholder="Description"
                                            id="description"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <input
                                            v-model="entryCreateInfo.sum"
                                            type="number"
                                            class="w-input"
                                            name="sum"
                                            placeholder="Sum"
                                            id="sum"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <select
                                            @change.prevent="checkTypeOfEntry()"
                                            v-model="typeOfEntry"
                                            name="typeOfEntry"
                                            id="typeOfEntry"
                                        >
                                            <option value="expense" selected>
                                                Expense
                                            </option>
                                            <option value="income">
                                                Income
                                            </option>
                                        </select>
                                    </td>
                                    <td>
                                        <a
                                            @click.prevent="
                                                createEntry();
                                                hideEarningsCreateRow();
                                            "
                                            type="button"
                                            class="btn btn-danger"
                                            style="color: blue"
                                            href
                                            >Create</a
                                        >
                                    </td>
                                    <td>
                                        <a
                                            @click.prevent="
                                                hideEarningsCreateRow()
                                            "
                                            type="button"
                                            class="btn btn-danger"
                                            style="color: blue"
                                            href
                                            >Cancel</a
                                        >
                                    </td>
                                </tr>
                                <tr></tr>
                                <tr
                                    v-if="
                                        Array.isArray(entries) &&
                                        entries.length > 0
                                    "
                                >
                                    <td>Total:</td>
                                    <td></td>
                                    <td>{{ getTotalSum("earnings") }}€</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div data-w-tab="Expenses" class="w-tab-pane">
                        <h1 v-if="alertExists == true" style="color: red">
                            {{ alert.message }}
                        </h1>
                        <table class="table">
                            <a
                                @click.prevent="showExpensesCreateRow()"
                                type="button"
                                class="btn btn-danger change-button"
                                style="color: blue"
                                href="#"
                                >Create new</a
                            >
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Description</th>
                                    <th>Sum</th>
                                    <th>Type</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="entry in entries">
                                    <template v-if="entry.isExpense">
                                        <tr
                                            class="ordinary-row"
                                            :key="entry.id"
                                        >
                                            <td>{{ entry.dateOfPayment }}</td>
                                            <td>{{ entry.description }}</td>
                                            <td>-{{ entry.sum }}€</td>
                                            <td>{{ entry.end }}</td>
                                            <td>Expense</td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        showEditRow(
                                                            $event,
                                                            entry
                                                        )
                                                    "
                                                    type="button"
                                                    class="
                                                        btn btn-danger
                                                        change-button
                                                    "
                                                    style="color: blue"
                                                    href
                                                    >Change</a
                                                >
                                            </td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        deleteOnClick(entry)
                                                    "
                                                    type="button"
                                                    class="
                                                        btn btn-danger
                                                        change-button
                                                    "
                                                    style="color: blue"
                                                    href
                                                    >Delete</a
                                                >
                                            </td>
                                        </tr>
                                        <tr
                                            hidden
                                            class="edit-row"
                                            :key="entry.id"
                                        >
                                            <td>
                                                <input
                                                    v-model="
                                                        entryInfo.dateOfPayment
                                                    "
                                                    type="text"
                                                    class="w-input"
                                                    name="dateOfPayment"
                                                    :placeholder="
                                                        entry.dateOfPayment
                                                    "
                                                    id="dateOfPayment"
                                                    required
                                                />
                                            </td>
                                            <td>
                                                <input
                                                    v-model="
                                                        entryInfo.description
                                                    "
                                                    type="text"
                                                    class="w-input"
                                                    name="description"
                                                    :placeholder="
                                                        entry.description
                                                    "
                                                    id="description"
                                                    required
                                                />
                                            </td>
                                            <td>
                                                <input
                                                    v-model="entryInfo.sum"
                                                    type="number"
                                                    class="w-input"
                                                    name="sum"
                                                    :placeholder="entry.sum"
                                                    id="sum"
                                                    required
                                                />
                                            </td>
                                            <td>
                                                <select
                                                    @change.prevent="
                                                        checkTypeOfEntry()
                                                    "
                                                    v-model="typeOfEntry"
                                                    name="typeOfEntry"
                                                    id="typeOfEntry"
                                                >
                                                    <option
                                                        value="expense"
                                                        selected
                                                    >
                                                        Expense
                                                    </option>
                                                    <option value="income">
                                                        Income
                                                    </option>
                                                </select>
                                            </td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        updateEntry(entry);
                                                        hideEditRow($event);
                                                    "
                                                    type="button"
                                                    class="btn btn-danger"
                                                    style="color: blue"
                                                    href
                                                    >Confirm</a
                                                >
                                            </td>
                                            <td>
                                                <a
                                                    @click.prevent="
                                                        hideEditRow($event)
                                                    "
                                                    type="button"
                                                    class="btn btn-danger"
                                                    style="color: blue"
                                                    href
                                                    >Cancel</a
                                                >
                                            </td>
                                        </tr>
                                    </template>
                                </template>
                                <tr hidden id="expenses-create-row">
                                    <td>
                                        <input
                                            v-model="
                                                entryCreateInfo.dateOfPayment
                                            "
                                            type="text"
                                            class="w-input"
                                            name="dateOfPayment"
                                            placeholder="01.01.2011"
                                            id="dateOfPayment"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <input
                                            v-model="
                                                entryCreateInfo.description
                                            "
                                            type="text"
                                            class="w-input"
                                            name="description"
                                            placeholder="Description"
                                            id="description"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <input
                                            v-model="entryCreateInfo.sum"
                                            type="number"
                                            class="w-input"
                                            name="sum"
                                            placeholder="Sum"
                                            id="sum"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <select
                                            @change.prevent="checkTypeOfEntry()"
                                            v-model="typeOfEntry"
                                            name="typeOfEntry"
                                            id="typeOfEntry"
                                        >
                                            <option value="expense" selected>
                                                Expense
                                            </option>
                                            <option value="income">
                                                Income
                                            </option>
                                        </select>
                                    </td>
                                    <td>
                                        <a
                                            @click.prevent="
                                                createEntry();
                                                hideExpensesCreateRow();
                                            "
                                            type="button"
                                            class="btn btn-danger"
                                            style="color: blue"
                                            href
                                            >Create</a
                                        >
                                    </td>
                                    <td>
                                        <a
                                            @click.prevent="
                                                hideExpensesCreateRow()
                                            "
                                            type="button"
                                            class="btn btn-danger"
                                            style="color: blue"
                                            href
                                            >Cancel</a
                                        >
                                    </td>
                                </tr>
                                <tr></tr>
                                <tr
                                    v-if="
                                        Array.isArray(entries) &&
                                        entries.length > 0
                                    "
                                >
                                    <td>Total:</td>
                                    <td></td>
                                    <td>{{ getTotalSum("expenses") }}€</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div data-w-tab="All" class="w-tab-pane">
                        <h1 v-if="alertExists == true" style="color: red">
                            {{ alert.message }}
                        </h1>
                        <table class="table">
                            <a
                                @click.prevent="showAllCreateRow()"
                                type="button"
                                class="btn btn-danger change-button"
                                style="color: blue"
                                href="#"
                                >Create new</a
                            >
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Description</th>
                                    <th>Sum</th>
                                    <th>Type</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="entry in entries">
                                    <tr class="ordinary-row" :key="entry.id">
                                        <td>{{ entry.dateOfPayment }}</td>
                                        <td>{{ entry.description }}</td>
                                        <td v-if="entry.isExpense">
                                            -{{ entry.sum }}€
                                        </td>
                                        <td v-else-if="!entry.isExpense">
                                            {{ entry.sum }}€
                                        </td>
                                        <td>{{ entry.sum }}€</td>
                                        <td>{{ entry.end }}</td>
                                        <td v-if="entry.isExpense">Expense</td>
                                        <td v-else-if="!entry.isExpense">
                                            Income
                                        </td>
                                        <td>
                                            <a
                                                @click.prevent="
                                                    showEditRow($event, entry)
                                                "
                                                type="button"
                                                class="
                                                    btn btn-danger
                                                    change-button
                                                "
                                                style="color: blue"
                                                href
                                                >Change</a
                                            >
                                        </td>
                                        <td>
                                            <a
                                                @click.prevent="
                                                    deleteOnClick(entry)
                                                "
                                                type="button"
                                                class="
                                                    btn btn-danger
                                                    change-button
                                                "
                                                style="color: blue"
                                                href
                                                >Delete</a
                                            >
                                        </td>
                                    </tr>
                                    <tr hidden class="edit-row" :key="entry.id">
                                        <td>
                                            <input
                                                v-model="
                                                    entryInfo.dateOfPayment
                                                "
                                                type="text"
                                                class="w-input"
                                                name="dateOfPayment"
                                                :placeholder="
                                                    entry.dateOfPayment
                                                "
                                                id="dateOfPayment"
                                                required
                                            />
                                        </td>
                                        <td>
                                            <input
                                                v-model="entryInfo.description"
                                                type="text"
                                                class="w-input"
                                                name="description"
                                                :placeholder="entry.description"
                                                id="description"
                                                required
                                            />
                                        </td>
                                        <td>
                                            <input
                                                v-model="entryInfo.sum"
                                                type="number"
                                                class="w-input"
                                                name="sum"
                                                :placeholder="entry.sum"
                                                id="sum"
                                                required
                                            />
                                        </td>
                                        <td>
                                            <select
                                                @change.prevent="
                                                    checkTypeOfEntry()
                                                "
                                                v-model="typeOfEntry"
                                                name="typeOfEntry"
                                                id="typeOfEntry"
                                            >
                                                <option
                                                    value="expense"
                                                    selected
                                                >
                                                    Expense
                                                </option>
                                                <option value="income">
                                                    Income
                                                </option>
                                            </select>
                                        </td>
                                        <td>
                                            <a
                                                @click.prevent="
                                                    updateEntry(entry);
                                                    hideEditRow($event);
                                                "
                                                type="button"
                                                class="btn btn-danger"
                                                style="color: blue"
                                                href
                                                >Confirm</a
                                            >
                                        </td>
                                        <td>
                                            <a
                                                @click.prevent="
                                                    hideEditRow($event)
                                                "
                                                type="button"
                                                class="btn btn-danger"
                                                style="color: blue"
                                                href
                                                >Cancel</a
                                            >
                                        </td>
                                    </tr>
                                </template>
                                <tr hidden id="all-create-row">
                                    <td>
                                        <input
                                            v-model="
                                                entryCreateInfo.dateOfPayment
                                            "
                                            type="text"
                                            class="w-input"
                                            name="dateOfPayment"
                                            placeholder="01.01.2011"
                                            id="dateOfPayment"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <input
                                            v-model="
                                                entryCreateInfo.description
                                            "
                                            type="text"
                                            class="w-input"
                                            name="description"
                                            placeholder="Description"
                                            id="description"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <input
                                            v-model="entryCreateInfo.sum"
                                            type="number"
                                            class="w-input"
                                            name="sum"
                                            placeholder="Sum"
                                            id="sum"
                                            required
                                        />
                                    </td>
                                    <td>
                                        <select
                                            @change.prevent="checkTypeOfEntry()"
                                            v-model="typeOfEntry"
                                            name="typeOfEntry"
                                            id="typeOfEntry"
                                        >
                                            <option value="expense" selected>
                                                Expense
                                            </option>
                                            <option value="income">
                                                Income
                                            </option>
                                        </select>
                                    </td>
                                    <td>
                                        <a
                                            @click.prevent="
                                                createEntry();
                                                hideAllCreateRow();
                                            "
                                            type="button"
                                            class="btn btn-danger"
                                            style="color: blue"
                                            href
                                            >Create</a
                                        >
                                    </td>
                                    <td>
                                        <a
                                            @click.prevent="hideAllCreateRow()"
                                            type="button"
                                            class="btn btn-danger"
                                            style="color: blue"
                                            href
                                            >Cancel</a
                                        >
                                    </td>
                                </tr>
                                <tr></tr>
                                <tr
                                    v-if="
                                        Array.isArray(entries) &&
                                        entries.length > 0
                                    "
                                >
                                    <td>Total:</td>
                                    <td></td>
                                    <td>{{ getTotalSum("all") }}€</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </body>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import router from "../../router";
import { Entry } from "../../domain/Entry";
import { EntryEdit } from "@/domain/EntryEdit";
import { EntryCreate } from "@/domain/EntryCreate";
import store from "../../store";
import { FetchResponse } from "@/types/FetchResponse";
import { AlertType } from "./../../types/AlertType";
import { AlertData } from "../../types/AlertData";

@Component
export default class EntriesIndex extends Vue {
    private alert: AlertData | null = null;
    private alertExists = false;
    private typeOfEntry = "expense";

    private entryInfo: EntryEdit = {
        id: "",
        dateOfPayment: "",
        sum: 0,
        description: "",
        isExpense: false
    };

    private entryCreateInfo: EntryCreate = {
        dateOfPayment: "01.01.2011",
        sum: 0,
        description: "",
        isExpense: false
    };

    get entries(): Entry[] | undefined {
        return store.state.entries?.data;
    }

    get entriesFetch(): FetchResponse<Entry[]> | null {
        return store.state.entries;
    }

    deleteOnClick(session: Entry): void {
        store
            .dispatch("deleteEntry", session.id)
            .then((result: FetchResponse<string>) => {
                if (result.statusCode >= 200 && result.statusCode < 300) {
                    router.push("/entries");
                } else {
                    this.alert = {
                        message:
                            result.statusCode.toString() +
                            " - " +
                            result.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true
                    };
                    this.alertExists = true;
                }
            });
    }

    updateEntry(): void {
        this.entryInfo.sum = Number(this.entryInfo.sum);
        store
            .dispatch("updateEntry", this.entryInfo)
            .then((result: FetchResponse<string>) => {
                if (result.statusCode >= 200 && result.statusCode < 300) {
                    router.push("/entries");
                } else {
                    this.alert = {
                        message:
                            result.statusCode.toString() +
                            " - " +
                            result.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true
                    };
                    this.alertExists = true;
                }
            });
    }

    checkTypeOfEntry(): void {
        if (this.typeOfEntry === "expense") {
            this.entryCreateInfo.isExpense = true;
            this.entryInfo.isExpense = true;
        } else if (this.typeOfEntry === "income") {
            this.entryCreateInfo.isExpense = false;
            this.entryInfo.isExpense = false;
        }
    }

    createEntry(): void {
        this.entryCreateInfo.sum = Number(this.entryCreateInfo.sum);

        if (
            this.entryCreateInfo.sum !== 0 &&
            this.entryCreateInfo.description.length > 0
        ) {
            store
                .dispatch("createEntry", this.entryCreateInfo)
                .then((result: FetchResponse<string>) => {
                    if (result.statusCode >= 200 && result.statusCode < 300) {
                        router.push("/entries");
                    } else {
                        this.alert = {
                            message:
                                result.statusCode.toString() +
                                " - " +
                                result.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true
                        };
                        this.alertExists = true;
                    }
                });
        } else {
            this.alert = {
                message:
                    "Creation failed! Sum has to be over 0 and description has to contain a text.",
                type: AlertType.Danger,
                dismissable: true
            };
            this.alertExists = true;
        }
    }

    getTotalSum(type: string): number {
        let totalSum = 0;
        if (type === "earnings") {
            for (const entry of this.entries as Entry[]) {
                if (!entry.isExpense) {
                    totalSum = totalSum + entry.sum;
                }
            }
        }
        if (type === "expenses") {
            for (const entry of this.entries as Entry[]) {
                if (entry.isExpense) {
                    totalSum = totalSum - entry.sum;
                }
            }
        }
        if (type === "all") {
            for (const entry of this.entries as Entry[]) {
                if (entry.isExpense) {
                    totalSum = totalSum - entry.sum;
                } else if (!entry.isExpense) {
                    totalSum = totalSum + entry.sum;
                }
            }
        }
        return totalSum;
    }

    showEditRow(event: Event, entry: Entry): void {
        const parent: any = (event.target as HTMLElement).parentElement
            ?.parentElement?.parentElement;
        const editRow: any = parent.querySelector(".edit-row");
        const ordinaryRow: any = parent.querySelector(".ordinary-row");
        editRow.hidden = "";
        ordinaryRow.hidden = "hidden";

        this.entryInfo.id = entry.id;
        this.entryInfo.dateOfPayment = entry.dateOfPayment;

        this.entryInfo.sum = entry.sum;
        this.entryInfo.description = entry.description;
        this.entryInfo.isExpense = entry.isExpense;

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "hidden";
        }
    }

    hideEditRow(event: Event): void {
        const parent: any = (event.target as HTMLElement).parentElement
            ?.parentElement?.parentElement;
        const editRow: any = parent.querySelector(".edit-row");
        const ordinaryRow: any = parent.querySelector(".ordinary-row");
        editRow.hidden = "hidden";
        ordinaryRow.hidden = "";

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "";
        }
    }

    showEarningsCreateRow(): void {
        const createRow: any = document.getElementById("earnings-create-row");
        createRow.hidden = "";

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "hidden";
        }
    }

    hideEarningsCreateRow(): void {
        const createRow: any = document.getElementById("earnings-create-row");
        createRow.hidden = "hidden";

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "";
        }
    }

    showExpensesCreateRow(): void {
        const createRow: any = document.getElementById("expenses-create-row");
        createRow.hidden = "";

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "hidden";
        }
    }

    hideExpensesCreateRow(): void {
        const createRow: any = document.getElementById("expenses-create-row");
        createRow.hidden = "hidden";

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "";
        }
    }

    showAllCreateRow(): void {
        const createRow: any = document.getElementById("all-create-row");
        createRow.hidden = "";

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "hidden";
        }
    }

    hideAllCreateRow(): void {
        const createRow: any = document.getElementById("all-create-row");
        createRow.hidden = "hidden";

        const changeButtons: any = document.querySelectorAll(".change-button");
        for (let i = 0; i < changeButtons.length; i++) {
            changeButtons[i].hidden = "";
        }
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        console.log("mounted");

        let plugin = document.createElement("script");
        plugin.setAttribute(
            "src",
            "https://d3e54v103j8qbb.cloudfront.net/js/jquery-3.5.1.min.dc5e7f18c8.js?site=62de7053e724d7516164cb68"
        );
        plugin.async = true;
        document.body.appendChild(plugin);

        plugin = document.createElement("script");
        plugin.setAttribute(
            "src",
            "https://cdn.jsdelivr.net/gh/williamstraus/scripts@ed8370ed7a5e1804ffdb86988f76d9cc1205e473/entries.js"
        );
        plugin.async = true;
        document.body.appendChild(plugin);

        store.dispatch("getEntries");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
        if (
            this.entriesFetch!.statusCode >= 200 &&
            this.entriesFetch!.statusCode < 300
        ) {
            this.alert = null;
        } else {
            this.alert = {
                message:
                    this.entriesFetch!.statusCode.toString() +
                    " - " +
                    this.entriesFetch!.errorMessage,
                type: AlertType.Danger,
                dismissable: true
            };
            this.alertExists = true;
        }

        this.checkTypeOfEntry();
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        store.dispatch("clearEntries");
        console.log("destroyed");
    }
}
</script>
