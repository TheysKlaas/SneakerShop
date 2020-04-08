<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Row>
      <Container>
        <h1>Our brands:</h1>
      </Container>
    </Row>
    <Container class="o-grid c-brand-cards">
      <div class v-for="brand in brands" :key="brand.companyName">
        <img
          class="c-brand-card-logo"
          :src="require(`../assets/img/${brand.companyName}.png`)"
          alt="Brandimg"
        />
      </div>
    </Container>
  </div>
</template>

<script>
// @ is an alias to /src
import Container from "@/components/layout/Container.vue";
import Row from "@/components/layout/Row.vue";
import Navigation from "@/components/Navigation.vue";
export default {
  name: "brands",
  components: { Container, Row, Navigation },

  computed: {
    brands() {
      return this.$store.getters.brands;
    }
  },
  created() {
    this.$store.dispatch("getBrands");
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/brand.scss";
</style>
