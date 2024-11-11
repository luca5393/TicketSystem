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

<script setup>
import { ref } from 'vue';
import supabase from '@/supabase'; // Import Supabase client
import { useRouter } from 'vue-router';

const router = useRouter();

const username = ref('');
const email = ref('');
const password = ref('');
const repeatPassword = ref('');
const userSession = ref(null); // This will hold the session if needed

const formSubmit = async () => {
    if (password.value !== repeatPassword.value) {
        alert('Passwords do not match!');
        return;
    }

    try {
        // Create a new user with Supabase authentication
        const { user, session, error } = await supabase.auth.signUp({
            email: email.value,
            password: password.value,
        });

        if (error) {
            alert(error.message);
            return;
        }

        // Optional: You can also store the username in Supabase if you'd like
        // Assuming you have a 'users' table with a 'username' column
        const { data, error: dbError } = await supabase
            .from('users')
            .insert([{ email: email.value, username: username.value }]);

        if (dbError) {
            console.error('Error storing username in DB:', dbError.message);
            alert('Error creating user profile.');
            return;
        }

        // Log the user in immediately after signing up
        userSession.value = session; // Store session in reactive state (Vue state)

        // Optionally, store the user or additional data
        console.log('User created successfully:', user);

        // Redirect to the login page after successful signup
        router.push({ name: 'login' });
    } catch (error) {
        console.error('An error occurred during signup:', error);
        alert('An error occurred. Please try again.');
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
    padding: 30px 40px;
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
