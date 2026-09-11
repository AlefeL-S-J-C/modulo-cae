<template>
<section>
  <PageHeader title="Serviços do Portal" subtitle="Gerencie os cards e detalhes exibidos na página de benefícios/serviços." @add="novo"/>

  <div class="mt-6 flex flex-wrap items-center gap-3">
    <IconField class="min-w-52 flex-1">
      <InputIcon class="pi pi-search"/>
      <InputText v-model="search" placeholder="Buscar serviço..." class="w-full"/>
    </IconField>
    <Dropdown v-model="categoriaFiltro" :options="categoriaOptions" optionLabel="label" optionValue="value" placeholder="Todas as categorias" class="w-full sm:w-64"/>
  </div>

  <DataTable :value="filteredItems" :loading="loading" paginator :rows="8" :rowsPerPageOptions="[8,16,32]" paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink RowsPerPageDropdown" class="mt-4 overflow-hidden rounded-2xl border" stripedRows sortable emptyMessage="Nenhum serviço encontrado">
    <Column field="icone" header="" :style="{width:'3.5rem'}"><template #body="{data}"><i :class="data.icone" class="text-lg text-[#073C87]"/></template></Column>
    <Column field="nome" header="Serviço" sortable><template #body="{data}"><div class="font-semibold">{{data.nome}}</div><div class="text-xs text-slate-500">{{data.tituloCurto}}</div></template></Column>
    <Column field="categoria" header="Categoria" sortable/>
    <Column field="ordem" header="Ordem" sortable/>
    <Column header="Destaque"><template #body="{data}"><ToggleSwitch :model-value="data.destaque" @update:model-value="toggleDestaque(data, $event)"/></template></Column>
    <Column header="Status"><template #body="{data}"><div class="flex items-center gap-2"><ToggleSwitch :model-value="data.ativo" @update:model-value="toggleAtivo(data, $event)"/><Tag :value="data.ativo?'Publicado':'Oculto'" :severity="data.ativo?'success':'secondary'"/></div></template></Column>
    <Column header="Ações" :style="{width:'8.5rem'}"><template #body="{data}"><div class="flex gap-1"><Button icon="pi pi-eye" text rounded @click="preview(data)"/><Button icon="pi pi-pencil" text rounded @click="editar(data)"/><Button icon="pi pi-trash" text rounded severity="danger" @click="excluir(data)"/></div></template></Column>
  </DataTable>

  <Dialog v-model:visible="visible" modal header="Serviço" class="w-full max-w-3xl">
    <div class="grid gap-4">
      <div class="grid grid-cols-1 gap-4 sm:grid-cols-2">
        <div><label class="label">Nome *</label><InputText v-model="form.nome" class="w-full"/></div>
        <div><label class="label">Categoria *</label><Dropdown v-model="form.categoria" :options="categoriasServico" editable filter placeholder="Selecione ou digite a categoria" class="w-full"/></div>
      </div>
      <div><label class="label">Título curto</label><InputText v-model="form.tituloCurto" class="w-full"/></div>
      <div><label class="label">Texto curto</label><Textarea v-model="form.textoCurto" rows="3" class="w-full"/></div>
      <div><label class="label">Descrição completa</label><Textarea v-model="form.descricaoCompleta" rows="5" class="w-full"/></div>
      <div class="grid grid-cols-1 gap-4 sm:grid-cols-2">
        <div><label class="label">Ícone PrimeIcons</label><Dropdown v-model="form.icone" :options="primeIcons" optionLabel="label" optionValue="value" filter placeholder="Selecione um ícone" class="w-full"><template #value="{value}"><i :class="value"></i><span class="ml-2 truncate">{{value}}</span></template><template #option="{option}"><i :class="option.value"></i><span class="ml-2">{{option.label}}</span></template></Dropdown></div>
        <div><label class="label">Contato</label><InputText v-model="form.contato" class="w-full"/></div>
      </div>
      <div><label class="label">Detalhes</label><Textarea v-model="form.detalhes" rows="3" class="w-full"/></div>
      <div><label class="label">Link externo</label><InputText v-model="form.linkExterno" class="w-full"/></div>
      <div><label class="label">Destaques (um por linha)</label><Textarea v-model="highlightsText" rows="5" class="w-full"/></div>
      <div class="flex flex-wrap gap-5">
        <div class="flex items-center gap-2"><ToggleSwitch v-model="form.destaque"/><span>Destaque</span></div>
        <div class="flex items-center gap-2"><ToggleSwitch v-model="form.ativo"/><span>Publicado</span></div>
        <div><span class="mr-2 text-sm font-semibold text-slate-600">Ordem</span><InputNumber v-model="form.ordem" placeholder="Ordem"/></div>
      </div>
    </div>
    <template #footer><Button label="Cancelar" text @click="visible=false"/><Button label="Salvar" icon="pi pi-check" :loading="saving" @click="salvar"/></template>
  </Dialog>

  <Dialog v-model:visible="previewVisible" modal header="Pré-visualização" class="w-full max-w-2xl">
    <div v-if="previewItem" class="rounded-2xl border p-6">
      <div class="flex gap-4">
        <div class="flex h-14 w-14 shrink-0 items-center justify-center rounded-xl bg-blue-50 text-2xl text-[#073C87]"><i :class="previewItem.icone"/></div>
        <div><p class="text-xs font-bold uppercase tracking-widest text-blue-600">{{previewItem.categoria}}</p><h3 class="text-xl font-bold">{{previewItem.tituloCurto}}</h3></div>
      </div>
      <p class="mt-5 text-slate-600">{{previewItem.descricaoCompleta}}</p>
      <div v-if="previewItem.destaques && previewItem.destaques.length" class="mt-5">
        <strong>Destaques</strong>
        <ul class="mt-2 space-y-1">
          <li v-for="(d,i) in previewItem.destaques" :key="i" class="flex items-start gap-2 text-sm text-slate-600"><i class="pi pi-check-circle mt-1 text-blue-600"/>{{d.texto}}</li>
        </ul>
      </div>
      <div class="mt-5 rounded-xl bg-slate-50 p-4">
        <strong>Informações</strong>
        <p v-if="previewItem.detalhes" class="mt-2 text-sm text-slate-600">{{previewItem.detalhes}}</p>
        <p class="mt-2 font-semibold">{{previewItem.contato}}</p>
        <a v-if="previewItem.linkExterno" :href="previewItem.linkExterno" target="_blank" rel="noopener" class="mt-2 inline-block font-semibold text-blue-600">Saiba mais sobre este serviço</a>
      </div>
    </div>
  </Dialog>

  <ConfirmDialog/>
  <Toast/>
