<template>
    <div class="create-order">
        <h1>Criar Novo Pedido</h1>

        <form @submit.prevent="submitOrder">
            <label for="customer">Cliente:</label>
            <select v-model="order.customerId" id="customer" required>
                <option value="" disabled>Selecione um cliente</option>
                <option v-for="customer in customers" :key="customer.id" :value="customer.id">
                    {{ customer.name }}
                </option>
            </select>

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

            <button type="submit" class="btn-submit">Criar Pedido</button>
        </form>

        <p v-if="message" class="message">{{ message }}</p>
    </div>
</template>

<script>
import { OrderService } from '../services/OrderService';

export default {
    name: 'CreateOrderPage',
    data() {
        return {
            order: {
                customerId: '',
                items: [{ productId: '', quantity: 1 }],
            },
            customers: [
                { id: 1, name: 'John Doe' }
            ],
            products: [
                { id: 1, name: 'Bicicleta MTB KRW Aro 20', price: 836.00 },
                { id: 2, name: 'Notebook Dell Inspiron I15', price: 3219.00 },
                { id: 3, name: 'Calculadora Financeira HP 12C', price: 306.25 }
            ],
            message: ''
        };
    },
    methods: {
        async submitOrder() {
            try {
                await OrderService.addOrder(this.order);
                this.message = 'Pedido criado com sucesso!';
                this.resetForm();
            } catch (error) {
                console.error('Erro ao criar pedido:', error);
                this.message = 'Erro ao criar pedido. Tente novamente.';
            }
        },
        addItem() {
            this.order.items.push({ productId: '', quantity: 1 });
        },
        removeItem(index) {
            this.order.items.splice(index, 1);
        },
        resetForm() {
            this.order = { customerId: '', items: [{ productId: '', quantity: 1 }] };
        },
        formatCurrency(value) {
            return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value);
        }
    }
};
</script>

<style scoped>
.create-order {
    max-width: 500px;
    margin: auto;
    text-align: left;
}

h1,
h2 {
    text-align: center;
    color: #4CAF50;
}

label {
    display: block;
    margin-top: 10px;
}

select,
input {
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