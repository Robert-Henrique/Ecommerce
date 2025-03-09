import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Orders from '../views/Orders.vue';
import CreateOrderPage from '../views/CreateOrder.vue';
import EditOrder from '../views/EditOrder.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/orders',
    name: 'Orders',
    component: Orders,
  },
  {
    path: '/create-order',
    name: 'CreateOrder',
    component: CreateOrderPage,
  },
  {
    path: '/edit-order/:id',
    name: 'EditOrder',
    component: EditOrder,
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;