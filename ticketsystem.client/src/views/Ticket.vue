<template>
    <AppHeader />

    <div class="margin-top">
        <h1>{{ mode === 'create' ? 'Create Ticket' : 'Ticket Details' }}</h1>

        <div v-if="mode === 'create'">
            <!-- Form to create a ticket -->
            <form @submit.prevent="createTicket">
                <label for="ticket-title">Title:</label>
                <input type="text" id="ticket-title" v-model="ticket.title" required />

                <label for="ticket-description">Description:</label>
                <textarea id="ticket-description" v-model="ticket.description" required></textarea>

                <button type="submit">Create Ticket</button>
            </form>
        </div>

        <div v-else>
            <p>Viewing ticket with ID: {{ id }}</p>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Ticket',
    props: {
        mode: {
            type: String,
            default: 'view',
        },
        id: {
            type: String,
            default: null,
        },
    },
    data() {
        return {
            ticket: {
                title: '',
                description: '',
            },
        };
    },
    methods: {
        createTicket() {
            console.log('Creating ticket:', this.ticket);
        },
    },
};
</script>


<style scoped>
.margin-top {
    margin-top: 100px;
}
</style>