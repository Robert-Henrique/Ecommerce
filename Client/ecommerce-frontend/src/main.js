// import { createApp } from 'vue'
// import App from './App.vue'

// createApp(App).mount('#app')

import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; // Importando o router configurado

createApp(App)
  .use(router) // Registrando o Vue Router
  .mount('#app'); // Montando a aplicação no div com id="app"

