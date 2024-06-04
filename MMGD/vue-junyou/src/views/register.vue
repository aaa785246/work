<script setup lang="ts">
import "@/assets/register.css";
import userInputEmail from "@/components/userInputEmail.vue";
import router from "@/router";
import axios from "axios";
import { ref } from "vue";
import { setCookie, getCookie } from "@/js/cookie";
import { useRouter } from "vue-router";
const Registered = ref(false);
const existedEmail = async () => {
  // const api = `http://192.168.1.203:8000/existed`;
  const api = `http://172.20.10.3:8000/existed`;
  await axios
    .post(api, {
      user_email: userEmail.value,
    })
    .then((response) => {
      if (response.data == userEmail.value) {
        Registered.value = true;
      } else {
        Registered.value = false;
      }
    })
    .catch((err) => {
      console.log(err)
    });
}
const routerP = useRouter();
const userEmail = ref("");
const userName = ref("");
const pwd = ref("");
const checkPwd = ref("");
const isSamePwd = ref(true);

const getEmailValue = (newValue: string) => {
  userEmail.value = newValue;
}
const getUserNameValue = (newValue: string) => {
  userName.value = newValue;
}

const validPwd = ref(true);
const checkValidPwd = () => {
  const pwdPattern = /^(?=.*[A-Z]).{8,}$/;
  validPwd.value = pwdPattern.test(pwd.value)
  samePwd();
}

const samePwd = () => {
  if (validPwd.value === false && checkPwd.value != pwd.value || validPwd.value === true && checkPwd.value != pwd.value) {
    isSamePwd.value = false;
  } else {
    isSamePwd.value = true;
  }
}

const registerBtn = async () => {
  //信箱驗證、密碼驗證、密碼重複、名稱有填入則可以到下一頁
  await existedEmail();
  if (validPwd.value === true && isSamePwd.value === true && userEmail.value && userName.value && Registered.value === false) {
    setCookie("userName", userName.value, 10);
    setCookie("userEmail", userEmail.value, 10);
    setCookie("pwd", pwd.value, 5);
    setCookie("isRegister", "Y", 10);
    routerP.push("/emailcheck")
  }
}

const goHomePage = () => {
  router.push("/")
}
const goLogin = () => {
  router.push("/login")
}
</script>

<template>
  <!-- 回上一頁 -->
  <img src="@/img/back.png" className="rg-back" title="back" alt="this is back" @click="goLogin" />
  <div class="registerPage">
    <img src="@/img/logo_75.jpg" alt="this is logo" class="reg-logo" @click="goHomePage" />
    <p class="reg-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>
  <userInputEmail :registed="Registered" @update:emailValue="getEmailValue" @update:userNameValue="getUserNameValue" />

  <div class="reg-inputAria2">
    <p class="reg-inputP">密碼:</p>
    <input type="password" class="reg-inputText" placeholder="包含大寫字母、數字、8個字元" v-model="pwd" @blur="checkValidPwd" />
  </div>
  <div v-if="!validPwd" class="stop">密碼不符合格式</div>
  <div class="reg-inputAria2">
    <p class="reg-inputP">確認密碼:</p>
    <input type="password" class="reg-inputText" placeholder="重複確認新密碼" v-model="checkPwd" @blur="samePwd" />
  </div>
  <div v-if="!isSamePwd" class="stop">密碼不相同</div>

  <div class="reg-loginbtnBox">
    <button class="reg-loginbtn" @click="registerBtn">註冊</button>
  </div>
</template>
