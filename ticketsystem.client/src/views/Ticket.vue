<template>
  <AppHeader />

  <div class="wrapper">
    <h1>{{ mode === "create" ? "Create Ticket" : mode === "edit" ? "Edit Ticket" : "Ticket Details" }}</h1>

    <div v-if="mode === 'create' || mode === 'edit'">
      <!-- Form to create or edit a ticket -->
      <form @submit.prevent="createOrUpdateTicket">
        <label for="ticket-title">Title:</label>
        <input class="title-input" type="text" id="ticket-title" v-model="ticket.title" required placeholder="Title" />

        <label for="ticket-description">Description:</label>
        <textarea class="description-input" id="ticket-description" v-model="ticket.desc" required
          placeholder="Describe your problem here"></textarea>

        <div class="select-container">
          <select id="priority" class="select-box" v-model="ticket.priority" required>
            <option value="" disabled>Select Priority</option>
            <option value="5">5</option>
            <option value="4">4</option>
            <option value="3">3</option>
            <option value="2">2</option>
            <option value="1">1</option>
          </select>

          <select id="supporter" class="select-box" v-model="ticket.role_id" required>
            <option value="" disabled>Select Supporter Level</option>
            <option v-for="option in roleList" :key="option.id" :value="option.id">
              {{ option.name }}
            </option>
          </select>
        </div>

        <select id="product" class="select-products" v-model="ticket.product_id" required>
          <option value="" disabled>Select the product</option>
          <option v-for="product in productList" :key="product.id" :value="product.id">
            {{ product.name }}
          </option>
        </select>

        <select id="helped" class="select-products" v-model="ticket.creator_id" required>
          <option value="" disabled>Select the helped person</option>
          <option v-for="option in personList" :key="option.id" :value="option.id">
            {{ option.username }}
          </option>
        </select>

        <div v-if="mode === 'edit'">
          <label for="ticket-answer">Answer:</label>
          <textarea class="description-input" id="ticket-answer" v-model="ticket.answer"
           placeholder="here is the answer"></textarea>
        </div>

        <button class="close-ticket">Close the ticket</button>

        <button class="create-ticket" type="submit">
          {{ mode === 'create' ? 'Create Ticket' : 'Update Ticket' }}
        </button>
      </form>
    </div>

    <div v-else>
      <!-- Ticket Detail View -->
      <h2>Ticket Detail</h2>
      <p><strong class="title">Title:</strong> {{ ticketDetail.title }}</p>
      <p class="description"><strong class="title">Description:</strong> {{ ticketDetail.desc }}</p>
      <p><strong class="title">Priority:</strong> {{ ticketDetail.priority }}</p>
      <p><strong class="title">Product:</strong> {{ ticketDetail.product_id }}</p>
      <p><strong class="title">Helped Person:</strong> {{ ticketDetail.creator_id }}</p>
      <p><strong class="title">Answer:</strong> {{ ticketDetail.answer }}</p>

      <!-- Button Container for Edit and Delete Buttons -->
      <div class="button-container">
        <button @click="createQna" class="qna-button">Create QNA</button>
        <button @click="navigateToEdit" class="edit-button">Edit</button>
        <button @click="deleteTicket" class="delete-button">Delete</button>
      </div>
    </div>
  </div>
</template>

