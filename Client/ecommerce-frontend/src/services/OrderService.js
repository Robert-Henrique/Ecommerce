import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5223/api/',
});

export const OrderService = {
  getOrders: () => api.get('orders'),
  getOrderDetails: (id) => api.get(`orders/${id}`),
  addOrder: (order) => api.post('orders', order),
  updateOrder: (id, order) => api.put(`orders/${id}`, order),
  removeOrder: (id) => api.delete(`orders/${id}`)
};
