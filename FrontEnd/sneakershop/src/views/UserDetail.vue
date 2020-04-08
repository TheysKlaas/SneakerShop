<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Row>
      <Container>
        <h1>User detail:</h1>
      </Container>
    </Row>
    <Container>
      <table class="c-table">
        <tr>
          <th>Username</th>
          <th>Name</th>
          <th>City</th>
          <th>Country</th>
        </tr>
        <tr class="c-table-row">
          <td>{{user.userName}}</td>
          <td>{{user.firstName}} {{user.lastName}}</td>
          <td>{{user.city}}</td>
          <td>{{user.country}}</td>
        </tr>
      </table>
    </Container>
    <Container class="o-grid c-orders-cards">
      <h1>Orders from {{user.firstName}}:</h1>
      <div class="c-order" v-for="order in orders" :key="order.orderId">
        <div class="c-order-card">
          <div class="c-order-card-title">
            <h2>orderdate: {{(order.timestamp).toString().slice(0,10)}}</h2>
          </div>

          <div class="o-grid u-grid-layout-orderItems">
            <product-card
              v-for="product in order.products"
              :key="product.productID"
              :productId="product.productID"
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
  name: "userDetail",
  components: { Container, Row, Navigation, ProductCard },

  computed: {
    user() {
      return this.$store.getters.user;
    },
    orders() {
      return this.$store.getters.orders;
    }
  },
  created() {
    this.$store.dispatch("getUser", this.$route.params.id);
    this.$store.dispatch("getOrderForUser", this.$route.params.id);
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/table.scss";
@import "@/assets/style/components/order.scss";
</style>
