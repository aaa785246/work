<script setup lang="ts">
import "@/assets/forgetpwd.css";
import { ref } from "vue";
import { setCookie, getCookie } from "@/js/cookie";
import { useRouter } from "vue-router";
import axios from "axios";
const router = useRouter();
const regEmail = ref("");
const validEmail = ref(true);
const remind = ref("")
const switchPage = () => {
  if (validEmail.value== true) {
    setCookie("userEmail", regEmail.value, 10);
    setCookie("isRegister","N",10);
    router.push("/emailcheck")
  }
}

const emailPattern = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/;
const validEmailfunc = () => {
  validEmail.value = emailPattern.test(regEmail.value);
  remind.value = "信箱不符合格式"
  existedEmail()
}

const existedEmail = async () => {
  // const api = `http://192.168.1.203:8000/existed`;
  const api = `http://192.168.1.200:8000/existed`;
  await axios
    .post(api, {
      user_email: regEmail.value,
    })
    .then((response) => {
    //  這裡
    if (emailPattern.test(response.data)) {
      regEmail.value = response.data
    }else{
      remind.value = response.data
      validEmail.value = false;
    }
    })
    .catch((err) => {
      console.log(err)
    });
}
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/login">
    <img src="@/img/back.png" className="fg-back" title="back" alt="this is back" />
  </RouterLink>
  <div class="forgetPage">
    <RouterLink to="/">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="fg-logo" />
    </RouterLink>
    <p class="fg-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>
  <div class="fg-textBox">
    <div class="fg-text">
      註冊時使用的電子郵件
    </div>
  </div>
  <div class="fg-inputAria2">
    <input type="text" class="fg-inputText" placeholder="電子郵件" v-model="regEmail" @blur="validEmailfunc"/>
  </div>
  <div v-if="!validEmail" class="stop">{{remind}}</div>
  <div class="fg-loginbtnBox">
    <button class="fg-loginbtn" @click="switchPage">獲取驗證碼</button>
  </div>

</template>
