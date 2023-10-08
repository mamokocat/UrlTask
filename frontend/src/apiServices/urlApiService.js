import axios from 'axios'

const instance = axios.create({
    baseURL: process.env.VUE_APP_API_URL,
    timeout: 5000
  });

export default {
    tryRedirect(path) {
        return instance.get(`/urls/redirect/${path}`).then(response => response)
    },
    getUrlList(skip, take) {
        return instance.get(`/urls?skip=${skip}&take=${take}`).then(response => {
            return response.data
        })
    },
    getUrl(id) {
        return instance.get(`/urls/${id}`).then(response => {
            return response.data
        })
    },
    validatePath(path, isEditMode) {
        // eslint-disable-next-line
        debugger
        return instance.get(`/urls/validatePath?path=${path}&isEditMode=${isEditMode}`)
    },
    addUrl(url) {
        return instance.put("/urls", url)
    },
    editUrl(url, id) {
        return instance.post(`/urls/${id}`, url)
    },
    deleteUrl(id) {
        return instance.delete(`/urls/${id}`)
    }
}