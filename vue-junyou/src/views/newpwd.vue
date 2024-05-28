<script setup lang="ts">
import "@/assets/newPwd.css";
import { getCookie, setCookie } from "@/js/cookie";
import { ref } from "vue";
import axios from "axios";
import successful from "@/components/successfulComponent.vue";
sessionStorage.removeItem("status")

const userPwd = ref("");
const checkUserPwd =ref("")
const remind = ref("")
const controllRemind = ref(false)
const switchDialog = ref(false);
const checkPwdSame = () =>{
  if (userPwd.value != checkUserPwd.value) {
    remind.value = "密碼請輸入相同"
    controllRemind.value = true;
  }else{
    controllRemind.value = false;
  }
}

const modeifyPwd = async () =>{
  const api = `http://192.168.1.200:8000/modeifyPwd`;
  await axios
    .post(api, {
      user_email:getCookie("userRegisterEmail"),
      user_pwd:userPwd.value
    })
    .then((response) => {
      switchDialog.value = true;
    setCookie("arrivedPage","/member",10)
    })
    .catch((err) => {
      console.log(err)
    });
}
const modeify = () =>{
  if (controllRemind.value == false) {
    modeifyPwd()
  }
}
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/login">
    <img
      src="@/img/back.png"
      className="np-back"
      title="back"
      alt="this is back"
    />
  </RouterLink>
  <div class="npPage">
    <RouterLink to="/">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="np-logo" />
    </RouterLink>
    <p class="np-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>

  <div class="np-inputAria">
    <p class="np-inputP">新密碼:</p>
    <input type="password" class="np-inputText" placeholder="包含大寫字母、小寫字母、數字" v-model="userPwd">
</div>
<div class="np-inputAria2">
    <p class="np-inputP">確認新密碼:</p>
    <input type="password" class="np-inputText" placeholder="重複新密碼" v-model="checkUserPwd" @blur="checkPwdSame">
  </div>
  <div v-if="controllRemind === true" class="np-remind">{{ remind }}</div>
<div class="np-loginbtnBox">
    <button class="np-loginbtn" @click="modeify">確認</button>
</div>
<successful v-if="switchDialog === true" :state="switchDialog" content="7" />
</template>
