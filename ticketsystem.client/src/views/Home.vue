<script lang="js" setup>
import TicketsList from '@/components/TicketsList.vue';
import ProductList from '@/components/ProductList.vue';
</script>

<template>
    <AppHeader />
    <h1>HOMESCREEN</h1>
    <input type="checkbox" v-model="ticketListTab">
    <ProductList v-if="ticketListTab"/>
    <TicketsList v-else/>
</template>         

<script lang="js">
export default {
  name: 'Home',
  data() {
    return {
      ticketListTab: true,
    };
  },
};
</script>

<style scoped>
</style>