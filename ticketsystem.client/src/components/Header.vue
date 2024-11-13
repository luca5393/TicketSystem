<template>
    <header class="app-header">
        <h1><router-link to="/">Ticket System</router-link></h1>
        <nav>
            <ul v-if="LoggedIn">
            </ul>
        </nav>
        <nav class="right">
            <ul v-if="LoggedIn">
                <li @click="logout">Logout</li>
            </ul>
            <ul v-else>
                <li><router-link to="/ticket">Create ticket</router-link></li>
                <li><router-link to="/login">Login</router-link></li>
                <li><router-link to="/signup">Signup</router-link></li>
            </ul>
        </nav>
    </header>
</template>

<script>
import supabase from '@/supabase';

export default {
    // eslint-disable-next-line vue/multi-word-component-names
    name: 'AppHeader',  // Renamed from "Header" to "AppHeader"
    data() {
        return {
            LoggedIn: false, // State to track login status
        };
    },
    async created() {
        // Fetch the current session using the correct method for Supabase v2.x+
        const { data: { session }, error } = await supabase.auth.getSession();
        
        if (error) {
            console.error("Error fetching session:", error.message);
        }

        // If session exists, the user is logged in
        this.LoggedIn = !!session;
    },
    methods: {
        // Method to handle logout
        async logout() {
            const { error } = await supabase.auth.signOut(); // Log the user out from Supabase
            if (error) {
                console.error("Error during logout:", error.message);
            } else {
                this.LoggedIn = false; // Update the login status
                this.$router.push({ name: 'Login' }); // Redirect to the login page
            }
        },
    },
};
</script>

<style scoped>
.app-header {
  background-color: #333;
  color: white;
  padding: 1rem;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 1000;
  display: flex;
  align-items: center;
}

nav {
  display: flex;
  &.right {
    margin-left: auto;
  }
}

nav ul {
  list-style: none;
  display: flex;
  gap: 1rem;
}

nav a {
  color: white;
  text-decoration: none;
}

nav a.router-link-exact-active {
  font-weight: bold;
}

body {
  padding-top: 4rem;
}
</style>
