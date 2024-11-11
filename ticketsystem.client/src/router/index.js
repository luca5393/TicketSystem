import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Ticket from '../views/Ticket.vue';
import Login from '../views/Login.vue';
import Signup from '../views/Signup.vue';
import Qna from '../views/Qna.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/ticket',
    name: 'TicketCreate',
    component: Ticket,
    props: { mode: 'create' },
  },
  {
    path: '/ticket/create',
    name: 'TicketCreate',
    component: Ticket,
    props: { mode: 'create' },
  },
  {
    path: '/ticket/:id',
    name: 'TicketView',
    component: Ticket,
    props: true,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/signup',
    name: 'Signup',
    component: Signup,
  },
  {
    path: '/qna',
    name: 'Qna',
    component: Qna,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
