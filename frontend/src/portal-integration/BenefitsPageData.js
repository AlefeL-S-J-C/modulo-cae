import { caeApi } from '../services/api'

export async function loadBenefitsServices() {
  const { data } = await caeApi.portal.servicos()
  return data
}

// Na BenefitsPage.vue atual, substitua o array local `services` por:
// const services = ref([])
// onMounted(async () => services.value = await loadBenefitsServices())
//
// Os campos retornados pela API correspondem ao conteúdo atualmente usado:
// nome/categoria/tituloCurto/textoCurto/descricaoCompleta/destaques/detalhes/contato/linkExterno/icone/destaque/ordem.
