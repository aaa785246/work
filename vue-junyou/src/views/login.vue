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
const loginStateCheck = ref(true);
//用來開關遮罩
const loginMuskOn = ref(false);
//登入狀態決定dialog框彈出內容(圖片、文字)
const dialogImgSrc = ref("@src/img/warning.png");
const dialogText = ref("");
const dialogText2 = ref("");
//跳轉秒數
const acceptSecond = ref(3);
//登入成功失敗間不同class的判斷
const classNameState = ref(true);

// 登入成功失敗判斷
const getuserAccountAndPwd = () => {
  // 當loginState(帳密)驗證成功傳回true
  if (loginStateCheck.value == true) {
    //開啟遮罩
    loginMuskOn.value = true;
    // 更新dialog內容與圖片
    dialogImgSrc.value = `${import.meta.env.BASE_URL}src/img/accept.png`;
    dialogText.value = "登入成功";
    dialogText2.value = `畫面於${acceptSecond.value}秒後跳轉`;
    //倒數跳轉網址
    setInterval(acceptSecondDown,1000);
    //登入成功失敗套用不同class
    ClassChange("textOn2","lg-dialogTextOff","lg-dialogTextOnPush")
    classNameState.value = true;
  } else {
    //開啟遮罩
    loginMuskOn.value = true;
    // 更新dialog內容與圖片
    dialogImgSrc.value = `${import.meta.env.BASE_URL}src/img/warning.png`;
    dialogText.value = "登入失敗";
    dialogText2.value = "帳號或密碼輸入錯誤";
    //登入成功失敗套用不同class
    ClassChange("textOn2","lg-dialogTextOff","lg-dialogTextOnErr")
    classNameState.value = false;
  }

};

// 點選遮罩即關閉
const muskOff = () => {
  loginMuskOn.value = !loginMuskOn.value;

  //將登入成功失敗的去添加的class換成關閉dialog時的class
  if (classNameState.value == true) {
    ClassChange("textOn2","lg-dialogTextOnPush","lg-dialogTextOff")
    //手動關遮罩 跳轉網址
    router.push("/shareExp")
  }else{
    ClassChange("textOn2","lg-dialogTextOnErr","lg-dialogTextOff")
  }
  
};

//秒數跑完跳轉網址
const acceptSecondDown = () => {
  acceptSecond.value--
  dialogText2.value = `畫面於${acceptSecond.value}秒後跳轉`;
  if (acceptSecond.value==-1) {
      router.push("/shareExp")
  }
}

//根據不同狀態做class轉換的函式
function ClassChange(id:string,oldClassName: string,newClassName: string) 
{
  const element = document.getElementById(id) as HTMLDivElement;
  element.classList.replace(oldClassName, newClassName);
}
</script>


<template>
  <!-- 回上一頁 -->
  <RouterLink to="/">
    <img
      src="@/img/back.png"
      class="lg-back"
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
    <div class="register">
      <RouterLink to="/register"><div>註冊</div></RouterLink>
    </div>
    <div class="forget">
      <RouterLink to="/forgetPwd"><div>忘記密碼</div></RouterLink>
    </div>
  </div>
  <div class="loginbtnBox">
    <button class="loginbtn" @click="getuserAccountAndPwd">登入</button>
  </div>
  <!-- 登入成功或失敗遮罩 -->
  <div
    :class="loginMuskOn ? 'lg-menuMaskOn' : 'lg-menuMaskOff'" @click="muskOff"></div>
  <div :class="loginMuskOn ? 'lg-dialogOn' : 'lg-dialogOff'"></div>
  <img :src="dialogImgSrc" alt="" :class="loginMuskOn ? 'lg-warningImgOn' : 'lg-warningImgOff'"
  />
  <div :class="loginMuskOn ? 'lg-dialogTextOn' : 'lg-dialogTextOff'">
    {{ dialogText }}
  </div>
  <div id="textOn2" class="lg-dialogTextOff">{{ dialogText2 }}</div>
</template>
