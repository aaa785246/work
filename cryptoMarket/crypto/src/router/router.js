import { createRouter, createWebHistory } from 'vue-router'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [  
    {
      path:'/',
      name:'cryptoList',
      component:()=>import("../views/CryptoList.vue")
    },
    {
      path:'/currencyDetails',
      name:'currencyDetails',
      component:()=>import("../views/CurrencyDetails.vue")
    },
  ]
})

export default router

