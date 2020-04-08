<template>
  <!-- <router-link :to="`/product/${id}`"> -->
  <div class="c-card" @click="navigateto(productId)">
    <img class="c-card-img" :src="require(`../assets/img/Shoes/${name}.png`)" alt="ShoeImage" />

    <div class="c-card-info">
      <div class="c-card-info-name">{{supplier}} {{name}}</div>
      <div class="c-card-info-price">
        <span style="color:black;font-weight:bold">€ {{price}}</span>
      </div>
    </div>
  </div>
  <!-- </router-link> -->
</template>

<script>
import Container from "@/components/layout/Container.vue";
export default {
  name: "ProductCard",

  components: { Container },

  props: {
    productId: String,
    name: String,
    price: Number,
    supplier: String,
    size: Number
  },
  methods: {
    navigateto(id) {
      this.$router.push({ path: "/product/" + id });
    }
  }
};
</script>

<style scoped lang="scss">
@import "@/assets/style/components/productcard.scss";
</style>