<script>
import { useRouter } from 'vue-router';
import supabase from '@/supabase';

  export default {
    name: "Ticket",
    props: {
      mode: {
        type: String,
        default: "view",
      },
      id: {
        type: String,
        default: null,
      },
      ticketDetail: {
        type: Object,
        default: () => ({})
      }
    },
    data() {
      return {
        ticket: {
          creator_id: "",
          role_id: 1,
          product_id: 0,
          priority: 0,
          title: "",
          desc: "",
          answer: "",
          status: "",
        },
        ticketDetail: {},
        personList: [],
        productList: [],
        roleList: [],
      };
    },
  setup() {
    const router = useRouter();
    return { router };
  },
  methods: {
    async createOrUpdateTicket() {
      const token = await supabase.auth.getSession();
      if (this.mode === "create") {
        var url = 'https://localhost:7253/Ticket/createTicket'
      }
      else if (this.mode === "edit") {
        var url = 'https://localhost:7253/Ticket/changeTicket'
      }
      try {
        console.log(this.role_id);
        const response = await fetch(url, {
          method: 'POST',
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token.data.session.access_token}`
          },
          body: JSON.stringify({
            creator_id: this.ticket.creator_id,
            role_id: this.ticket.role_id,
            product_id: this.ticket.product_id,
            priority: this.ticket.priority,
            title: this.ticket.title,
            desc: this.ticket.desc,
            answer: this.ticket.answer,
            status: "open",
          })
        });
        if (!response.ok) {
          alert('An error occurred while saving user data.');
          return;
        }
      } catch (error) {
        console.error('An error occurred during save:', error);
        alert('An error occurred. Please try again.');
      }
      if (this.mode === "edit") {
        this.router.push(`/ticket/${this.id}`);
      }
    },
    async fetchTicketDetails() {
      const token = await supabase.auth.getSession();
      try {
        const response = await fetch('https://localhost:7253/Ticket/ticket?id='+this.id, {
          method: 'GET',
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token.data.session.access_token}`
          },
        });
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const responseData = await response.json();
        if (responseData && responseData.tickets) {
          this.ticketDetail = responseData.tickets[0];
          if (this.mode === 'edit') {
            console.log(this.ticketDetail);
            this.ticket = { ...this.ticketDetail };
          }
        } else {
          console.error("No ticket found in response");
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    },
    navigateToEdit() {
        this.router.push(`/ticket/${this.id}/edit`);
      },
    async createQna() {
      const token = await supabase.auth.getSession();
      try {
        const response = await fetch(`https://localhost:7253/Product/productQNAList`, {
          method: 'POST',
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token.data.session.access_token}`
          },
          body: JSON.stringify({ id: this.ticket.product_id, Desc: "", Name: "" }),
        });
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const qnaData = await response.json();
        this.qnaList = qnaData.qna;
      } catch (error) {
        console.error('Error fetching QNA:', error);
      }
      },
      async deleteTicket() {
        const token = await supabase.auth.getSession();
        try {
          const response = await fetch(`https://localhost:7253/Ticket/removeTicket`, {
            method: 'POST',
            headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token.data.session.access_token}`
          },
          body: JSON.stringify({ id: this.ticket.id, Desc: "", Name: "" }),
        });
          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }
          console.log("Ticket deleted successfully");
          this.router.push('/tickets');
        } catch (error) {
          console.error("Error deleting ticket:", error);
        }
      },
      async featchpersonList() {
      const token = await supabase.auth.getSession();
      try {
        const response = await fetch('https://localhost:7253/User/userList', {
          method: 'GET',
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token.data.session.access_token}`
          },
        });
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const responseData = await response.json();
        if (responseData && responseData.users) {
          this.personList = responseData.users;
        } else {
          console.error("No ticket found in response");
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    },
    async featchproductList() {
      try {
        const response = await fetch("https://localhost:7253/Product/productList", {
          method: 'GET',
          headers: {"Content-Type": "application/json",},
        });
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const responseData = await response.json();
        if (responseData && responseData.products) {
          this.productList = responseData.products;
        } else {
          console.error("No ticket found in response");
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    },
    async featchroleList() {
      try {
        const response = await fetch('https://localhost:7253/User/roleNameList', {
          method: 'POST',
          headers: {
            "Content-Type": "application/json",},
            body: JSON.stringify( [1, 2, 3]  ),
        });
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const responseData = await response.json();
        if (responseData && responseData.role) {
          this.roleList = responseData.role;
        } else {
          console.error("No ticket found in response");
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    },
    },
    watch: {
      mode(newMode) {
        if (newMode === 'edit' && this.ticketDetail) {
          this.ticket = { ...this.ticketDetail };
        }
      },
      id: {
        immediate: true,
        handler() {
          if (this.id && (this.mode === 'view' || this.mode === 'edit')) {
            this.fetchTicketDetails();
          }
          this.featchpersonList();
          this.featchroleList();
          this.featchproductList();
        }
      },
    },
  };
</script>

<style scoped>
  .wrapper {
    width: 70vh;
    height: auto;
    padding: 20px;
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
    border-radius: 10px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 55%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  h1 {
    display: flex;
    justify-content: center;
    margin-bottom: 20px;
  }

  .description {
    word-wrap: break-word;
    white-space: pre-wrap;
    max-width: 100%;
  }

  .title-input,
  .description-input,
  .select-products,
  .select-box {
    background: var(--color-input-bg);
    color: var(--input-color-text);
    border: 1px solid;
    border-color: var(--color-button);
    border-radius: 5px;
    padding: 10px;
    width: 100%;
    margin-bottom: 15px;
  }

  .select-container {
    display: flex;
    justify-content: space-between;
    margin-bottom: 15px;
  }

  .select-box {
    width: 48%;
  }

  .select-products {
    margin-top: 10px;
  }

  .select-box option,
  .select-products option {
    background-color: var(--color-background);
    color: var(--input-color-text);
  }

  .description-input {
    resize: none;
    height: 150px;
  }

  .create-ticket {
    height: 50px;
    width: 100%;
    background-color: var(--color-button);
    color: var(--color-button-text);
    border: none;
    border-radius: 5px;
    cursor: pointer;
  }

  .button-container {
    display: flex;
    gap: 10px; /* Adds spacing between buttons */
    margin-top: 10px;
  }

  .close.ticket {

  }

  .edit-button,
  .delete-button {
    padding: 8px 12px;
    color: var(--input-color-text);
    border: solid;
    border-color: var(--color-border);
    border-radius: 1px;
    cursor: pointer;
  }

  .edit-button {
    margin-left: 15vw;
    background-color: green;
  }

  .delete-button {
    background-color: red;
  }

  .qna-button {
    margin-top: 10px;
    padding: 8px 12px;
    background-color: var(--color-button);
    color: var(--color-button-text);
    border: solid;
    border-color: var(--color-border);
    border-radius: 1px;
    cursor: pointer;
  }

  .title {
    color: var(--input-color-text);
    font-size: large;
  }
</style>
