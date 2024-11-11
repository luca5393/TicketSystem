<script lang="js" setup>
import TicketsList from '@/components/TicketsList.vue';
import ProductList from '@/components/ProductList.vue';
</script>

<template>
    <AppHeader />
    <div class="main-container test">
        <div class="tab-container">
            <input type="checkbox" id="tab-switch" v-model="ticketListTab" />
            <label for="tab-switch" class="tab-label">
                <span class="tab-option">Products</span>
                <span class="tab-option">Tickets</span>
            </label>
        </div>

        <ProductList v-if="!ticketListTab"/>
        <TicketsList v-else/>
    </div>
</template>         

<script lang="js">
export default {
  name: 'Home',
  data() {
    return {
      ticketListTab: false,
    };
  },
};
</script>

<style scoped>
.main-container {
    display: flex;
    margin: 7rem 2rem 2rem 2rem;
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
  background-color: #f2f2f2;
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
