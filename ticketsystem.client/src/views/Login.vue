<template>
  <form method="post">
    <div class="wrapper">
      <h1>Login</h1>
      <div class="input-box">
        <input type="text" id="username" name="username" placeholder="Username" required />
      </div>
      <div class="input-box">
        <input type="password" id="password" name="password" placeholder="Password" required />
      </div>
      <div class="remember-forgot">
        <label><input type="checkbox" /> Remember me</label>
        <a href="#">Forgot password?</a>
      </div>
      <button type="submit" class="btn">Login</button>
      <div class="register-link">
        <p>Don't have an account? <a href="Signup">Register</a></p>
      </div>
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
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: "Poppins", sans-serif;
}

.wrapper {
  width: 100%;
  background: transparent;
  border: 2px solid var(--color-wrapper-border);
  backdrop-filter: blur(8px);
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  padding: 30px 80px;
}

.wrapper h1 {
  font-size: 36px;
  text-align: center;
}

.wrapper .input-box {
  position: relative;
  width: 100%;
  height: 50px;
  margin: 30px 0;

}

.input-box input {
  width: 100%;
  height: 100%;
  background: transparent;
  border: none;
  outline: none;
  border: 2px solid var(--color-border-input);
  border-radius: 40px;
  font-size: 16px;
  padding: 20px 45px 20px 20px;
  color: var(--input-color-text);
}

.input-box input::placeholder {
  color: var(--color-placeholder);
}

.input-box i {
  position: absolute;
  right: 20px;
  top: 50%;
  transform: translateY(-50%);
  font-size: 20px;
}

.wrapper .remember-forgot {
  display: flex;
  justify-content: space-between;
  font-size: 14.5px;
  margin: -15px 0 15px;
}

.remember-forgot label input {
  margin-right: 3px;
}

.remember-forgot a {
  color: var(--color-link);
  text-decoration: none;
}

.remember-forgot a:hover {
  text-decoration: underline;
}

.wrapper .btn {
  width: 100%;
  height: 45px;
  background: var(--color-button);
  border: none;
  outline: none;
  border-radius: 40px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  cursor: pointer;
  font-size: 16px;
  color: var(--color-button-text);
  font-weight: 600;
}

.wrapper .register-link {
  font-size: 14.5px;
  text-align: center;
  margin: 20px 0 15px;
}

.register-link p a {
  color: var(--color-link);
  text-decoration: none;
  font-weight: 600;
}

.register-link p a:hover {
  text-decoration: underline;
}
</style>
