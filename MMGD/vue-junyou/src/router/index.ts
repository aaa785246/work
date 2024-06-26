import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/homepage.vue'
import { useArticleStore } from '@/stores/article'

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
      component:()=>import("/src/views/register.vue")
    },
    {
      path:'/emailcheck',
      name:'emailcheck',
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
      path:'/shareexp',
      name:'shareexp',
      component:()=>import("@/views/shareExp.vue")
    },
    {
      path:'/member',
      name:'member',
      component:()=>import("@/views/member.vue")
    },{
      path:'/post',
      name:'post',
      component:()=>import("@/views/post.vue")
    },
    {
      path:'/personalArticle',
      name:'personalArticle',
      component:()=>import("@/views/personalArticle.vue")
    },
    {
      path:'/collectArticle',
      name:'collectArticle',
      component:()=>import("@/views/collectArticle.vue")
    },
    {
      path:`/article/:article_number`,
      name:'article',
      component:()=>import("@/views/article.vue")
    }, {
      path:`/userData`,
      name:'userData',
      component:()=>import("@/views/userData.vue")
    }
  ]
})

export default router
