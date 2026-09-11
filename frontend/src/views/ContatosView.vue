<template>
  <section>
    <PageHeader title="Central de Atendimento" subtitle="Telefones e canais usados pelo modal Entre em contato do Portal do Aluno." @add="openNew"/>
    <DataTable :value="items" stripedRows paginator :rows="10" :loading="loading" class="mt-6 overflow-hidden rounded-2xl border">
      <Column field="tipo" header="Tipo"/>
      <Column field="numero" header="Número"/>
      <Column field="descricao" header="Descrição"/>
      <Column header="Status"><template #body="{data}"><Tag :value="data.ativo?'Ativo':'Inativo'" :severity="data.ativo?'success':'secondary'"/></template></Column>
      <Column field="ordem" header="Ordem"/>
      <Column header="Ações" style="width:130px"><template #body="{data}">
        <div class="flex gap-2"><Button icon="pi pi-pencil" text rounded @click="edit(data)"/><Button icon="pi pi-trash" text rounded severity="danger" @click="remove(data)"/></div>
      </template></Column>
    </DataTable>
    <Dialog v-model:visible="visible" modal header="Contato" class="w-full max-w-lg">
      <div class="grid gap-4">
        <div><label class="label">Tipo</label><InputText v-model="form.tipo" class="w-full"/></div>
        <div><label class="label">Número</label><InputText v-model="form.numero" class="w-full"/></div>
        <div><label class="label">Descrição</label><InputText v-model="form.descricao" class="w-full"/></div>
        <div><label class="label">Ordem</label><InputNumber v-model="form.ordem" class="w-full"/></div>
        <div class="flex items-center gap-3"><ToggleSwitch v-model="form.ativo"/><span>Publicado no Portal</span></div>
      </div>
      <template #footer><Button label="Cancelar" text @click="visible=false"/><Button label="Salvar" icon="pi pi-check" @click="save"/></template>
    </Dialog>
    <ConfirmDialog/>
    <Toast/>
  </section>
</template>
<script setup>
import {ref,onMounted} from 'vue'
import {caeApi} from '../services/api'
import PageHeader from '../components/PageHeader.vue'
import DataTable from 'primevue/datatable'; import Column from 'primevue/column'; import Button from 'primevue/button'; import Dialog from 'primevue/dialog'; import InputText from 'primevue/inputtext'; import InputNumber from 'primevue/inputnumber'; import ToggleSwitch from 'primevue/toggleswitch'; import Tag from 'primevue/tag'; import ConfirmDialog from 'primevue/confirmdialog'; import Toast from 'primevue/toast'; import {useConfirm} from 'primevue/useconfirm'; import {useToast} from 'primevue/usetoast'
const items=ref([]),loading=ref(false),visible=ref(false),editing=ref(null),confirm=useConfirm(),toast=useToast()
const blank=()=>({tipo:'Telefone',numero:'',descricao:'',ordem:1,ativo:true});const form=ref(blank())
const load=async()=>{loading.value=true;try{items.value=(await caeApi.contatos.list()).data}finally{loading.value=false}}
const openNew=()=>{editing.value=null;form.value=blank();visible.value=true};const edit=x=>{editing.value=x;form.value={...x};visible.value=true}
const save=async()=>{if(!form.value.numero)return;editing.value?await caeApi.contatos.update(editing.value.id,form.value):await caeApi.contatos.create(form.value);visible.value=false;toast.add({severity:'success',summary:'Salvo',detail:'Conteúdo atualizado',life:2500});load()}
const remove=x=>confirm.require({message:`Excluir ${x.numero}?`,header:'Confirmar exclusão',icon:'pi pi-exclamation-triangle',accept:async()=>{await caeApi.contatos.remove(x.id);load()}})
onMounted(load)
</script>
<style scoped>.label{display:block;margin-bottom:.35rem;font-size:.875rem;font-weight:600;color:#334155}</style>
