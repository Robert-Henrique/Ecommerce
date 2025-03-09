<template>
    <div class="home">
      <h1>Bem-vindo ao sistema de pedidos!</h1>
      <p>Este é o sistema onde você pode gerenciar seus pedidos de forma eficiente.</p>
      <router-link to="/orders" class="btn-link">Ir para os pedidos</router-link>
    </div>
  </template>
  
  <script>
  export default {
    name: 'HomePage',  // Alterado para nome composto
  };
  </script>
  
  <style scoped>
  .home {
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
  
  .btn-link {
    display: inline-block;
    margin-top: 20px;
    padding: 10px 20px;
    font-size: 1em;
    color: white;
    background-color: #4CAF50;
    text-decoration: none;
    border-radius: 5px;
  }
  
  .btn-link:hover {
    background-color: #45a049;
  }
  </style>
  