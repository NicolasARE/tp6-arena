<template>
  <h1>Hello World</h1>

  <h2>Affichage des produits depuis un appel au back</h2>

  <div v-for="product in products">
    <h3>{{ product.name }}</h3>
    <p>{{ product.description }}</p>
    <p><b>{{ product.price }}$</b></p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

// fetch data from localhost:5000
const products = ref([])
onMounted(async () => {
  const res = await fetch('http://localhost:5000/api/product/getproducts')
  products.value = await res.json()
})
</script>
