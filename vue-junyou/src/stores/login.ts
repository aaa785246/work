import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useLoginStore = defineStore('loginPiniaState', {
  state:()=>({loginPiniaState:false,seconds:0}),
  actions:{
    increment() {
  this.loginPiniaState = !this.loginPiniaState
    },
    countTime() {
      setInterval(()=>{
        this.seconds ++
      },1000)
     
    },
    setInitial(){
      this.loginPiniaState = false;
      this.seconds = 0 ;
    }
  },
  persist:true
}
)

