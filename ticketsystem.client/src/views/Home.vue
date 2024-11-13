<script lang="js" setup>
import TicketsList from '@/components/TicketsList.vue';
import ProductList from '@/components/ProductList.vue';
</script>

<template>
  <AppHeader />
  <div class="wrapper">
    <div class="tab-container">
      <input type="checkbox" id="tab-switch" v-model="ticketListTab" />
      <label for="tab-switch" class="tab-label">
        <span class="tab-option">Products</span>
        <span class="tab-option">Tickets</span>
      </label>
    </div>
    <div class="product-list">
      <ProductList v-if="!ticketListTab" />
      <TicketsList v-else />
    </div>
  </div>
</template>

<script lang="js">
export default {
  // eslint-disable-next-line vue/multi-word-component-names
  name: 'Home',
  data() {
    return {
      ticketListTab: false,
    };
  },
};
</script>

<style scoped>

.wrapper {
  width: 70vh;
  height: 80vh;
  padding: 20px;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  border-radius: 10px;
  position: relative;
  top: 60px;
  align-items: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.tab-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 300px;
  margin: 20px auto;
}


#tab-switch {
  display: none;
}

.tab-label {
  display: flex;
  width: 100%;
  background-color: var(--color-placeholder);
  color: var(--color-button-text);
  border-radius: 20px;
  cursor: pointer;
  padding: 5px;
}

.tab-label .tab-option {
  flex: 1;
  text-align: center;
  padding: 10px;
  font-weight: bold;
  color: #666;
  transition: color 0.3s ease;
}

.tab-label .tab-option:first-child {
  border-top-left-radius: 20px;
  border-bottom-left-radius: 20px;
}

.tab-label .tab-option:last-child {
  border-top-right-radius: 20px;
  border-bottom-right-radius: 20px;
}

#tab-switch:checked + .tab-label .tab-option:first-child {
  color: #ccc;
}

#tab-switch:not(:checked) + .tab-label .tab-option:last-child {
  color: #ccc;
}
</style>
