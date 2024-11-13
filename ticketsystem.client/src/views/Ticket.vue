<template>
  <AppHeader />

  <div class="wrapper">
    <h1>{{ mode === "create" ? "Create Ticket" : "Ticket Details" }}</h1>

    <div v-if="mode === 'create'">
      <!-- Form to create a ticket -->
      <form @submit.prevent="createTicket">
        <label for="ticket-title">Title:</label>
        <input
          class="title-input"
          type="text"
          id="ticket-title"
          v-model="ticket.title"
          required
          placeholder="Title"
        />

        <label for="ticket-description">Description:</label>
        <textarea
          class="description-input"
          id="ticket-description"
          v-model="ticket.description"
          required
          placeholder="Describe your problem here"
        ></textarea>

        <div class="select-container">
          <select class="select-box">
            <option value="" disabled selected>Priority</option>
            <option value="5">5</option>
            <option value="4">4</option>
            <option value="3">3</option>
            <option value="2">2</option>
            <option value="1">1</option>
          </select>

          <select class="select-box">
            <option value="" disabled selected>Supporter</option>
            <option value="1">First supporter</option>
            <option value="2">Second supporter</option>
            <option value="3">Third supporter</option>
          </select>
        </div>

        <select class="select-products">
          <option value="" disabled selected>Select the product</option>
          <option value="1">option 1</option>
          <option value="2">option 2</option>
          <option value="3">option 3</option>
          <option value="4">option 4</option>
        </select>

        <select class="select-products">
          <option value="" disabled selected>Select the helped</option>
          <option value="1">Lisa</option>
          <option value="2">Marvin</option>
          <option value="3">Claus</option>
          <option value="4">Hans</option>
        </select>

        <button class="create-ticket" type="submit">Create Ticket</button>
      </form>
    </div>

    <div v-else>
      <p>Viewing ticket with ID: {{ id }}</p>
    </div>
  </div>
</template>

<script>
export default {
  // eslint-disable-next-line vue/multi-word-component-names
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
      },
    };
  },
  methods: {
    createTicket() {
      console.log("Creating ticket:", this.ticket);
    },
  },
};
</script>

<style scoped>
.wrapper {
  width: 60vh;
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

.sort-container {
  position: absolute;
  top: 10%;
  right: 5%;
}

.sort-button {
  padding: 10px 20px;
  background-color: var(--color-button);
  color: var(--color-button-text);
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.ticket-display-container {
  position: absolute;
  top: 20%;
  right: 5%;
  width: 300px;
  max-height: 70vh;
  overflow-y: auto;
  background: rgba(0, 0, 0, 0.8);
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
  color: white;
}

.ticket-display {
  border-bottom: 1px solid rgba(255, 255, 255, 0.2);
  padding: 10px 0;
  word-wrap: break-word;
  white-space: normal;
  overflow-wrap: break-word;
}

.delete-button {
  margin-top: 10px;
  padding: 5px 10px;
  background-color: red;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
</style>
