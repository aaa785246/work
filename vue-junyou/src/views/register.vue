<script setup lang="ts">
import "@/assets/register.css";
import userInputEmail from "@/components/userInputEmail.vue";
import router from "@/router";
import { ref } from "vue";
import { setCookie, getCookie } from "@/js/cookie";
import { useRouter } from "vue-router";
const routerP = useRouter();
const userEmail = ref("");
const userName = ref("");
const pwd = ref("");
const checkPwd = ref("");
const isSamePwd = ref(true);
const getEmailState = (newValue: string) => {
  userEmail.value = newValue;
}
const getUserNameState = (newValue: string) => {
  userName.value = newValue;
}

const validPwd = ref(true);
const checkValidPwd = () => {
const pwdPattern =/^(?=.*[A-Z]).{8,}$/;
validPwd.value = pwdPattern.test(pwd.value)
samePwd();
}

const samePwd = () => {
  if (validPwd.value === false && checkPwd.value != pwd.value || validPwd.value === true && checkPwd.value != pwd.value) {
    isSamePwd.value = false;
  }else {
    isSamePwd.value = true;
  }
}

const registerBtn = () => {
  //信箱驗證、密碼驗證、密碼重複、名稱有填入則可以到下一頁
  if (validPwd.value === true && isSamePwd.value === true && userEmail.value && userName.value) {
    setCookie("userName",userName.value,10);
    setCookie("userEmail",userEmail.value,10);
    setCookie("pwd",pwd.value,5);
    setCookie("isRegister","Y",10);
    routerP.push("/emailcheck")
  }
}

</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/login">
    <img src="@/img/back.png" className="rg-back" title="back" alt="this is back" />
  </RouterLink>
  <div class="registerPage">
    <RouterLink to="/">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="reg-logo" />
    </RouterLink>
    <p class="reg-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>
  <userInputEmail @update:emailValue="getEmailState" @update:userNameValue="getUserNameState" />

  <div class="reg-inputAria2">
    <p class="reg-inputP">密碼:</p>
    <input type="password" class="reg-inputText" placeholder="包含大寫字母、數字、8個字元" v-model="pwd" @blur="checkValidPwd"/>
  </div>
  <div v-if="!validPwd" class="stop">密碼不符合格式</div>
  <div class="reg-inputAria2">
    <p class="reg-inputP">確認密碼:</p>
    <input type="password" class="reg-inputText"  placeholder="重複確認新密碼" v-model="checkPwd" @blur="samePwd"/>
  </div>
  <div v-if="!isSamePwd" class="stop">密碼不相同</div>

  <div class="reg-loginbtnBox">
    <button class="reg-loginbtn" @click="registerBtn">註冊</button>
  </div>
</template>
