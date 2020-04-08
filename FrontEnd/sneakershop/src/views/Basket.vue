<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Container>
      <h1>These products are in your card:</h1>
      <div class="c-basket" v-if="basketProducts != 0">
        <div class="c-basket-item" v-for="p in basketProducts" :key="p.productName">
          <img
            class="c-basket-item-img"
            :src="require(`../assets/img/Shoes/${p.productName}.png`)"
            alt="ShoeImg"
          />
          <h2>{{p.supplier.companyName}} {{p.productName}}</h2>
          <h2>€ {{p.unitPrice}}</h2>
        </div>
        <button @click="order()" class="c-button c-button__primary c-button-order">Order now</button>
        <button @click="clear()" class="c-button c-button__secondary c-button-clear">
          <p class="c-button__secondary-text">Clear basket</p>
        </button>
      </div>
      <div v-else>no products yet</div>
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
  name: "basket",
  components: { Container, Row, Navigation, ProductCard },

  computed: {
    basketProducts() {
      return this.$store.getters.basketProducts;
    }
  },
  created() {
    this.$store.dispatch("getProductsFromBasket");
  },
  methods: {
    order() {
      this.$store.dispatch("placeOrder", localStorage.userId);
      this.$router.push({ path: "/myorders" });
    },
    clear() {
      localStorage.removeItem("basket");
      location.reload(true);
    }
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/basket.scss";
@import "@/assets/style/components/button.scss";
</style>
