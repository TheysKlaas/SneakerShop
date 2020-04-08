<template>
  <div>
    <nav class="c-navbar">
      <div class="c-navbar-top">
        <p>Worldwide shipping</p>
        <p>30 days return policy</p>
      </div>
      <Container>
        <div class="c-navbar-center">
          <div class="c-navbar-brand">
            <router-link class="c-navbar-item" :to="`/`">
              <h1 class="title">Sneakerz</h1>
            </router-link>
          </div>
          <div class="c-navbar-menu">
            <router-link class="c-navbar-item" :to="`/products`">
              <h4>All Sneakers</h4>
            </router-link>
            <router-link class="c-navbar-item" :to="`/brands`">
              <h4>Brands</h4>
            </router-link>
          </div>
          <div class="c-navbar-menu" v-if="IsAdmin">
            <router-link class="c-navbar-item" :to="`/allorders`">
              <h4>All Orders</h4>
            </router-link>
            <router-link class="c-navbar-item" :to="`/allusers`">
              <h4>All Users</h4>
            </router-link>
          </div>
          <div class="c-navbar-end" v-if="IsAuth">
            <router-link class="c-navbar-item" :to="`/myorders`">
              <div class="c-navbar-item-icon">
                <img
                  class="c-navbar-item-icon-order"
                  :src="require(`../assets/img/shoeIcon.png`)"
                  alt="navIcon"
                />
                <h4>My orders</h4>
              </div>
            </router-link>
            <router-link class="c-navbar-item" :to="`/basket`">
              <div class="c-navbar-item-icon">
                <img
                  class="c-navbar-item-icon-basket"
                  :src="require(`../assets/img/shoppingcartIcon.png`)"
                  alt="navIcon"
                />
                <h4>Basket</h4>
              </div>
            </router-link>
            <div class="c-navbar-item" @click="logout()">
              <div class="c-navbar-item-icon">
                <img
                  class="c-navbar-item-icon-basket"
                  :src="require(`../assets/img/logoutIcon.png`)"
                  alt="navIcon"
                />
                <h4>Log Out</h4>
              </div>
            </div>
          </div>
          <div v-if="!IsAuth" class="c-navbar-item" @click="login()">
            <div class="c-navbar-item-icon">
              <img
                class="c-navbar-item-icon-basket"
                :src="require(`../assets/img/loginIcon.png`)"
                alt="navicon"
              />
              <h4>Log in</h4>
            </div>
          </div>
        </div>
      </Container>
    </nav>
  </div>
</template>

<script>
import Container from "@/components/layout/Container.vue";
export default {
  name: "Navigation",

  components: { Container },

  data() {
    return {
      IsAuth: localStorage.isAuth,
      IsAdmin: localStorage.isAdmin
    };
  },

  methods: {
    logout() {
      localStorage.removeItem("isAdmin");
      localStorage.removeItem("isAuth");
      localStorage.removeItem("bearer");
      localStorage.removeItem("userId");
      localStorage.removeItem("basket");
      location.reload(true);
    },
    login() {
      this.$router.push({ path: "/login" });
    }
  }
};
</script>

<style scoped lang="scss">
@import "@/assets/style/components/navigation.scss";
</style>
