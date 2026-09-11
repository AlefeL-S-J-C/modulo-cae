<template>
<Dialog v-model:visible="visible" modal header="Central de Ajuda" class="w-full max-w-md">
  <p class="text-sm text-slate-500">Encontre rapidamente respostas para suas dúvidas sobre o Portal do Aluno.</p>
  <InputText v-model="search" placeholder="O que você está procurando?" class="mt-4 w-full"/>
  <div class="mt-5 space-y-2"><div v-for="(item,index) in filtered" :key="item.id" class="overflow-hidden rounded-xl border"><button class="flex w-full items-center justify-between p-4 text-left" @click="open=index"><span class="flex items-center gap-3"><i :class="item.icone" :style="{color:item.corIcone}"/>{{item.titulo}}</span><i :class="open===index?'pi pi-chevron-up':'pi pi-chevron-down'"/></button><div v-if="open===index" class="border-t bg-slate-50 p-4 text-sm text-slate-600">{{item.resposta}}</div></div></div>
  <template #footer><Button label="Fechar" text @click="visible=false"/></template>
</Dialog>
</template>
<script setup>
import {ref,computed,onMounted} from 'vue';import Dialog from 'primevue/dialog';import InputText from 'primevue/inputtext';import Button from 'primevue/button';import {caeApi} from '../services/api'
const visible=defineModel('visible',{type:Boolean,default:false});const items=ref([]),search=ref(''),open=ref(0);const filtered=computed(()=>items.value.filter(x=>x.titulo.toLowerCase().includes(search.value.toLowerCase())));onMounted(async()=>items.value=(await caeApi.portal.cae()).data.faqs)
</script>
