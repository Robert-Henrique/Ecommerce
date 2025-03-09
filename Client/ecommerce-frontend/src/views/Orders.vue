<template>
  <div class="orders">
    <h1>Lista de Pedidos</h1>
    <p>Aqui estão todos os pedidos realizados.</p>

    <button @click="createOrder" class="btn-create">Cadastrar Novo Pedido</button>

    <table v-if="orders.length > 0" border="1" cellspacing="0" cellpadding="8">
      <thead>
        <tr>
          <th>ID</th>
          <th>Cliente</th>
          <th>Data do Pedido</th>
          <th>Status</th>
          <th>Total</th>
          <th>Itens</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="order in orders" :key="order.id">
          <td>{{ order.id }}</td>
          <td>{{ order.customerName }}</td>
          <td>{{ formatDate(order.orderDate) }}</td>
          <td>{{ order.status }}</td>
          <td>{{ formatCurrency(order.totalAmount) }}</td>
          <td>
            <ul>
              <li v-for="item in order.orderItems" :key="item.productId">
                {{ item.productName }} ({{ item.quantity }} x {{ formatCurrency(item.unitPrice) }})
              </li>
            </ul>
          </td>
          <td>
            <button @click="editOrder(order.id)" class="btn-edit">Editar</button>
            <button @click="deleteOrder(order.id)" class="btn-delete">Excluir</button>
          </td>
        </tr>
      </tbody>
    </table>
    <p v-else>Nenhum pedido encontrado.</p>
  </div>
</template>

<script>
import { OrderService } from '../services/OrderService';

export default {
  name: 'OrdersPage',
  data() {
    return {
      orders: [],
    };
  },
  created() {
    this.fetchOrders();
  },
  methods: {
    async fetchOrders() {
      try {
        const response = await OrderService.getOrders();
        this.orders = response.data;
      } catch (error) {
        console.error('Erro ao carregar os pedidos:', error);
      }
    },
    formatCurrency(value) {
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      }).format(value);
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      return date.toLocaleDateString('pt-BR', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
      });
    },
    createOrder() {
      this.$router.push('/create-order');
    },
    editOrder(id) {
      this.$router.push(`/edit-order/${id}`); 
    },
    async deleteOrder(id) {
      if (confirm('Tem certeza que deseja excluir este pedido?')) {
        try {
          await OrderService.removeOrder(id);
          this.fetchOrders();
        } catch (error) {
          console.error('Erro ao excluir o pedido:', error);
        }
      }
    }
  },
};
</script>

<style scoped>
.orders {
  text-align: center;
  margin-top: 50px;
}

h1 {
  font-size: 2em;
  color: #4CAF50;
}

p {
  font-size: 1.2em;
  color: #555;
}

button {
  padding: 10px 20px;
  margin: 10px 5px;
  border: none;
  cursor: pointer;
  font-size: 1em;
  border-radius: 5px;
}

button:hover {
  opacity: 0.8;
}

.btn-create {
  background-color: #4CAF50;
  color: white;
}

.btn-edit {
  background-color: #ffa500;
  color: white;
}

.btn-delete {
  background-color: #f44336;
  color: white;
}

table {
  width: 100%;
  margin-top: 20px;
  border-collapse: collapse;
}

th, td {
  text-align: left;
  padding: 8px;
}

th {
  background-color: #4CAF50;
  color: white;
}

td {
  border-top: 1px solid #ddd;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  font-size: 1em;
}
</style>
