<script setup lang="ts">
import "@/assets/register.css";
import { ref, watch, defineEmits, computed } from "vue";
const pwd = "";
const checkPwd = "";
const emits = defineEmits(["update:pwdValue","update:checkPwdValue"]);
const inputPwd = computed({
    get() {
        return pwd;
    },
    set(newValue) {
        emits("update:pwdValue", newValue);
    }
})
const inputCheckPwd = computed({
    get() {
        return checkPwd;
    },
    set(newValue) {
        emits("update:checkPwdValue", newValue);
    }
})
</script>
<template>
    <div class="reg-inputAria2">
    <p class="reg-inputP">新密碼:</p>
    <input type="text" class="reg-inputText" placeholder="包含大寫字母、小寫字母、數字" v-model="inputPwd"/>
  </div>
  <div class="reg-inputAria2">
    <p class="reg-inputP">重複確認新密碼:</p>
    <input type="text" class="reg-inputText" placeholder="重複確認新密碼" v-model="inputCheckPwd"/>
  </div>

</template>