<template>
    <div class="c-row">
        <slot/> <!-- Om content nog in te steken later -->
    </div>
</template>

<script>
export default {
    name: "Row"
};
</script>

<style lang="scss" scoped>
@import '@/assets/style/objects/row.scss';

</style>