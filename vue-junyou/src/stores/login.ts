import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useLoginStore = defineStore('loginPiniaState', {
  state:()=>({loginPiniaState:false}),
  actions:{
    increment() {
  this.loginPiniaState = !this.loginPiniaState
    }
  },
  persist:true
}
)


// const persistPlugin = createPersistPlugin();
