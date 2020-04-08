<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Row>
      <Container>
        <h1>All customers:</h1>
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
        <tr
          class="c-table-row"
          v-for="user in allUsers"
          :key="user.id"
          @click="userDetail(user.id)"
        >
          <td>{{user.userName}}</td>
          <td>{{user.firstName}} {{user.lastName}}</td>
          <td>{{user.city}}</td>
          <td>{{user.country}}</td>
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
export default {
  name: "allusers",
  components: { Container, Row, Navigation },

  computed: {
    allUsers() {
      return this.$store.getters.allUsers;
    }
  },
  created() {
    this.$store.dispatch("getAllUsers");
  },
  methods: {
    userDetail(userId) {
      this.$router.push({ path: "/user/" + userId });
    }
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/table.scss";
</style>
