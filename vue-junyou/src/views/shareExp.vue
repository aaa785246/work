<script setup lang="ts">
import "@/assets/shareExp.css";
import { onMounted, ref } from "vue";
import axios from "axios";
import { type articles } from "@/js/api";
// import { useArticleStore } from '@/stores/article'
import { useRouter } from "vue-router";
import { getCookie, setCookie } from "@/js/cookie";
import pag from "@/components/pagination.vue"
const router = useRouter();

const loginState = getCookie("loginState");
//如果他有在瀏覽網站並且有登入就保存它的登入狀態
if (loginState == "true") setCookie("loginState","true",20)
const searchContent = ref(getCookie("searchContent"))
const reload = ref("")
const reloadApi = () =>{
  reload.value = "true";
}
const finish = () =>{
  reload.value = "false";
}
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/">
    <img src="@/img/back.png" className="sh-back" title="back" alt="this is back" />
  </RouterLink>
  <div class="sh-middle">
    <!-- 搜尋框 -->
    <input type="text" placeholder="請輸入職業" class="sh-searchBox" v-model="searchContent"/>
    <!-- 搜尋框查詢按鈕 -->
    <img src="@/img/magnifying-glass.png" title="searchBoxGlass" alt="this is searchBoxGlass" class="sh-searchBoxGlass" @click="reloadApi"/>
  </div>
  <pag :content="searchContent" :reload="reload" @finish="finish" />
</template>