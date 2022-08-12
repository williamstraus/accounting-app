<template>
  <div id="app">
    <nav>
      <Identity />
    </nav>
    <router-view/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Identity from "./components/Identity.vue";

@Component({
    components: {
        Identity
    }
})
export default class App extends Vue {
}
</script>
