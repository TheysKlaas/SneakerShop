<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Container>
      <Row class="o-grid u-grid-layout">
        <div class="c-sidebar">
          <div>
            <h2 class="c-sidebar-brands-title">Filter on brand:</h2>
            <check-box
              class="c-sidebar-brands"
              v-for="brand in brands"
              :key="brand.companyName"
              :name="brand.companyName"
              @click="onClickChild(brand.companyName)"
            ></check-box>
          </div>
          <div class="c-sidebar-price">
            <h2>Price range</h2>
            <div class="c-sidebar-price-form">
              <div class="c-sidebar-price-inputs">
                <input class="c-form__input" v-model="from" placeholder="from" type="number" />
                <input class="c-form__input" v-model="to" placeholder="to" type="number" />
              </div>
              <button @click="filterPrice(from,to)" class="c-button c-button__primary">apply</button>
            </div>
          </div>
        </div>
        <div class="c-products">
          <div class="c-products-intro">
            <h1>All Sneakerz</h1>
            <p>
              <span style="color:black;font-weight:bold">Sneakerz</span>
              has a wide collection of shoes. We are
              specialized in sneakers. We have a lot of popular
              brands like: Nike, Adidas, New Balance, Puma, etc...
              Enjoy your online shop experience!
            </p>
          </div>
          <div v-if="filteredproducts !==null" class="o-grid u-grid-layout-products">
            <product-card
              v-for="p in filteredproducts"
              :key="filteredproducts[p]"
              :productId="p.productId"
              :name="p.productName"
              :price="p.unitPrice"
              :supplier="p.supplier.companyName"
            />
          </div>
          <div v-else class="o-grid u-grid-layout-products">
            <product-card
              v-for="p in products"
              :key="products[p]"
              :productId="p.productId"
              :name="p.productName"
              :price="p.unitPrice"
              :supplier="p.supplier.companyName"
            />
          </div>
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
import CheckBox from "@/components/CheckBox.vue";
export default {
  name: "home",
  components: { Container, Row, Navigation, ProductCard, CheckBox },
  data() {
    return {
      checkedBrands: [],
      from: null,
      to: null
    };
  },
  computed: {
    //2render vd state
    brands() {
      return this.$store.getters.brands;
    },
    products() {
      return this.$store.getters.products;
    },
    filteredproducts() {
      return this.$store.getters.filterdProducts;
    }
  },
  created() {
    this.$store.dispatch("getBrands");
    this.$store.dispatch("getProducts");
    console.log("created");
  },
  methods: {
    onClickChild(value) {
      console.log(value);

      var id_tag = value,
        position = this.checkedBrands.indexOf(id_tag);
      if (~position) this.checkedBrands.splice(position, 1);
      else this.checkedBrands.push(id_tag);

      //this.checkedBrands.push(value);
      console.log(this.checkedBrands);
      this.filterProducts(this.checkedBrands);
    },
    productBrandInArray(product) {
      if (this.checkedBrands.indexOf(product.supplier.companyName) !== -1) {
        return product;
      }
    },
    filterProducts(brands) {
      this.from = null;
      this.to = null;
      var filterproducts = this.products.filter(this.productBrandInArray);
      if (filterproducts && filterproducts.length) {
        // not empty
        this.$store.dispatch("updateProducts", filterproducts);
      } else {
        // empty
        this.$store.dispatch("updateProducts", this.products);
      }
    },
    productPriceInRange(product) {
      if (product.unitPrice >= this.from && product.unitPrice <= this.to) {
        return product;
      }
    },
    filterPrice() {
      if (this.checkedBrands && this.checkedBrands.length) {
        // not empty
        var filterdProducts = this.filteredproducts.filter(
          this.productPriceInRange
        );
        this.$store.dispatch("updateProducts", filterdProducts);
      } else {
        // empty
        var filterdProducts = this.products.filter(this.productPriceInRange);
        this.$store.dispatch("updateProducts", filterdProducts);
      }
    }
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/products.scss";
@import "@/assets/style/components/button.scss";
@import "@/assets/style/components/form.scss";
</style>
