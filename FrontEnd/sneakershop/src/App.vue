<template>
  <div id="app">
    <router-view class="c-content" />
    <app-footer />
  </div>
</template>

<script>
import Container from "@/components/layout/Container.vue";
import Row from "@/components/layout/Row.vue";
import AppFooter from "@/components/AppFooter";

export default {
  name: "app",

  components: {
    Container,
    Row,
    AppFooter
  }
};
</script>

<style lang="scss">
@import "@/assets/style/screen.scss";
.c-content {
  height: 100vh;
}
</style>