</section>
</template>

<script setup>
import { ref, onMounted, computed, watch } from 'vue'
import { caeApi } from '../services/api'
import PageHeader from '../components/PageHeader.vue'
import { primeIcons, categoriasServico } from '../services/icones'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import InputNumber from 'primevue/inputnumber'
import ToggleSwitch from 'primevue/toggleswitch'
import Tag from 'primevue/tag'
import Dropdown from 'primevue/dropdown'
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import ConfirmDialog from 'primevue/confirmdialog'
import Toast from 'primevue/toast'
import { useConfirm } from 'primevue/useconfirm'
import { useToast } from 'primevue/usetoast'

const items = ref([])
const visible = ref(false)
const previewVisible = ref(false)
const previewItem = ref(null)
const editing = ref(null)
const loading = ref(false)
const saving = ref(false)
const search = ref('')
const categoriaFiltro = ref('')
const confirm = useConfirm()
const toast = useToast()
const form = ref({})

const highlightsText = computed({
  get: () => (form.value.destaques || []).map(x => x.texto).join('\n'),
  set: v => (form.value.destaques = v.split('\n').filter(Boolean).map((texto, i) => ({ texto, ordem: i + 1 })))
})

const categoriaOptions = computed(() => [{ label: 'Todas as categorias', value: '' }, ...categoriasServico.map(c => ({ label: c, value: c }))])

const filteredItems = computed(() => {
  const q = search.value.toLowerCase().trim()
  return items.value.filter(i => {
    if (categoriaFiltro.value && i.categoria !== categoriaFiltro.value) return false
    if (!q) return true
    return [i.nome, i.categoria, i.tituloCurto, i.textoCurto, i.detalhes].some(v => (v || '').toLowerCase().includes(q))
  })
})

const erro = detail => toast.add({ severity: 'error', summary: 'Erro', detail, life: 4000 })

const load = async () => {
  loading.value = true
  try { items.value = (await caeApi.servicos.list()).data }
  catch { erro('Não foi possível carregar os serviços.') }
  finally { loading.value = false }
}

const novo = () => {
  editing.value = null
  form.value = { nome:'', categoria:'Atendimento', tituloCurto:'', textoCurto:'', descricaoCompleta:'', icone:'pi pi-briefcase', contato:'', detalhes:'', linkExterno:'', destaque:false, ativo:true, ordem:items.value.length+1, destaques:[] }
  visible.value = true
}

const editar = x => {
  editing.value = x
  form.value = { ...x, destaques: (x.destaques || []).map(d => ({ ...d })) }
  visible.value = true
}

const salvar = async () => {
  if (!form.value.nome?.trim()) return erro('Informe o nome do serviço.')
  if (!form.value.categoria?.trim()) return erro('Informe a categoria.')
  saving.value = true
  try {
    editing.value ? await caeApi.servicos.update(editing.value.id, form.value) : await caeApi.servicos.create(form.value)
    visible.value = false
    toast.add({ severity: 'success', summary: 'Salvo', detail: 'Serviço atualizado', life: 2000 })
    load()
  }
  catch { erro('Não foi possível salvar o serviço.') }
  finally { saving.value = false }
}

const preview = x => { previewItem.value = x; previewVisible.value = true }

const excluir = x => confirm.require({
  message: `Excluir "${x.nome}"?`,
  header: 'Confirmar exclusão',
  icon: 'pi pi-exclamation-triangle',
  acceptLabel: 'Excluir',
  rejectLabel: 'Cancelar',
  acceptClass: 'p-button-danger',
  accept: async () => {
    try {
      await caeApi.servicos.remove(x.id)
      toast.add({ severity: 'success', summary: 'Excluído', detail: 'Serviço removido', life: 2000 })
      load()
    }
    catch { erro('Não foi possível excluir o serviço.') }
  }
})

const toggleDestaque = async (data, val) => {
  const antes = data.destaque
  data.destaque = val
  try { await caeApi.servicos.update(data.id, data) }
  catch { data.destaque = antes; erro('Não foi possível atualizar o destaque.') }
}

const toggleAtivo = async (data, val) => {
  const antes = data.ativo
  data.ativo = val
  try { await caeApi.servicos.update(data.id, data) }
  catch { data.ativo = antes; erro('Não foi possível atualizar o status.') }
}

onMounted(load)
</script>

<style scoped>
.label{display:block;margin-bottom:.35rem;font-size:.875rem;font-weight:600;color:#334155}
</style>