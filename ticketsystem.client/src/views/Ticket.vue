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
        <textarea class="description-input" id="ticket-description" v-model="ticket.description" required
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
    ticketDetail: null,
  },
  data() {
    return {
      ticket: {
        title: "",
        description: "",
        priority: "",
        supporter: "",
        product: "",
        helped: ""
      },
      // Sample test data
      testTickets: [
        {
          id: "1",
          title: "Network Issue",
          description: "The internet connection is very slow and intermittent.",
          priority: "1",
          supporter: "First supporter",
          product: "Product A",
          helped: "Lisa"
        },
        {
          id: "2",
          title: "Software Crash",
          description: "The software crashes when I try to save a file.",
          priority: "3",
          supporter: "Second supporter",
          product: "Product B",
          helped: "Marvin"
        },
        {
          id: "3",
          title: "Login Issue",
          description: "Unable to log in with correct credentials.",
          priority: "2",
          supporter: "Third supporter",
          product: "Product C",
          helped: "Claus"
        },
        {
          id: "4",
          title: "Printer Not Working",
          description: "The printer does not respond when trying to print a document.",
          priority: "5",
          supporter: "First supporter",
          product: "Product D",
          helped: "Hans"
        }
      ]
    };
  },
  setup() {
    const router = useRouter();
    return { router };
  },
  methods: {
    async createOrUpdateTicket() {
      // Code for creating or updating ticket
      // Same as before
    },
    async fetchTicketDetails() {
      console.log("Fetching ticket details for ID:", this.id);
      const foundTicket = this.testTickets.find(ticket => ticket.id === this.id);

      if (foundTicket) {
        this.ticketDetail = foundTicket;
        console.log("Ticket details found:", this.ticketDetail); // Check if data is populated
      } else {
        console.warn("No ticket found for ID:", this.id);
        this.ticketDetail = {}; // Clear ticket details if not found
      }

      // Log mode to ensure it's set correctly
      console.log("Current mode:", this.mode);

      // Populate form fields if in "edit" mode
      if (this.mode === "edit" && this.ticketDetail) {
        this.ticket = { ...this.ticketDetail };
        console.log("Populated ticket for edit:", this.ticket);
      }
    },

    navigateToEdit() {
      this.router.push(`/ticket/${this.id}/edit`);
    },
    async createQna() {
      // Same as before
    },
    async deleteTicket() {
      // Same as before
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
  mounted() {
    if (this.id) {
      this.fetchTicketDetails();
    }
  }
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
  gap: 10px;
  /* Adds spacing between buttons */
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
