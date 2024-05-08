<script setup lang="ts">
import "@/assets/register.css";
import { ref, watch, defineEmits, computed } from "vue";
const email = ref("");
const userName = ref("");
const emits = defineEmits(["update:emailValue", "update:userNameValue"]);

const validEmail = ref(true);
const checkEmail = () => {
    const emailPattern = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/;
    validEmail.value = emailPattern.test(email.value);
    //輸入內容通過正規表達式後才傳送到父層
    if (validEmail.value) {
        emits("update:emailValue", email.value);
        // console.log("資料已完成");
    }
}
const userNameEmit = () => {
    emits("update:userNameValue",userName.value);
}
</script>
<template>
    <div class="reg-inputAria">
        <p class="reg-inputP">電子郵件:</p>
        <input type="text" class="reg-inputText" placeholder="xxx@xxx.com" v-model="email" @blur="checkEmail" />
    </div>
    <div v-if="!validEmail" class="stop">電子郵件不符合格式</div>
    <div class="reg-inputAria2">
        <p class="reg-inputP">名稱:</p>
        <input type="text" class="reg-inputText" placeholder="名稱" v-model="userName" @blur="userNameEmit"/>
    </div>
</template>