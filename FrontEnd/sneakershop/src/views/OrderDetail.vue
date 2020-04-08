<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Row>
      <Container>
        <h1>order detail:</h1>
      </Container>
    </Row>

    <Container class="o-grid c-orders-cards">
      <div class="c-order">
        <div class="c-order-card">
          <div class="c-order-card-title">
            <h2>name customer: {{order.user.firstName}} {{order.user.lastName}}</h2>
            <h2>orderdate: {{(order.timestamp).toString().slice(0,10)}}</h2>
            <h2>order number: {{order.orderId}}</h2>
          </div>

          <div class="o-grid u-grid-layout-orderItems">
            <product-card
              v-for="product in order.products"
              :key="product.product.productId"
              :productId="product.product.productId"
              :name="product.product.productName"
              :price="product.product.unitPrice"
              :supplier="product.product.supplier.companyName"
            />
          </div>
          <div>
            <h2>price: € {{order.totalPrice}}</h2>
          </div>
          <hr />
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
import ProductCard from "@/components/ProductCard.vue";

export default {
  name: "orderDetail",
  components: { Container, Row, Navigation, ProductCard },

  computed: {
    order() {
      return this.$store.getters.order;
    }
  },
  created() {
    this.$store.dispatch("getOrder", this.$route.params.id);
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/order.scss";
</style>
