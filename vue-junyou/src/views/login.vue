<script setup lang="ts">
import "@/assets/login.css";
import { ref } from "vue";
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
const router = useRouter();
//使用者輸入的帳號密碼
const userAccount = ref("");
const pwd = ref("");
//前端傳入帳號密碼做驗證 後端傳回一個布靈值 當他等於true的時候登入成功
const loginDataCheck = ref(false);
// 登入判斷
const getuserAccountAndPwd = () => {
  // if (loginDataCheck.value) {
  //    //登入成功dialog彈出並轉址
  //    router.push('/')
  // }else{
  //    //登入失敗dialog彈出
  //    console.log("帳號或密碼錯誤")
  // }
  loginDataCheck.value = !loginDataCheck.value;
};
</script>
<template>
  <!-- 回上一頁 -->
  <RouterLink to="/">
    <img
      src="@/img/back.png"
      className="lg-back"
      title="back"
      alt="this is back"
    />
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
      placeholder="電子郵件或帳號"
      v-model="userAccount"
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
    <input type="checkbox" class="check" />
    <div class="remember">記住帳號</div>
    <RouterLink to="/register"><div class="register">註冊</div></RouterLink>
    <RouterLink to="/forgetPwd"> <div class="forget">忘記密碼</div></RouterLink>
  </div>
  <div class="loginbtnBox">
    <button class="loginbtn" @click="getuserAccountAndPwd">登入</button>
  </div>
  <div :class="loginDataCheck ? 'lg-menuMaskOn' : 'lg-menuMaskOff'"></div>
  <div :class="loginDataCheck ? 'lg-dialogOn' : 'lg-dialogOff'"></div>
  <img src="@/img/warning.png" alt="" :class="loginDataCheck?'lg-warningImgOn':'lg-warningImgOff'">
  <div :class="loginDataCheck ? 'lg-dialogTextOn' : 'lg-dialogTextOff'">你他媽</div>
</template>
