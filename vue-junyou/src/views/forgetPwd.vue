<script setup lang="ts">
import "@/assets/forgetpwd.css";
sessionStorage.setItem('status', '1');
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/login">
    <img
      src="@/img/back.png"
      className="fg-back"
      title="back"
      alt="this is back"
    />
  </RouterLink>
  <div class="forgetPage">
    <RouterLink to="/">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="fg-logo" />
    </RouterLink>
    <p class="fg-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>
  <div class="fg-textBox">
    <div class="fg-text">
    註冊時使用的電子郵件:
  </div>
  </div>
  <div class="fg-inputAria2">
    <input
      type="text"
      class="fg-inputText"
      placeholder="電子郵件"
    />
  </div>
  <RouterLink to="/emailcheck">
    <div class="fg-loginbtnBox">
    <button class="fg-loginbtn">確認</button>
  </div>
</RouterLink>
</template>
