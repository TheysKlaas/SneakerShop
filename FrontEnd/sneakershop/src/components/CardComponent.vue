<template>
  <div class="c-card">
    <div class="c-card__header">
      <p class="c-card__header-title">{{ title }}</p>
    </div>

    <slot></slot>
  </div>
</template>

<script>
export default {
  name: "CardComponent",
  props: {
    title: String,
    callGateObj: Object,
    showCall: Boolean
  },

  methods: {
    callGate: async function(callGateObj) {
      await this.$store.dispatch("callGate", callGateObj).gatesData;
    }
  }
};
</script>

<style scoped lang="scss">
@import "@/assets/style/components/card.scss";
</style>