<script setup lang="ts">
import "@/assets/emailCheck.css";
const data = sessionStorage.getItem('status');
</script>
<template>
  <!-- 回上一頁 -->
  <RouterLink to="/login">
    <img
        src="@/img/back.png"
        className="em-back"
        title="back"
        alt="this is back"
      />
</RouterLink>
    <div class="emailPage">
    <RouterLink to="/">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="email-logo" />
    </RouterLink>
    <p class="email-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>
  <div class="email-textBox">
    <div class="email-text">
    已將驗證信寄至您所註冊的電子 <br>
    郵件信箱，請回傳驗證碼。
  </div>
  </div>
  <div class="email-inputAria2">
    <input
      type="text"
      class="email-inputText"
      placeholder="驗證碼"
    />
  </div>

  <RouterLink  :to="data === '1' ? '/newPwd' : '/'" >
    <div class="email-loginbtnBox">
    <button class="email-loginbtn">確認</button>
  </div>
</RouterLink>
  
</template>