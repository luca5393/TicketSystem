<!-- eslint-disable vue/multi-word-component-names -->
<template>
  <div class="wrapper">
    <form @submit.prevent="formSubmit">
      <h1>Signup</h1>
      <div class="input-box">
        <input v-model="username" type="text" placeholder="Username" required />
        <i class='bx bxs-user'></i>
      </div>
      <div class="input-box">
        <input v-model="email" type="email" placeholder="Email" required />
        <i class='bx bxs-envelope'></i>
      </div>
      <div class="input-box">
        <input v-model="password" type="password" placeholder="Password" required />
        <i class='bx bxs-lock-alt'></i>
      </div>
      <div class="input-box">
        <input v-model="repeatPassword" type="password" placeholder="Repeat password" required />
        <i class='bx bxs-lock-alt'></i>
      </div>
      <button type="submit" class="btn">Signup</button>
    </form>
  </div>
</template>

<script>
import { useRouter } from 'vue-router';

export default {
  data() {
    return {
      username: '',
      email: '',
      password: '',
      repeatPassword: '',
    };
  },
  methods: {
    async formSubmit() {
      if (this.password !== this.repeatPassword) {
        alert('Passwords do not match!');
        return;
      }

      try {
        // Send the user ID to the backend to store custom data
        const response = await fetch('http://localhost:7253/Api/signup', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            username: this.username,
            email: this.email,
            password: this.password,
            role: 0,
          })
        });

        if (!response.ok) {
          alert('An error occurred while saving user data.');
          return;
        }

        // Redirect or perform additional actions
        const router = useRouter(); // Get the router instance
        router.push({ name: 'login' });
      } catch (error) {
        console.error('An error occurred during signup:', error);
        alert('An error occurred. Please try again.');
      }
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
  width: 30%;
  background: transparent;
  border: 2px solid var(--color-wrapper-border);
  backdrop-filter: blur(8px);
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  padding: 30px 40px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
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
