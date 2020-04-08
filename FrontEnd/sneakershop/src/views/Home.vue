<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Row>
      <img class="c-banner" :src="require(`../assets/img/NikeBanner.png`)" alt="Banner" />
    </Row>
    <Container>
      <Row>
        <div class="c-brands">
          <router-link v-for="brand in brands" :key="brand.companyName" :to="`/products`">
            <img
              class="c-brand-logo"
              :src="require(`../assets/img/${brand.companyName}.png`)"
              alt="CompanyImg"
            />
          </router-link>
        </div>
      </Row>
      <Row class="c-new">
        <h1>New in:</h1>
        <div class="o-grid u-grid-layout-products u-grid-layout-products-new">
          <product-card
            v-for="p in products"
            :key="p.productName"
            :productId="p.productId"
            :name="p.productName"
            :price="p.unitPrice"
            :supplier="p.supplier.companyName"
          />
        </div>
      </Row>
    </Container>
  </div>
</template>

<script>
// @ is an alias to /src
import Container from "@/components/layout/Container.vue";
import Row from "@/components/layout/Row.vue";
import Navigation from "@/components/Navigation.vue";
import ProductCard from "@/components/ProductCard.vue";
export default {
  name: "home",
  components: { Container, Row, Navigation, ProductCard },
  computed: {
    //2render vd state
    brands() {
      return this.$store.getters.brands;
    },
    products() {
      return this.$store.getters.products;
    },
    isAuth() {
      return this.$store.getters.isAuth;
    }
  },
  //1 action uitvoeren
  created() {
    this.$store.dispatch("getBrands");

    this.$store.dispatch("getNewProducts");
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/brand.scss";
.c-banner {
  width: 100%;
  margin-bottom: 2rem;
}

.c-new {
  //   border: 1px dashed;
  border-image: none;
  border: 1px dashed black;
  padding: 0 1rem;
  margin: 3rem 0;
}
</style>
