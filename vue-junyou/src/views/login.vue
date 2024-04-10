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
const loginStateCheck = ref(false);
//用來開關遮罩的變數
const loginMuskOn = ref(false);
//登入狀態決定dialog框彈出內容
const dialogText = ref("");
const dialogImgSrc = ref("@src/img/warning.png");
const acceptSecond = ref(3);
// 登入判斷
const getuserAccountAndPwd = () => {
  // 當loginState(帳密)驗證成功傳回true
  if (loginStateCheck.value==true){
    //開啟遮罩
    loginMuskOn.value= true;
    // 更新dialog內容與圖片
    dialogText.value = "登入成功";
    dialogImgSrc.value = `${import.meta.env.BASE_URL}src/img/accept.png`
    //跳轉網址
    setInterval(acceptSecondDown,1000);
    


  }else {
    //開啟遮罩
    loginMuskOn.value= true;
    // 更新dialog內容與圖片
    dialogText.value = "登入失敗";
    dialogImgSrc.value = `${import.meta.env.BASE_URL}src/img/warning.png`;
  }
};
// 點選遮罩即關閉
const muskOff = () => {
  loginMuskOn.value = ! loginMuskOn.value;
}

//跳轉網址function
const acceptSecondDown = () => {
  acceptSecond.value--
  if (acceptSecond.value==-1) {
      router.push("/shareExp")
    }
}

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
    <div class="register"><RouterLink to="/register"><div >註冊</div></RouterLink></div>
    <div class="forget"><RouterLink to="/forgetPwd"><div>忘記密碼</div></RouterLink></div>
   
  </div>
  <div class="loginbtnBox">
    <button class="loginbtn" @click="getuserAccountAndPwd">登入</button>
  </div>
  <!-- 登入成功或失敗遮罩 -->
  <div :class="loginMuskOn ? 'lg-menuMaskOn' : 'lg-menuMaskOff'" @click="muskOff"></div>
  <div :class="loginMuskOn ? 'lg-dialogOn' : 'lg-dialogOff'"></div>
  <img :src="dialogImgSrc" alt="" :class="loginMuskOn?'lg-warningImgOn':'lg-warningImgOff'">
  <!-- <div :class="loginMuskOn ? 'lg-dialogTextOn' : 'lg-dialogTextOff'">{{dialogText}} </div> -->
  <div :class="loginMuskOn ? 'lg-dialogTextOn' : 'lg-dialogTextOff'">畫面於{{acceptSecond}}秒後跳轉...</div>
</template>
