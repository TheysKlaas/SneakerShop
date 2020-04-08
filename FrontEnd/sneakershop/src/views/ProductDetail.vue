<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Container>
      <div class="c-productdetail">
        <img
          class="c-card-img"
          :src="
						require(`../assets/img/Shoes/${products.productName}.png`)
					"
          alt="ShoeImg"
        />
        <div class="c-productdetail-info">
          <h1>
            {{ products.supplier.companyName }}
            {{ products.productName }}
          </h1>
          <h1>€ {{ products.unitPrice }}</h1>
          <h1>Size:</h1>
          <div class="radio-toolbar o-grid u-grid-layout-xsmall">
            <input v-model="selectedSize" type="radio" id="size36" name="size" value="36" />
            <label for="size36">36</label>

            <input v-model="selectedSize" type="radio" id="size37" name="size" value="37" />
            <label for="size37">37</label>

            <input v-model="selectedSize" type="radio" id="size38" name="size" value="38" />
            <label for="size38">38</label>

            <input v-model="selectedSize" type="radio" id="size39" name="size" value="39" />
            <label for="size39">39</label>

            <input v-model="selectedSize" type="radio" id="size40" name="size" value="40" />
            <label for="size40">40</label>

            <input v-model="selectedSize" type="radio" id="size41" name="size" value="41" />
            <label for="size41">41</label>

            <input v-model="selectedSize" type="radio" id="size42" name="size" value="42" />
            <label for="size42">42</label>

            <input v-model="selectedSize" type="radio" id="size43" name="size" value="43" />
            <label for="size43">43</label>

            <input v-model="selectedSize" type="radio" id="size44" name="size" value="44" />
            <label for="size44">44</label>

            <input v-model="selectedSize" type="radio" id="size45" name="size" value="45" />
            <label for="size45">45</label>
          </div>
          <button
            @click="order(selectedSize, $route.params.id, products.unitPrice)"
            class="c-button c-button__primary c-button-order"
          >Add to cart</button>
        </div>
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
  name: "productDetail",
  components: { Container, Row, Navigation },
  data() {
    return {
      selectedSize: null,
      display: false
    };
  },
  computed: {
    products() {
      return this.$store.getters.products;
    }
  },
  created() {
    this.$store.dispatch("getProduct", this.$route.params.id);
  },
  methods: {
    order(selectedSize, id, unitPrice) {
      if (!localStorage.isAuth || localStorage.isAuth == false) {
        this.$router.push({ path: "/login" });
      } else {
        const productForBasket = {
          productId: id,
          size: selectedSize,
          price: unitPrice
        };

        this.$store.commit("addProductToBasket", productForBasket);
        this.$router.push({ path: "/basket" });
      }
    }
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/products.scss";
@import "@/assets/style/components/button.scss";
</style>
