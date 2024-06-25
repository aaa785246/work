<script setup lang="ts">
import "@/assets/emailCheck.css";
import axios from "axios";
import emailjs from 'emailjs-com';
import { setCookie, getCookie } from "@/js/cookie";
import { ref } from "vue";
import successful from "@/components/successfulComponent.vue";
import failed from "@/components/failedComponent.vue"
import router from "@/router";
const registerStateDialog = ref<boolean>(false);
const switchDialog = ref<boolean>(false);
const userName = ref(getCookie("userName"));
const userEmail = ref(getCookie("userEmail"));
const pwd = ref(getCookie("pwd"));
const isRegister = ref(getCookie("isRegister"))
const verify = ref("");
const userInput = ref("");
const content = ref("")
function verifyRandomInt(min: number, max: number) {
  return Math.floor((Math.random() * (max - min)) + min);
}
for (let i = 0; i < 6; i++) {
  verify.value += verifyRandomInt(0, 9).toString();
}
emailjs.init('q6I64WVaTUYi401rI');
const serviceID = 'service_rlazx1v';
const templateID = 'template_l8o7kzc';
const emailContent = {
  email: userEmail.value,
  userName: userName.value,
  message: verify.value
}
emailjs.send(serviceID, templateID, emailContent);

const register = async () => {
  // const api = `http://192.168.1.203:8000/register`;
  const api = `http://172.20.10.3:8000/register`;
  await axios
    .post(api, {
      userName: userName.value,
      email: userEmail.value,
      password: pwd.value
    })
    .then((response) => {
      console.log(response.data)
    })
    .catch((err) => {
      console.log(err)
    });
};
const registerOrChangePwd = () => {
  if (userInput.value == verify.value) {
    if (isRegister.value == "Y") {
      register();
      registerStateDialog.value = true;
      switchDialog.value = true
      content.value="2";
      setCookie("arrivedPage","/login",10)
    }else {
      registerStateDialog.value = true;
      switchDialog.value = true
      content.value="3";
      setCookie("arrivedPage","/newpwd",10)
    }

  }
}
const componentClose = () => {
  switchDialog.value = false;
  registerStateDialog.value = false;
}
const goLogin = () =>{
  router.push("/login")
}
const goHomePage = () =>{
  router.push("/")
}
</script>

<template>
  <!-- 回上一頁 -->
    <img src="@/img/back.png" className="em-back" title="back" alt="this is back" @click="goLogin"/>
  <div class="emailPage">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="email-logo" @click="goHomePage"/>
    <p class="email-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>
  <div class="email-textBox">
    <div class="email-text">
      已將驗證信寄至您所註冊的電子郵件信箱{{ userEmail }}，請回傳驗證碼。
    </div>
  </div>
  <div class="email-inputAria2">
    <input type="text" class="email-inputText" placeholder="驗證碼" v-model="userInput" />
  </div>


  <div class="email-loginbtnBox">
    <button class="email-loginbtn" @click="registerOrChangePwd">確認</button>
  </div>
  <successful v-if="registerStateDialog === true" :state="switchDialog" :content="content" />
  <failed v-else :state="switchDialog" @close-dialog="componentClose" :content="content" />
</template>