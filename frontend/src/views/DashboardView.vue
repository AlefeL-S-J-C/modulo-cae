<template>
  <section>
    <div class="mb-8">
      <p class="text-sm font-semibold text-[#3565b3]">VISÃO GERAL</p>
      <h2 class="mt-1 text-3xl font-bold">Conteúdo do Portal</h2>
      <p class="mt-2 text-slate-500">Gerencie tudo que será exibido dinamicamente no Portal do Aluno.</p>
    </div>
    <div class="grid gap-5 md:grid-cols-2 xl:grid-cols-4">
      <div v-for="card in cards" :key="card.label" class="rounded-2xl border bg-white p-5 shadow-sm">
        <div class="flex items-center justify-between">
          <span class="text-sm text-slate-500">{{ card.label }}</span>
          <i :class="[card.icon,'text-[#073C87]']"/>
        </div>
        <strong class="mt-4 block text-3xl">{{ card.value }}</strong>
        <span class="mt-1 block text-xs text-slate-400">itens cadastrados</span>
      </div>
    </div>
    <div class="mt-6 rounded-2xl border bg-white p-6">
      <h3 class="font-bold">Fluxo de publicação</h3>
      <div class="mt-5 grid gap-3 md:grid-cols-4">
        <div v-for="(step,i) in steps" :key="step" class="rounded-xl bg-slate-50 p-4">
          <span class="text-xs font-bold text-[#3565b3]">0{{i+1}}</span>
          <p class="mt-2 text-sm font-semibold">{{ step }}</p>
        </div>
      </div>
    </div>
  </section>
</template>
<script setup>
import { onMounted, ref } from 'vue'
import { caeApi } from '../services/api'
const cards=ref([{label:'Contatos',value:0,icon:'pi pi-phone'},{label:'Horários',value:0,icon:'pi pi-clock'},{label:'FAQs',value:0,icon:'pi pi-question-circle'},{label:'Serviços',value:0,icon:'pi pi-briefcase'}])
const steps=['Cadastrar conteúdo','Revisar informações','Ativar/publicar','Portal consome pela API']
onMounted(async()=>{
  const [c,h,f,s]=await Promise.all([caeApi.contatos.list(),caeApi.horarios.list(),caeApi.faqs.list(),caeApi.servicos.list()])
  cards.value[0].value=c.data.length;cards.value[1].value=h.data.length;cards.value[2].value=f.data.length;cards.value[3].value=s.data.length
})
</script>
