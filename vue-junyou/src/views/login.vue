<script setup lang="ts">
import "@/assets/login.css"

</script>
<template>
<div class="login">
    <img src="@/img/logo_75.jpg" alt="this is logo" class="logo">
    <p class="loginTitle">輕鬆獲取 <br>所需要的面試資訊</p>
</div>

<div class="inputAria">
    <p class="inputP">帳號:</p>
    <input type="text" class="inputText" placeholder="電子郵件或帳號">
</div>
<div class="inputAria2">
    <p class="inputP">密碼:</p>
    <input type="text" class="inputText" placeholder="包含大寫字母、小寫字母、數字">
</div>

<div class="stateAria">
    <input type="checkbox" class="check">
    <div class="remember">
        記住帳號
    </div>
    <div class="register">註冊</div>
    <div class="forget">忘記密碼?</div>
</div>
</template>