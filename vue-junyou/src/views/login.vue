<script setup lang="ts">
import "@/assets/login.css";
import { ref } from "vue";
import { defineComponent, computed } from "vue";
import { useRouter } from "vue-router";
import { useLoginStore } from '@/stores/login'
import errorOrAccept from "@/components/errorOrAccept.vue"
const store = useLoginStore();
const router = useRouter();

//使用者輸入的帳號密碼
const userAccount = ref("");
const pwd = ref("");
//前端傳入帳號密碼做驗證 後端傳回一個值 當他等於true的時候登入成功
const loginState = ref(false);
//點選登入執行Component
const actorsComponent = ref(false)

// 假設登入都成功
const getuserAccountAndPwd = () => {
  loginState.value = false;
  actorsComponent.value = true;
  //登入成功傳遞pinia變數
  if (loginState.value) {
    store.increment()
    console.log("狀態:" + store.$state.loginState)
  }
};



//用來開關遮罩
const loginMuskOn = ref(false);
// 點選遮罩即關閉
const muskOff = () => {
  loginMuskOn.value = !loginMuskOn.value;
}

//跳轉秒數
// const acceptSecond = ref(3);
//秒數跑完跳轉網址
// const acceptSecondDown = () => {
//   acceptSecond.value--
//   dialogText2.value = `畫面於${acceptSecond.value}秒後跳轉`;
//   if (acceptSecond.value==-1) {
//       router.push("/shareExp")
//   }
// }
</script>


<template>
  <!-- 回上一頁 -->
  <RouterLink to="/">
    <img src="@/img/back.png" class="lg-back" title="back" alt="this is back" />
  </RouterLink>

  <div class="login">
    <RouterLink to="/"><img src="@/img/logo_75.jpg" alt="this is logo" class="logo" /></RouterLink>
    <p class="loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>

  <div class="inputAria">
    <p class="inputP">帳號:</p>
    <input type="text" class="inputText" placeholder="電子郵件" v-model="userAccount" />
  </div>
  <div class="inputAria2">
    <p class="inputP">密碼:</p>
    <input type="text" class="inputText" placeholder="包含大寫字母、小寫字母、數字" v-model="pwd" />
  </div>

  <div class="stateAria">
    <input type="checkbox" class="check" />
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
  <errorOrAccept v-if="actorsComponent" :state="loginState" />
</template>