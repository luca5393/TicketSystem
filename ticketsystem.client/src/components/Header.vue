<template>
    <header class="app-header">
        <h1>Ticket System</h1>
        <nav>
            <ul v-if="LoggedIn">
                <li><router-link to="/">Home</router-link></li>
                <li><router-link to="/qna">QNA</router-link></li>
            </ul>
        </nav>
        <nav class="right">
            <ul v-if="LoggedIn">
                <li>Logout</li>
            </ul>
            <ul v-else>
                <li><router-link to="/login">Login</router-link></li>
                <li><router-link to="/signup">Signup</router-link></li>
            </ul>
        </nav>
    </header>
</template>
  
<script lang="js">
export default {
    name: 'Header',
    data() {
        return {
            LoggedIn: false,
        };
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


  