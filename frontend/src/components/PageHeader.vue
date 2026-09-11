<template>
  <div class="flex flex-col justify-between gap-4 md:flex-row md:items-end">
    <div><h2 class="text-2xl font-bold">{{title}}</h2><p class="mt-1 text-sm text-slate-500">{{subtitle}}</p></div>
    <Button label="Novo cadastro" icon="pi pi-plus" @click="$emit('add')"/>
  </div>
</template>
<script setup>
import Button from 'primevue/button'
defineProps({title:String,subtitle:String});defineEmits(['add'])
</script>
