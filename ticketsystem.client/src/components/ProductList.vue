<template>
  <div class="product-list">
    <ul v-if="items.length">
      <li v-for="item in items" :key="item.id" class="product-item">
        <span class="product-name">{{ item.name }}</span>
        <div class="buttons">
          <router-link :to="{ name: 'ProductSla', params: { id: item.id } }" class="btn sla">SLA</router-link>
          <router-link :to="{ name: 'ProductQna', params: { id: item.id } }" class="btn qna">QNA</router-link>
        </div>
      </li>
    </ul>
    <p v-else>No products available</p>
    <!-- Display this if items is empty -->
  </div>
</template>

<script>

export default {
  name: "ProductList",
  data() {
    return {
      items: [],
    };
  },
  mounted() {
    this.fetchProducts();
  },
  methods: {
    async fetchProducts() {
      try {
        const response = await fetch('https://localhost:7253/Product/productList', {
          method: "GET",
          headers: { "Content-Type": "application/json" },
        });

        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const responseData = await response.json();

        if (responseData && responseData.products) {
          this.items = responseData.products;
        } else {
          console.error("No products found in response");
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    }

  },
};
</script>

<style scoped>
.product-list {
  max-width: 600px;
  margin: 0 auto;
  padding-top: 20px;
  font-family: Arial, sans-serif;
  color: #ddd;
}

ul {
  list-style-type: none;
  padding: 0;
  margin: 0;
}

.product-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #333;
  border-radius: 8px;
  margin-bottom: 12px;
  padding: 10px 15px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.product-name {
  font-size: 1rem;
  color: #ddd;
}

.buttons {
  display: flex;
  gap: 10px;
}

.btn {
  padding: 8px 15px;
  border: none;
  border-radius: 4px;
  font-size: 0.9rem;
  cursor: pointer;
  transition: background-color 0.3s ease;
  color: white;
}

.sla {
  background-color: #4caf50;
}

.sla:hover {
  background-color: #45a049;
}

.qna {
  background-color: #2196f3;
}

.qna:hover {
  background-color: #1976d2;
}
</style>
