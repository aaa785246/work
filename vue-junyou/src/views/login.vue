<script setup lang="ts">
import axios from 'axios';
import Vue from 'vue';
import "@/assets/login.css";
import { ref, watch } from "vue";
import { useRouter } from "vue-router";
import { useLoginStore } from "@/stores/login";
import loginSuccessful from "@/components/loginSuccessful.vue";
import loginFailed from "@/components/loginFailed.vue";
// import type {user} from '@/js/api';
import { getUserDataList, type user } from "@/js/api";
import { setCookie, getCookie } from "@/js/cookie";
import EmailCheck from "./emailCheck.vue";
const store = useLoginStore();
const router = useRouter();
//使用者輸入的帳號密碼
const userEmail = ref("");
const pwd = ref("");
//記住帳號操作 checkBox->getCookie->帶入input框
const rememberEmailBox = ref(false);
const rememberEmail= getCookie("rememberEmail")
if (rememberEmail) userEmail.value = rememberEmail;
const rememberEmailfunc = () =>{
rememberEmailBox.value = !rememberEmailBox.value
console.log(rememberEmailBox.value)
}

//登入成功失敗開啟哪一個dialog
const loginStateDialog = ref<boolean>(false);
const switchDialog = ref<boolean>(false);


new Vue({
  el:'#app',
  data:{
    "user_email": "",
    "user_password": ""
  }
  methods:{
    signIn(){
      const api =`http://192.168.50.193:8001/api/loginCheck`
      axios.
    }
  }
})

















const user = ref<user>();
// 假設登入都成功
const getuserAccountAndPwd = async () => {
  const userData: user[] = await getUserDataList();
  if (userData) {
    const userDataTmp = userData.find((v) => {
      return v.email === userEmail.value && v.pwd === pwd.value;
    });

    //帳號驗證後有該筆資料
    if (userDataTmp != undefined) {
      switchDialog.value = true;
      loginStateDialog.value = true;
      store.userName = userDataTmp.userName;
      setCookie("loginState", "Y", 10);
      //記住帳號
      if (rememberEmailBox.value) setCookie("rememberEmail",userDataTmp.email,10); 
    } else {
      switchDialog.value = true;
      loginStateDialog.value = false;
    }
  }
};

//component關閉
const componentClose = () => {
  switchDialog.value = false;
  loginStateDialog.value = false;
};
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/">
    <img src="@/img/back.png" class="lg-back" title="back" alt="this is back" />
  </RouterLink>

  <div class="login">
    <RouterLink to="/"
      ><img src="@/img/logo_75.jpg" alt="this is logo" class="logo"
    /></RouterLink>
    <p class="loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>

  <div class="inputAria">
    <p class="inputP">帳號:</p>
    <input
      type="text"
      class="inputText"
      placeholder="電子郵件"
      v-model="userEmail"
    />
  </div>
  <div class="inputAria2">
    <p class="inputP">密碼:</p>
    <input
      type="text"
      class="inputText"
      placeholder="包含大寫字母、小寫字母、數字"
      v-model="pwd"
    />
  </div>

  <div class="stateAria">
    <input type="checkbox" class="check" v-model="rememberEmailBox" @click="rememberEmailfunc"/>
    <div class="remember">記住帳號</div>
    <div class="register">
      <RouterLink to="/register">
        <div>註冊</div>
      </RouterLink>
    </div>
    <div class="forget">
      <RouterLink to="/forgetPwd">
        <div>忘記密碼</div>
      </RouterLink>
    </div>
  </div>
  <div class="loginbtnBox">
    <button class="loginbtn" @click="getuserAccountAndPwd">登入</button>
  </div>
  <loginSuccessful
    v-if="loginStateDialog === true"
    :state="switchDialog"
  />
  <loginFailed
  v-else
    :state="switchDialog"
    @close-dialog="componentClose"
  />
</template>
