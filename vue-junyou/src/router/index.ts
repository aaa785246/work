import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/homepage.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name:'homepage',
      component:HomePage
    },
    {
      path:'/login',
      name:'login',
      component:()=>import("@/views/login.vue")
    },
    {
      path:'/register',
      name:'register',
      component:()=>import("@/views/register.vue")
    },
    {
      path:'/emailCheck',
      name:'emailCheck',
      component:()=>import("@/views/emailCheck.vue")
    },
    {
      path:'/forgetpwd',
      name:'forgetpwd',
      component:()=>import("@/views/forgetPwd.vue")
    },
    {
      path:'/newpwd',
      name:'newpwd',
      component:()=>import("@/views/newpwd.vue")
    },
    {
      path:'/shareExp',
      name:'shareExp',
      component:()=>import("@/views/shareExp.vue")
    },
    {
      path:'/article',
      name:'article',
      component:()=>import("@/views/article.vue")
    }, {
      path:'/member',
      name:'member',
      component:()=>import("@/views/member.vue")
    },
  ]
})

export default router
