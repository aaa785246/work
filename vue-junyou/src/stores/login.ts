import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useLoginStore = defineStore('userName', {
  state:()=>({userName:""}),
  persist:true
},

)

