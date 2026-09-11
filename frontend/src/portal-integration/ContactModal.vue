<template>
  <Dialog v-model:visible="visible" modal :showHeader="false" class="w-full max-w-md">
    <div class="text-center">
      <div class="mx-auto flex h-14 w-14 items-center justify-center rounded-full bg-[#073C87] text-white"><i class="pi pi-headphones text-xl"/></div>
      <h2 class="mt-4 text-xl font-bold">Central de Atendimento ao Estudante (CAE)</h2>
      <p class="mt-2 text-sm text-slate-500">Estamos aqui para ajudar.</p>
    </div>
    <div class="mt-6 rounded-xl bg-slate-50 p-4">
      <p class="mb-3 text-xs font-bold text-slate-500">ENTRE EM CONTATO</p>
      <div v-for="phone in data.contatos" :key="phone.id" class="flex items-center justify-between gap-3 py-2">
        <span class="text-sm font-semibold">{{phone.numero}}</span>
        <Button label="Ligar" size="small" icon="pi pi-phone" @click="call(phone.numero)"/>
      </div>
    </div>
    <div class="mt-5"><h3 class="mb-3 font-semibold"><i class="pi pi-clock mr-2"/>Horários de Atendimento</h3>
      <div v-for="h in data.horarios" :key="h.id" class="flex justify-between py-1 text-sm"><span class="text-slate-500">{{h.diaSemana}}</span><b>{{h.fechado?'Fechado':`${h.horaInicio} às ${h.horaFim}`}}</b></div>
    </div>
    <template #footer><Button label="Fechar" text class="w-full" @click="visible=false"/></template>
  </Dialog>
</template>
<script setup>
import {ref,onMounted} from 'vue';import Dialog from 'primevue/dialog';import Button from 'primevue/button';import {caeApi} from '../services/api'
const visible=defineModel('visible',{type:Boolean,default:false});const data=ref({contatos:[],horarios:[]});onMounted(async()=>data.value=(await caeApi.portal.cae()).data);const call=n=>window.location.href=`tel:+55${n.replace(/\D/g,'')}`
</script>
