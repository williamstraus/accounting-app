<template>
  <div>
    <link
        href="https://cdn.jsdelivr.net/gh/williamstraus/scripts@ed8370ed7a5e1804ffdb86988f76d9cc1205e473/identity.css"
        rel="stylesheet" type="text/css" />
    <div v-if="isAuthenticated" data-animation="default" data-collapse="medium" data-duration="400" data-easing="ease" data-easing2="ease"
        role="banner" class="w-nav">
        <div class="container-2 w-container">
            <nav role="navigation" class="w-nav-menu"><a @click="logoutOnClick" href="#" class="button w-button">Log out</a></nav>
            <nav role="navigation" class="nav-menu w-nav-menu">
                <div>Logged in as {{userEmail}}</div>
            </nav>
        </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../store";
import router from "../router";
import JwtDecode from "jwt-decode";

@Component
export default class Identity extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userEmail(): string {
        if (store.state.jwt) {
            const decoded = JwtDecode(store.state.jwt) as Record<
                string,
                string
            >;
            return decoded[
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            ];
        }
        return "null";
    }

    logoutOnClick(): void {
        store.dispatch("clearJwt");
        store.dispatch("clearEntries");
        router.push("/");
    }
}
</script>
