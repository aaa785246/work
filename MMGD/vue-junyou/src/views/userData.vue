<script setup lang="ts">
import axios from "axios";
import "@/assets/userData.css"
import userInputEmail from "@/components/userInputEmail.vue";
import { getCookie, setCookie } from "@/js/cookie";
import { ref } from "vue";
import router from "@/router";
import successful from "@/components/successfulComponent.vue";
const init_userEmail = ref(getCookie("userEmail"));
const userEmail = ref("");
const userName = ref("");
const pwd = ref("");
const checkPwd = ref("");
const isSamePwd = ref(true);
const Registered = ref(false);
const getEmailValue = (newValue: string) => {
    userEmail.value = newValue;
}
const getUserNameValue = (newValue: string) => {
    userName.value = newValue;
}

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

const validPwd = ref(true);
const checkValidPwd = () => {
    const pwdPattern = /^(?=.*[A-Z]).{8,}$/;
    validPwd.value = pwdPattern.test(pwd.value)
    samePwd();
    if (pwd.value == "") {
        validPwd.value = true;
    }
}

const samePwd = () => {
    if (validPwd.value === false && checkPwd.value != pwd.value || validPwd.value === true && checkPwd.value != pwd.value) {
        isSamePwd.value = false;
    } else {
        isSamePwd.value = true;
    }
}


const modeifySuccessful = ref(false);

const modeifyBtn = async () => {

    //只修改密碼
    if (validPwd.value === true && isSamePwd.value === true && pwd.value != "") {
        await existedEmail()
        if (Registered.value != true) {
        modeifyUserData()
        modeifySuccessful.value = true;
        }
    }
    //修改電子郵件與姓名
    if (userEmail.value != "" || userName.value != "" && validPwd.value === true && isSamePwd.value === true) {
        // 先檢查電子郵件有沒有人用過
        await existedEmail()
        if (Registered.value != true) {
            modeifyUserData()
            setCookie("userName",userName.value,10);
            modeifySuccessful.value = true;
        }

    }

}

const goMember = () => {
    router.push("/member")
}


const modeifyUserData = async () => {
    // const api = `http://192.168.1.203:8000/modeifyuserdata`;
    const api = `http://172.20.10.3:8000/modeifyuserdata`;
    await axios
        .post(api, {
            init_userEmail: init_userEmail.value,
            user_email: userEmail.value,
            user_name: userName.value,
            user_pwd: pwd.value
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
    <userInputEmail :registed="Registered" @update:emailValue="getEmailValue" @update:userNameValue="getUserNameValue" />

    <div class="user-divider"></div>
    <div class="user-inputAria2">
        <p class="user-inputP">密碼:</p>
        <input type="password" class="user-inputText" placeholder="包含大寫字母、數字、8個字元" v-model="pwd"
            @blur="checkValidPwd" />
    </div>
    <div v-if="!validPwd && pwd != ``" class="stop">密碼不符合格式</div>
    <div class="user-inputAria2">
        <p class="user-inputP">確認密碼:</p>
        <input type="password" class="user-inputText" placeholder="重複確認新密碼" v-model="checkPwd" @blur="samePwd" />
    </div>
    <div v-if="!isSamePwd" class="stop">密碼不相同</div>

    <div class="user-loginbtnBox">
        <button class="user-loginbtn" @click="modeifyBtn">修改</button>
    </div>
    <successful v-if="modeifySuccessful" :state="modeifySuccessful" content="10" />
</template>