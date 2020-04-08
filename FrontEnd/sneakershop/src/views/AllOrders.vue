<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Row>
      <Container>
        <h1>All orders:</h1>
      </Container>
    </Row>
    <Container>
      <table class="c-table">
        <tr>
          <th>Order number</th>
          <th>Order date</th>
          <th>User</th>
          <th>Total price</th>
        </tr>
        <tr
          class="c-table-row"
          v-for="order in allOrders"
          :key="order.orderId"
          @click="orderDetail(order.orderId)"
        >
          <td>{{order.orderId}}</td>
          <td>{{(order.timestamp).toString().slice(0,10)}}</td>
          <td>{{order.user.firstName}} {{order.user.lastName}}</td>
          <td>{{order.totalPrice}}</td>
        </tr>
      </table>
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
  name: "allorders",
  components: { Container, Row, Navigation, ProductCard },

  computed: {
    allOrders() {
      return this.$store.getters.allOrders;
    }
  },
  created() {
    this.$store.dispatch("getallorders");
  },
  methods: {
    orderDetail(orderId) {
      this.$router.push({ path: "/orderDetail/" + orderId });
    }
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/order.scss";
@import "@/assets/style/components/table.scss";
</style>
