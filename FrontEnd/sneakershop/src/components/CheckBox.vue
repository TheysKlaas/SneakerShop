<template>
  <div>
    <input
      class="c-sidebar-brands-checkbox"
      :id="`toggle${uniqueID}`"
      type="checkbox"
      @click="filter(name)"
    />
    <label :for="`toggle${uniqueID}`">
      <h3 class="c-device__name">{{ name }}</h3>
    </label>
  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: "CheckBox",
  props: {
    name: String
  },
  //1 action uitvoeren
  created() {
    this.uniqueID = this.createUniqueId();
  },
  methods: {
    filter: function(brand) {
      this.$emit("click");
    },
    createUniqueId: function() {
      const randomId = Math.floor(Math.random() * 100000);
      return randomId;
    }
  }
};
</script>
<style lang="scss" scoped>
@import "@/assets/style/components/form.scss";
</style>
