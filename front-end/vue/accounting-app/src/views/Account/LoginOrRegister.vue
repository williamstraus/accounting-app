<template>
    <body>
    <link
        href="https://cdn.jsdelivr.net/gh/williamstraus/scripts@ed8370ed7a5e1804ffdb86988f76d9cc1205e473/login.css"
        rel="stylesheet" type="text/css" />
        <h1 class="heading">Welcome to my accounting app!</h1>
        <div class="container w-container">
            <div class="columns w-row">
                <div class="w-col w-col-6">
                    <h2>Sign up</h2>
                    <h2 style="color: red" v-if="registrationWasOk === false">Bad registration attempt. Check your inputs. Password has to be at least 4 characters long.</h2>
                    <h2 style="color: green" v-if="registrationWasOk === true">Account successfully registered!</h2>
                    <div class="w-form">
                        <form id="register-form" name="register-form" method="post">
                            <label for="name">Name</label>
                            <input class="w-input" v-model="registerInfo.name" type="text" maxlength="256" name="name" placeholder="" id="name" required />
                            <label for="email">Email
                                Address</label>
                            <input class="w-input" v-model="registerInfo.email" type="email" maxlength="256" name="email" placeholder="" id="email" required />
                            <label for="Password">Password</label>
                            <input class="w-input" v-model="registerInfo.password" type="password" maxlength="256" name="Password" placeholder="" id="Password" required />
                            <div>
                                <button class="w-button" @click.prevent="registerOnClick()">Register</button>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="w-col w-col-6">
                    <h2>Log in</h2>
                    <h2 style="color: red" v-if="loginWasOk === false">Bad login attempt</h2>
                    <div class="w-form">
                        <form id="login-form" name="login-form" method="post">
                            <label for="email-2">Email Address</label>
                            <input class="w-input" v-model="loginInfo.email" type="email" maxlength="256" name="email-2" placeholder="" id="email-2" required/>
                            <label for="Password-2">Password</label>
                            <input class="w-input" v-model="loginInfo.password" type="password" maxlength="256" name="Password-2" placeholder="" id="Password-2" required/>
                            <div>
                                <button class="w-button" @click.prevent="loginOnClick()">Log in</button>
                            </div>
                            <h4>If the button doesn't respond, please wait a few seconds and try again.</h4>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </body>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { LoginDTO } from "@/types/LoginDTO";
import { RegisterDTO } from "@/types/RegisterDTO";
import store from "../../store";
import router from "../../router";

@Component
export default class LoginOrRegister extends Vue {
    private loginWasOk: boolean | null = null;
    private registrationWasOk: boolean | null = null;

    private loginInfo: LoginDTO = {
        email: "",
        password: ""
    };

    private registerInfo: RegisterDTO = {
        email: "",
        name: "",
        password: ""
    };

    loginOnClick(): void {
        if (
            this.loginInfo.email.length > 0 &&
            this.loginInfo.password.length > 0
        ) {
            store
                .dispatch("authenticateUser", this.loginInfo)
                .then((isLoggedIn: boolean) => {
                    if (isLoggedIn) {
                        this.loginWasOk = true;
                        router.push("/entries");
                    } else {
                        this.loginWasOk = false;
                    }
                });
        }
    }

    registerOnClick(): void {
        if (
            this.registerInfo.email.length > 0 &&
            this.registerInfo.password.length > 0
        ) {
            store
                .dispatch("registerUser", this.registerInfo)
                .then((isRegistered: boolean) => {
                    if (isRegistered) {
                        this.registrationWasOk = true;
                        router.push("/");
                    } else {
                        this.registrationWasOk = false;
                    }
                });
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
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}
</script>
