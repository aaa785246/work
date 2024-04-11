import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useLoginStore = defineStore('loginState', () => {
  const loginState = ref(false)
  function increment() {
    loginState.value = !loginState.value
  }

  return { loginState, increment }
})
