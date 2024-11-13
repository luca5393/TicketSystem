import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Ticket from '../views/Ticket.vue';
import Login from '../views/Login.vue';
import Signup from '../views/Signup.vue';
import Product from '../views/Product.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
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
    path: '/ticket',
    redirect: '/ticket/create',
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
    props: route => ({ id: route.params.id, mode: 'view' }),
  },
  {
    path: '/ticket/:id/edit',
    name: 'TicketEdit',
    component: Ticket,
    props: route => ({ id: route.params.id, mode: 'edit' }),
  },

  // Product routes
  {
    path: '/product',
    redirect: '/',
  },
  {
    path: '/product/:id',
    name: 'ProductDefault',
    redirect: (to) => {
      return { name: 'ProductQna', params: { id: to.params.id } };
    },
  },
  {
    path: '/product/:id/qna',
    name: 'ProductQna',
    component: Product,
    props: route => ({ id: route.params.id, section: 'qna' }),
  },
  {
    path: '/product/:id/sla',
    name: 'ProductSla',
    component: Product,
    props: route => ({ id: route.params.id, section: 'sla' }),
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
