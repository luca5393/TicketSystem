import './assets/main.css';
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import Header from './components/Header.vue';

const app = createApp(App);

app.component('AppHeader', Header);

app.use(router);
app.mount('#app');
