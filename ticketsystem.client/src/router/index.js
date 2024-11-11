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


    // Ticket routes
    // Start
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
    props: true,
  },
    // Ticket routes
    // END
  

    // Product routes
    // Start
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
    props: { section: 'qna' },
  },
  {
    path: '/product/:id/sla',
    name: 'ProductSla',
    component: Product,
    props: { section: 'sla' },
  },
    // Product routes
    // END
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
