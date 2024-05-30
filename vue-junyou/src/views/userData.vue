<script setup lang="ts">
import axios from "axios";
import "@/assets/userData.css"
import userInputEmail from "@/components/userInputEmail.vue";
import { getCookie, setCookie } from "@/js/cookie";
import { ref } from "vue";
import router from "@/router";
const init_userEmail = ref(getCookie("userEmail"));
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




const registerBtn = () => {
    //信箱驗證、密碼驗證、密碼重複、名稱有填入則可以到下一頁
    if (validPwd.value === true && isSamePwd.value === true || userEmail.value!="" || userName.value!="") {
        modeifyUserData()
        setCookie("userName",userName.value,60);
    }
}

const goMember = () => {
    router.push("/member")
}


const modeifyUserData = async () => {
    // const api = `http://192.168.1.203:8000/modeifyuserdata`;
    const api = `http://192.168.1.200:8000/modeifyuserdata`;
    await axios
        .post(api, {
            init_userEmail:init_userEmail.value,
            user_email: userEmail.value,
            user_name:userName.value,
            user_pwd:pwd.value
        })
        .then((response) => { 
            console.log(response.data)
        });
};
</script>
<template>
    <div class="user-memberAria">
        <img src="@/img/back.png" class="user-back" title="back" alt="this is back" @click="goMember" />
        <div class="user-title">會員資料</div>
    </div>
    <userInputEmail @update:emailValue="getEmailValue" @update:userNameValue="getUserNameValue" />

    <div class="user-divider"></div>
    <div class="user-inputAria2">
        <p class="user-inputP">密碼:</p>
        <input type="password" class="user-inputText" placeholder="包含大寫字母、數字、8個字元" v-model="pwd" @blur="checkValidPwd" />
    </div>
    <div v-if="!validPwd && pwd!=``" class="stop">密碼不符合格式</div>
    <div class="user-inputAria2">
        <p class="user-inputP">確認密碼:</p>
        <input type="password" class="user-inputText" placeholder="重複確認新密碼" v-model="checkPwd" @blur="samePwd" />
    </div>
    <div v-if="!isSamePwd" class="stop">密碼不相同</div>

    <div class="user-loginbtnBox">
        <button class="user-loginbtn" @click="registerBtn">修改</button>
    </div>
</template>