<template>
    <div class="product-list">
      <p class="title">Product List</p>
      <ul>
        <li v-for="item in items" :key="item.id" class="product-item">
          <span class="product-name">{{ item.product }}</span>
          <div class="buttons">
            <router-link :to="{ name: 'ProductSla', params: { id: item.id } }" class="btn sla">SLA</router-link>
            
            <router-link :to="{ name: 'ProductQna', params: { id: item.id } }" class="btn qna">QNA</router-link>
          </div>
        </li>
      </ul>
    </div>
  </template>
  
  <script lang="js">
  export default {
    name: 'ProductList',
    data() {
      return {
        items: [
          { id: 1, product: 'Product 1' },
          { id: 2, product: 'Product 2' },
          { id: 3, product: 'Product 3' },
        ],
      };
    },
  };
  </script>
  
  <style scoped>
  .product-list {
    max-width: 600px;
    margin: 0 auto;
    font-family: Arial, sans-serif;
  }
  
  .title {
    font-size: 1.5rem;
    font-weight: bold;
    text-align: center;
    margin-bottom: 20px;
  }
  
  ul {
    list-style-type: none;
    padding: 0;
  }
  
  .product-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #f9f9f9;
    border-radius: 8px;
    margin-bottom: 12px;
    padding: 10px 15px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  .product-name {
    font-size: 1rem;
    color: #333;
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
  }
  
  .sla {
    background-color: #4caf50;
    color: white;
  }
  
  .sla:hover {
    background-color: #45a049;
  }
  
  .qna {
    background-color: #2196f3;
    color: white;
  }
  
  .qna:hover {
    background-color: #1976d2;
  }
  </style>
  