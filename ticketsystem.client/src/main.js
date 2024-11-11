import './assets/main.css';
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import Header from './components/Header.vue';
import axios from 'axios';

const app = createApp(App);

app.component('AppHeader', Header);
app.provide('router', router);
app.provide('axios', axios);

app.use(router);
app.mount('#app');
