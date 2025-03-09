<template>
    <div class="edit-order">
      <h1>Editar Pedido</h1>
  
      <form v-if="order" @submit.prevent="submitOrder">
        <!-- Seleção do Cliente -->
        <label for="customer">Cliente:</label>
        <select v-model="order.customerId" id="customer" required>
          <option value="" disabled>Selecione um cliente</option>
          <option v-for="customer in customers" :key="customer.id" :value="customer.id">
            {{ customer.name }}
          </option>
        </select>
  
        <!-- Seleção do Status -->
        <label for="status">Status:</label>
        <select v-model="order.status" id="status" required>
          <option v-for="(label, value) in statuses" :key="value" :value="value">
            {{ label }}
          </option>
        </select>
  
        <!-- Lista de Itens -->
        <h2>Itens do Pedido</h2>
        <div v-for="(item, index) in order.items" :key="index" class="order-item">
          <label>Produto:</label>
          <select v-model="item.productId" required>
            <option value="" disabled>Selecione um produto</option>
            <option v-for="product in products" :key="product.id" :value="product.id">
              {{ product.name }} - {{ formatCurrency(product.price) }}
            </option>
          </select>
  
          <label>Quantidade:</label>
          <input type="number" v-model="item.quantity" min="1" required />
  
          <button type="button" @click="removeItem(index)" class="btn-delete">Remover</button>
        </div>
  
        <button type="button" @click="addItem" class="btn-add">Adicionar Item</button>
  
        <!-- Botão de salvar alterações -->
        <button type="submit" class="btn-submit">Salvar Alterações</button>
      </form>
  
      <p v-if="message" class="message">{{ message }}</p>
    </div>
  </template>
  
  <script>
  import { OrderService } from '../services/OrderService';
  
  export default {
    name: 'EditOrderPage',
    data() {
      return {
        orderId: null,
        order: null,
        customers: [
          { id: 1, name: 'John Doe' },
          { id: 2, name: 'Maria Silva' }
        ],
        products: [
          { id: 1, name: 'Bicicleta MTB KRW Aro 20', price: 836.00 },
          { id: 2, name: 'Notebook Dell Inspiron I15', price: 3219.00 }
        ],
        statuses: {
          1: 'Pendente',
          2: 'Processando',
          3: 'Enviado',
          4: 'Entregue'
        },
        message: ''
      };
    },
    created() {
      this.orderId = this.$route.params.id;
      this.fetchOrder();
    },
    methods: {
      async fetchOrder() {
        try {
          const response = await OrderService.getOrderDetails(this.orderId);
          this.order = {
            customerId: response.data.customerId,
            status: response.data.status,
            items: response.data.orderItems.map(item => ({
              productId: item.productId,
              quantity: item.quantity
            }))
          };
        } catch (error) {
          console.error('Erro ao carregar pedido:', error);
        }
      },
      async submitOrder() {
        try {
          await OrderService.updateOrder(this.orderId, this.order);
          this.message = 'Pedido atualizado com sucesso!';
        } catch (error) {
          console.error('Erro ao atualizar pedido:', error);
          this.message = 'Erro ao atualizar pedido. Tente novamente.';
        }
      },
      addItem() {
        this.order.items.push({ productId: '', quantity: 1 });
      },
      removeItem(index) {
        this.order.items.splice(index, 1);
      },
      formatCurrency(value) {
        return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value);
      }
    }
  };
  </script>
  
  <style scoped>
  .edit-order {
    max-width: 500px;
    margin: auto;
    text-align: left;
  }
  
  h1, h2 {
    text-align: center;
    color: #4CAF50;
  }
  
  label {
    display: block;
    margin-top: 10px;
  }
  
  select, input {
    width: 100%;
    padding: 8px;
    margin-top: 5px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  
  .order-item {
    display: flex;
    gap: 10px;
    align-items: center;
    margin-bottom: 10px;
  }
  
  button {
    margin-top: 10px;
    padding: 8px;
    border: none;
    cursor: pointer;
    border-radius: 5px;
  }
  
  .btn-add {
    background-color: #4CAF50;
    color: white;
  }
  
  .btn-delete {
    background-color: #f44336;
    color: white;
  }
  
  .btn-submit {
    background-color: #008CBA;
    color: white;
    width: 100%;
  }
  
  .message {
    text-align: center;
    font-weight: bold;
    margin-top: 15px;
    color: #4CAF50;
  }
  </style>  