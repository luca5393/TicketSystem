<template>
  <div>
    <p class="title">Tickets List</p>

    <div class="controls">
      <select v-model="selectedProduct">
        <option value="AP">All Products</option>
        <option v-for="product in uniqueProducts" :key="product" :value="product">
          {{ product }}
        </option>
      </select>

      <select v-model="sortOption">
        <option value="newest">Newest</option>
        <option value="oldest">Oldest</option>
        <option value="priority">Priority</option>
      </select>
    </div>

    <ul>
      <li v-for="item in sortedItems" :key="item.id" class="ticket-container">
        <router-link :to="{ name: 'TicketView', params: { id: item.id } }">
          <div class="ticket-item">
            <span>
              {{ item.name }} - {{ item.product }} - Priority: {{ item.priority }} - Date:
              {{ new Date(item.time).toLocaleString() }} | Status: {{ item.status }}
            </span>
          </div>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
import supabase from '@/supabase';

export default {
  name: 'TicketsList',
  data() {
    return {
      selectedProduct: "AP",
      sortOption: "newest",
      items: [],
    };
  },
  mounted() {
    this.fetchTicketList();
  },
  methods: {
    async fetchTicketList() {
      const token = await supabase.auth.getSession();
      try {
        const userresponse = await fetch('https://localhost:7253/Ticket/userTicketList', {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token.data.session.access_token}`
          },
        });

        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${userresponse.status}`);
        }

        const userResponseData = await userresponse.json();

        if (userResponseData && userResponseData.products) {
          this.items = userResponseData.products;
        } else {
          console.error("No tickets found in response");
        }
      } catch (error) {
        console.error("Error fetching tickets:", error);
      }

      try {
        const roleResponse = await fetch('https://localhost:7253/Ticket/roleTicketList', {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token.data.session.access_token}`
          },
        });

        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${roleResponse.status}`);
        }

        const roleResponseData = await roleResponse.json();

        if (roleResponseData && roleResponseData.products) {
          this.items += roleResponseData.products;
          removeDuplicates(this.items);
        } else {
          console.error("No tickets found in response");
        }
      } catch (error) {
        console.error("Error fetching tickets:", error);
      }
    },
    removeDuplicates(items) {
      return items.filter((value, index, self) =>
        index === self.findIndex((t) => (
          t.id === value.id
        ))
  );
}
  },
  computed: {
    uniqueProducts() {
      const products = new Set(this.items.map(item => item.product));
      return Array.from(products);
    },
    filteredItems() {
      if (this.selectedProduct === "AP") {
        return this.items;
      }
      return this.items.filter(item => item.product === this.selectedProduct);
    },
    sortedItems() {
      let sorted = [...this.filteredItems];

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
div {
  font-family: Arial, sans-serif;
  padding: 10px;
  background-color: var(--color-background);
  border-radius: 8px;
  max-width: 700px;
  max-height: 65vh;
  overflow-y: auto;
  margin: 0 auto;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

p {
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 20px;
  text-align: center;
  color: var(--input-color-text);
}

.controls {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 15px;
  justify-content: center;
}

select {
  padding: 8px 12px;
  font-size: 16px;
  border: 1px solid var(--color-button);
  border-radius: 4px;
  background-color: var(--color-background);
  color: var(--input-color-text);
  cursor: pointer;
}

ul {
  list-style-type: none;
  padding: 0;
}

.ticket-container {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.ticket-item {
  background-color: var(--color-background);
  border: 1px solid var(--color-button);
  color: var(--input-color-text);
  padding: 10px;
  border-radius: 4px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
  cursor: pointer;
  flex-grow: 1;
}

.ticket-item:hover {
  background-color: var(--color-background);
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.delete-checkbox {
  margin-right: 10px;
  cursor: pointer;
}

.delete-selected-button {
  padding: 8px 16px;
  font-size: 16px;
  background-color: red;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.delete-selected-button:hover {
  background-color: darkred;
}
</style>
