<template>
    <div>
      <p>Tickets List</p>
  
      <!-- Product Filter -->
      <select v-model="selectedProduct">
        <option value="AP">All Products</option>
        <option v-for="product in uniqueProducts" :key="product" :value="product">
          {{ product }}
        </option>
      </select>
  
      <!-- Sort by dropdown -->
      <select v-model="sortOption">
        <option value="newest">Newest</option>
        <option value="oldest">Oldest</option>
        <option value="priority">Priority</option>
      </select>
  
      <!-- Ticket List -->
      <ul>
        <li v-for="item in sortedItems" :key="item.id">
          {{ item.name }} - {{ item.product }} - Priority: {{ item.priority }} - Date: {{ new Date(item.time).toLocaleString() }}
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
        sortOption: "newest", 
        items: [
          { id: 1, name: 'Item A', product: 'Product 1', priority: 4, time: "2024-12-05T13:45:30Z" },
          { id: 2, name: 'Item B', product: 'Product 2', priority: 2, time: "2024-10-05T13:45:30Z" },
          { id: 3, name: 'Item C', product: 'Product 1', priority: 3, time: "2024-11-05T15:45:30Z" },
        ],
      };
    },
    computed: {
      uniqueProducts() {
        const products = new Set(this.items.map(item => item.product));
        return Array.from(products);
      },
      filteredItems() {
        if (this.selectedProduct === "AP") {
          return this.items; // Show all products if 'All Products' is selected
        }
        return this.items.filter(item => item.product === this.selectedProduct);
      },
      sortedItems() {
        let sorted = [...this.filteredItems];
  
        // Sorting logic based on the selected sort option
        switch (this.sortOption) {
          case 'oldest':
            sorted.sort((a, b) => new Date(a.time) - new Date(b.time));
            break;
          case 'priority':
            sorted.sort((a, b) => b.priority - a.priority);
            break;
          case 'newest':
          default:
            sorted.sort((a, b) => new Date(b.time) - new Date(a.time));
            break;
        }
  
        return sorted;
      },
    },
  };
  </script>
  
  <style scoped>
/* Container styling */
div {
  font-family: Arial, sans-serif;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 8px;
  max-width: 600px;
  margin: 0 auto;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

/* Title Styling */
p {
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 20px;
  text-align: center;
  color: #333;
}

/* Select dropdowns */
select {
  padding: 8px 12px;
  font-size: 16px;
  margin-right: 20px;
  border: 1px solid #ccc;
  border-radius: 4px;
  background-color: #fff;
  cursor: pointer;
}

/* Select container for styling dropdowns */
div > select {
  display: inline-block;
  margin-bottom: 20px;
}

/* Ticket List */
ul {
  list-style-type: none;
  padding: 0;
}

/* Ticket Item */
li {
  background-color: #fff;
  border: 1px solid #ddd;
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 4px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
}

/* Ticket Item Text */
li span {
  display: inline-block;
  margin-right: 10px;
  color: #555;
}

/* Priority Label */
li span.priority {
  font-weight: bold;
  color: #e74c3c; /* Red color for high priority */
}

/* Date formatting */
li span.date {
  font-size: 0.9em;
  color: #777;
  font-style: italic;
  margin-left: 10px;
}
</style>

  