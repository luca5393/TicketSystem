<template>
  <header class="app-header">
    <div class="left">
      <h1><router-link to="/">Ticket System</router-link></h1>
      <nav>
        <ul v-if="LoggedIn"></ul>
      </nav>
    </div>
    <nav class="right">
      <ul v-if="LoggedIn">
        <li><router-link to="/ticket">Create Ticket</router-link></li>
        <li><a href="#" @click.prevent="logout" class="logout-link">Logout</a></li>
      </ul>
      <ul v-else>
        <li><router-link to="/login">Login</router-link></li>
        <li><router-link to="/signup">Signup</router-link></li>
      </ul>
    </nav>
  </header>
</template>

<script>
  import supabase from '@/supabase';

  export default {
    name: 'AppHeader',
    data() {
      return {
        LoggedIn: false,
      };
    },
    async created() {
      const { data: { session }, error } = await supabase.auth.getSession();

      if (error) {
        console.error("Error fetching session:", error.message);
      }

      this.LoggedIn = !!session;
    },
    methods: {
      async logout() {
        const { error } = await supabase.auth.signOut();
        if (error) {
          console.error("Error during logout:", error.message);
        } else {
          this.LoggedIn = false;
          this.$router.push({ name: 'Login' });
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
    justify-content: space-between;
  }

  .left {
    display: flex;
    align-items: center;
    gap: 1rem;
  }

  nav ul {
    list-style: none;
    display: flex;
    gap: 1rem;
  }

  nav a, .logout-link {
    color: white;
    text-decoration: none;
    cursor: pointer;
  }

  

  .right {
    margin-left: auto;
  }

  body {
    padding-top: 4rem;
  }
</style>
