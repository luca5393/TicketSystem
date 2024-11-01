<template>
    <form method="post">
        <h1>Login</h1>
        <div class="input-box">
            <input type="text" name="username" placeholder="Username" required>
        </div>
        <div class="input-box">
            <input type="password" name="password" placeholder="Password" required>
        </div>
        <div class="remember-forgot">
            <label><input type="checkbox"> Remember me</label>
            <a href="#">Forgot password?</a>
        </div>
        <button type="submit" class="btn">Login</button>
        <div class="register-link">
            <p>Don't have an account? <a href="Signup">Register</a></p>
        </div>
    </form>
</template>

<script lang="js">
export default {
  name: 'LogIn',
  methods: {
    formSubmit() {
      this.$router.push("home");
    }
  }
};
</script>

<style scoped>


form {
    display: flex;
    flex-direction: column;
    position: fixed;
    left: 40%;
    width: 20%;
    top: 40%;
    background: transparent;
    border: 2px solid var(--color-border-costume);
    backdrop-filter: blur(8px);;
    box-shadow: 0 0 10px rgba(0, 0, 0, .1);
    color: var(--color-text);
    border-radius: 10px;
    padding: 30px 40px;


    h1 {
        font-size: 36px;
        text-align: center;
    }

}
</style>
