<template>
    <div>
        <p>TicketsList</p>
        <select v-model="selectedProduct">
            <option value="AP">All Products</option>
            <option v-for="product in uniqueProducts" :key="product" :value="product">
                {{ product }}
            </option>
        </select>
        <ul>
            <li v-for="item in items" :key="item.id">
                {{ item.name }} - {{ item.product }}
            </li>
        </ul>
    </div>
</template>
  
<script lang="js">
export default {
    name: 'TicketsList',
    data() {
        return {
            selectedProduct: "AP",
            items: [
                { id: 1, name: 'Item A', product: 'Product 1' },
                { id: 2, name: 'Item B', product: 'Product 2' },
                { id: 3, name: 'Item C', product: 'Product 1' },
            ],
        };
    },
    computed: {
        uniqueProducts() {
        const products = new Set(this.items.map(item => item.product));
        return Array.from(products);
        },
    }
};

</script>

<style scoped>
</style>


  