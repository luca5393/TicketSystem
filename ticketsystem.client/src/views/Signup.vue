<template>
    <h1>Signup</h1>
    <div class="wrapper">
        <form  method="post">
            <h1>Signup</h1>
            <div class="input-box">
                <input type="text" name="username" placeholder="Username" required>
                <i class='bx bxs-user'></i>
            </div>
            <div class="input-box">
                <input type="password" name="password" placeholder="Password" required>
                <i class='bx bxs-lock-alt'></i>
            </div>
            <div class="input-box">
                <input type="password" name="repeatPassword" placeholder="Repeat password" required>
                <i class='bx bxs-lock-alt'></i>
            </div>
            <div class="input-box">
                <input type="email" name="email" placeholder="Email" required>
                <i class='bx bxs-envelope'></i>
            </div>
            <button type="submit" class="btn" @click="formSubmit" >Signup</button>
        </form>

    </div>
</template>

<script lang="js">
export default {
  name: 'SignUp',
  methods: {
    formSubmit() {
      this.$router.push("login");
    }
  }
};

</script>

<style scoped>

</style>
