import axios from 'axios'

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:7047/api',
  headers: { 'Content-Type': 'application/json' }
})

export const caeApi = {
  contatos: {
    list: () => api.get('/cae/contatos'),
    create: data => api.post('/cae/contatos', data),
    update: (id, data) => api.put(`/cae/contatos/${id}`, data),
    remove: id => api.delete(`/cae/contatos/${id}`)
  },
  horarios: {
    list: () => api.get('/cae/horarios'),
    create: data => api.post('/cae/horarios', data),
    update: (id, data) => api.put(`/cae/horarios/${id}`, data),
    remove: id => api.delete(`/cae/horarios/${id}`)
  },
  faqs: {
    list: () => api.get('/cae/faqs/admin'),
    create: data => api.post('/cae/faqs', data),
    update: (id, data) => api.put(`/cae/faqs/${id}`, data),
    remove: id => api.delete(`/cae/faqs/${id}`)
  },
  servicos: {
    list: () => api.get('/conteudos/servicos/admin'),
    create: data => api.post('/conteudos/servicos', data),
    update: (id, data) => api.put(`/conteudos/servicos/${id}`, data),
    remove: id => api.delete(`/conteudos/servicos/${id}`)
  },
  portal: {
    cae: () => api.get('/portal/cae'),
    servicos: () => api.get('/portal/servicos')
  }
}
