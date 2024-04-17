<script setup lang="ts">
import "@/assets/login.css";
import { ref, watch } from "vue";
import { useRouter } from "vue-router";
import { useLoginStore } from '@/stores/login'
import errorOrAccept from "@/components/errorOrAccept.vue"
import {  getUserDataList } from '@/js/api';
const store = useLoginStore();
const router = useRouter();
//使用者輸入的帳號密碼
const userEmail= ref("");
const pwd = ref("");
const seconds = ref(store.seconds)
//前端傳入帳號密碼做驗證 後端傳回一個值 當他等於true的時候登入成功
const loginState = ref(false);
//dialog文字
const content = ref("");
//點選登入執行Component
// const modelValue = defineModel<Boolean>();
const actorsComponent = ref(false)
interface user{
email:string;
pwd:string;
};
const user= ref<user>()
  store.setInitial();
// 假設登入都成功
const getuserAccountAndPwd = async() => {
  const userData:user[] = await getUserDataList();
  if (userData) {
    //清空登入狀態
    // loginState.value = false;
    
    const userDataTmp = userData.find(
    v =>{ return v.email === userEmail.value && v.pwd === pwd.value})

    //帳號驗證後有該筆資料
    if (userDataTmp != undefined) {
      loginState.value = true;
      store.increment()
      store.countTime()
      console.log("登入狀態:" + store.loginPiniaState)
      content.value = "登入成功，"  
    }else{
      content.value = "登入失敗，帳號或密碼錯誤。"  
    }
    actorsComponent.value = true;
  }
 
};


// 偵測pinia秒數
watch(seconds,(newValue,oldValue)=>{
  if (newValue) {
    console.log(seconds)
    // store.setZero()
  }
})
//component關閉
const componentClose=()=>{
  actorsComponent.value=false
}

const test = () => {
  console.log("pinia狀態:" + store.loginPiniaState)
}
const test2 = () => {
  console.log("秒數:" + store.seconds)
}
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
    <input type="text" class="inputText" placeholder="電子郵件" v-model="userEmail" />
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
  <button class="loginbtn" @click="test">狀態</button>
  <button class="loginbtn" @click="test2">秒數</button>
  <errorOrAccept v-if="actorsComponent"  :state="loginState" :content="content" @close-dialog="componentClose"/>
</template>