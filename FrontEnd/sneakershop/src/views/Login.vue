<template>
  <div>
    <Row>
      <Navigation />
    </Row>
    <Row class="c-card__center">
      <CardComponent class="c-card__large" title="Login">
        <form action="post" class="c-form">
          <label class="c-form__label" for="Email">E-Mail</label>
          <input class="c-form__input" type="email" id="Email" v-model="email" required />

          <label class="c-form__label" for="Password">Wachtwoord</label>
          <input class="c-form__input" type="password" id="Password" v-model="password" required />

          <div class="c-errorHandler" v-if="errors.length">
            <p class="c-errorHandler__header">Foutmeldingen:</p>
            <ul class="c-errorHandler__error">
              <p
                class="c-errorHandler__error-item"
                v-for="error in this.errors"
                v-bind:key="errors[error]"
              >- {{ error }}</p>
            </ul>
          </div>
          <p v-bind:class="loginClass">Inloggen ...</p>

          <button type="submit" class="c-button c-button__secondary" v-on:click="validateForm">
            <p class="c-button__secondary-text">Inloggen</p>
          </button>
        </form>

        <router-link class="c-register__link" :to="{path: '/register'}">
          <p>Registreer</p>
        </router-link>
      </CardComponent>
    </Row>
  </div>
</template>

<script>
// @ is an alias to /src
import Row from "@/components/layout/Row.vue";
import CardComponent from "@/components/CardComponent.vue";
import Navigation from "@/components/Navigation.vue";

export default {
  name: "login",

  components: {
    Row,
    CardComponent,
    Navigation
  },

  data() {
    return {
      errors: [],
      email: "",
      password: "",
      loginClass: ""
    };
  },

  computed: {
    isAuth() {
      return this.$store.getters.isAuth;
    }
  },

  created() {
    this.loginClass = "c-errorHandler__login";
  },

  methods: {
    validateForm: async function(e) {
      // 1. Stops default action of an element from happening; for example: prevent a submit button from submitting a form
      e.preventDefault();
      this.errors = [];

      if (this.email == "") {
        this.errors.push("Gelieve een e-mailadres in te vullen.");
      }
      if (this.password == "") {
        this.errors.push("Gelieve een wachtwoord in te vullen.");
      }

      if (this.errors.length == 0) {
        this.login();
      }
    },

    login: async function() {
      this.loginClass = "c-errorHandler__login-show";

      // 3. Make login object for API call
      let payload = {
        userName: this.email,
        password: this.password
      };

      // 4. Call API call 'Login' from store/modules
      this.$store.dispatch("login", payload).then(() => {
        // 4.1. Check if isAuthenticated

        if (this.isAuth == true) {
          // 4.1.1 Navigate to the overview of gates

          this.$router.push({ path: "/" });
        } else {
          // 4.1.2 Generate error-message
          this.loginClass = "c-errorHandler__login";
          this.errors.push("Gelieve correcte gegevens in te geven.");
        }
      });
    }
  }
};
</script>

<style scoped lang="scss">
@import "@/assets/style/components/form.scss";
@import "@/assets/style/components/button.scss";
@import "@/assets/style/components/login.scss";
@import "@/assets/style/components/card.scss";
@import "@/assets/style/components/errorHandler.scss";
</style>
