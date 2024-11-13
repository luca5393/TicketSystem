<template>
  <AppHeader />

  <div class="wrapper">
    <h1>{{ mode === "create" ? "Create Ticket" : mode === "edit" ? "Edit Ticket" : "Ticket Details" }}</h1>

    <div v-if="mode === 'create' || mode === 'edit'">
      <!-- Form to create or edit a ticket -->
      <form @submit.prevent="createOrUpdateTicket">
        <label for="ticket-title">Title:</label>
        <input class="title-input"
               type="text"
               id="ticket-title"
               v-model="ticket.title"
               required
               placeholder="Title" />

        <label for="ticket-description">Description:</label>
        <textarea class="description-input"
                  id="ticket-description"
                  v-model="ticket.description"
                  required
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

          <select id="supporter" class="select-box" v-model="ticket.supporter" required>
            <option value="" disabled>Select Supporter</option>
            <option value="1">First supporter</option>
            <option value="2">Second supporter</option>
            <option value="3">Third supporter</option>
          </select>
        </div>

        <select id="product" class="select-products" v-model="ticket.product" required>
          <option value="" disabled>Select the product</option>
          <option value="Product A">Product A</option>
          <option value="Product B">Product B</option>
          <option value="Product C">Product C</option>
          <option value="Product D">Product D</option>
        </select>

        <select id="helped" class="select-products" v-model="ticket.helped" required>
          <option value="" disabled>Select the helped person</option>
          <option value="Lisa">Lisa</option>
          <option value="Marvin">Marvin</option>
          <option value="Claus">Claus</option>
          <option value="Hans">Hans</option>
        </select>

        <button class="create-ticket" type="submit">
          {{ mode === 'create' ? 'Create Ticket' : 'Update Ticket' }}
        </button>
      </form>
    </div>

    <div v-else>
      <!-- Ticket Detail View -->
      <h2>Ticket Detail</h2>
      <p><strong class="title">Title:</strong> {{ ticketDetail.title }}</p>
      <p class="description"><strong class="title">Description:</strong> {{ ticketDetail.description }}</p>
      <p><strong class="title">Priority:</strong> {{ ticketDetail.priority }}</p>
      <p><strong class="title">Assigned Supporter:</strong> {{ ticketDetail.supporter }}</p>
      <p><strong class="title">Product:</strong> {{ ticketDetail.product }}</p>
      <p><strong class="title">Helped Person:</strong> {{ ticketDetail.helped }}</p>

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
    },
    data() {
      return {
        ticket: {
          title: "",
          description: "",
          priority: "",
          supporter: "",
          product: "",
          helped: "",
        },
        ticketDetail: null,
        qnaList: [], // Added for storing QNA data
      };
    },
    setup() {
      const router = useRouter();
      return { router };
    },
    methods: {
      async createOrUpdateTicket() {
        if (this.mode === "create") {
          console.log("Creating ticket:", this.ticket);
          // Logic to save the new ticket
        } else if (this.mode === "edit") {
          console.log("Updating ticket:", this.ticket);
          // Update ticket logic here
          this.router.push(`/ticket/${this.id}`);
        }
      },
      async fetchTicketDetails() {
        // Simulate fetching ticket data based on `id`
        this.ticketDetail = {
          title: "Login Issue",
          description: "Unable to login to the system due to an authentication error.",
          priority: "3",
          supporter: "2",
          product: "Product A",
          helped: "Lisa",
        };

        // Populate form fields if in "edit" mode
        if (this.mode === "edit") {
          this.ticket = { ...this.ticketDetail };
        }
      },
      navigateToEdit() {
        this.router.push(`/ticket/${this.id}/edit`);
      },
      async createQna() {
        try {
          const response = await fetch(`https://localhost:7253/Product/productQNAList`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: this.ticket.product, Desc: "", Name: "" }), // Assuming `product` is the identifier
          });
          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }
          const qnaData = await response.json();
          console.log(qnaData.qna);
          this.qnaList = qnaData.qna;
        } catch (error) {
          console.error('Error fetching QNA:', error);
        }
      },
      async deleteTicket() {
        try {
          // Simulate a delete request (Replace with actual API call)
          const response = await fetch(`https://localhost:7253/tickets/${this.id}`, {
            method: 'DELETE',
          });
          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }
          console.log("Ticket deleted successfully");
          this.router.push('/tickets'); // Redirect to the tickets list page after deletion
        } catch (error) {
          console.error("Error deleting ticket:", error);
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
