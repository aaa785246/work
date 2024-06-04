<script setup lang="ts">
import Vue from "vue";
import axios from "axios";
import "@/assets/login.css";
import { ref, watch } from "vue";
import { useRouter } from "vue-router";
import successful from "@/components/successfulComponent.vue";
import failed from "@/components/failedComponent.vue"
import { setCookie, getCookie } from "@/js/cookie";
const router = useRouter();
//預設去到的頁面
if (getCookie("arrivedPage") == null || getCookie("arrivedPage") == "/login") setCookie("arrivedPage", "/member", 10)
//使用者輸入的帳號密碼
const userEmail = ref(getCookie("rememberEmail") || "");
const pwd = ref("");
//記住帳號操作 checkBox打勾->登入成功setCookie->下次自動帶入input框
const IsCheckRememberEmail = ref(false);
//登入成功失敗開啟哪一個dialog
const loginStateDialog = ref<boolean>(false);
const switchDialog = ref<boolean>(false);
// 定義函式來呼叫 API
const signin = async () => {
  // const api = `http://192.168.1.203:8000/loginCheck`;
  const api = `http://172.20.10.3:8000/loginCheck`;
  await axios
    .post(api, {
      user_email: userEmail.value,
      user_password: pwd.value,
    })
    .then((response) => {
      switchDialog.value = true;
      loginStateDialog.value = true;
      setCookie("userName", response.data.user_name, 60)
      setCookie("userEmail", userEmail.value, 60);
      setCookie("loginState", response.data.verify, 10);
      // 記住帳號
      if (IsCheckRememberEmail.value === true) setCookie("rememberEmail", userEmail.value, 43200);
    })
    .catch(() => {
      switchDialog.value = true;
      loginStateDialog.value = false;
    });
};


//component關閉
const componentClose = () => {
  switchDialog.value = false;
  loginStateDialog.value = false;
};
const checkRemember = () => {
  IsCheckRememberEmail.value = !IsCheckRememberEmail.value;
}

const goRegister = () => {
  router.push("/register")
}
const goHomePage = () => {
  router.push("/")
}

const goForget= () => {
  router.push("/forgetPwd")
}
</script>

<template>
  <!-- 回上一頁 -->

  <img src="@/img/back.png" class="lg-back" title="back" alt="this is back" @click="goHomePage" />


  <div class="login">
    <img src="@/img/logo_75.jpg" alt="this is logo" class="logo" @click="goHomePage" />
    <p class="loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>

  <div class="inputAria">
    <p class="inputP">電子郵件:</p>
    <input type="text" class="inputText" placeholder="電子郵件" v-model="userEmail" />
  </div>
  <div class="inputAria2">
    <p class="inputP">密碼:</p>
    <input type="password" class="inputText" placeholder="包含大寫字母、小寫字母、數字" v-model="pwd" />
  </div>

  <div class="stateAria">
    <input type="checkbox" class="check" v-model="IsCheckRememberEmail" />
    <div class="remember" @click="checkRemember">記住電子郵件</div>
    <div class="register" @click="goRegister">
      <div>註冊</div>
    </div>
    <div class="forget" @click="goForget">
        <div>忘記密碼</div>
    </div>
  </div>
  <div class="loginbtnBox">
    <button class="loginbtn" @click="signin">登入</button>
  </div>
  <successful v-if="loginStateDialog === true" :state="switchDialog" content="1" />
  <failed v-else :state="switchDialog" @close-dialog="componentClose" content="1" />
</template>