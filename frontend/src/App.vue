<template>
  <div class="min-h-screen bg-slate-50">
    <aside class="fixed inset-y-0 left-0 hidden w-64 border-r border-slate-200 bg-white lg:block">
      <div class="flex h-16 items-center gap-3 border-b px-6">
        <div class="flex h-9 w-9 items-center justify-center rounded-xl bg-[#073C87] text-white"><i class="pi pi-headphones"/></div>
        <div><strong class="text-sm">Gestão de Conteúdo</strong><p class="text-xs text-slate-500">CAE</p></div>
      </div>
      <nav class="p-4 space-y-1">
        <RouterLink v-for="item in menu" :key="item.to" :to="item.to"
          class="flex items-center gap-3 rounded-xl px-4 py-3 text-sm font-medium text-slate-600 hover:bg-slate-100 [&.router-link-active]:bg-blue-50 [&.router-link-active]:text-[#073C87]">
          <i :class="item.icon"/>{{ item.label }}
        </RouterLink>
      </nav>
    </aside>

    <main class="lg:pl-64">
      <header class="sticky top-0 z-30 flex h-16 items-center justify-between border-b bg-white/90 px-4 backdrop-blur md:px-8">
        <div>
          <p class="text-xs font-semibold uppercase tracking-wider text-[#3565b3]">Portal do Aluno</p>
          <h1 class="font-bold text-slate-800">Gestão de Conteúdo do CAE</h1>
        </div>
        <div class="flex h-9 w-9 items-center justify-center rounded-full bg-slate-100 text-sm font-bold">CAE</div>
      </header>
      <div class="p-4 md:p-8"><RouterView/></div>
    </main>
  </div>
</template>

<script setup>
const menu = [
  { to:'/dashboard', label:'Dashboard', icon:'pi pi-home' },
  { to:'/contatos', label:'Central de Atendimento', icon:'pi pi-phone' },
  { to:'/horarios', label:'Horários', icon:'pi pi-clock' },
  { to:'/faqs', label:'Central de Ajuda', icon:'pi pi-question-circle' },
  { to:'/servicos', label:'Serviços', icon:'pi pi-briefcase' }
]
</script>
