<script setup lang="ts">
import "@/assets/register.css";
</script>

<template>
<!-- 回上一頁 -->
<RouterLink to="/login">
    <img
        src="@/img/back.png"
        className="rg-back"
        title="back"
        alt="this is back"
      />
</RouterLink>
  <div class="registerPage">
    <RouterLink to="/">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="reg-logo" />
    </RouterLink>
    <p class="reg-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>
  <div class="reg-inputAria">
    <p class="reg-inputP">帳號:</p>
    <input type="text" class="reg-inputText" placeholder="電子郵件或帳號" />
  </div>
  <div class="reg-inputAria2">
    <p class="reg-inputP">名稱:</p>
    <input type="text" class="reg-inputText" placeholder="名稱" />
  </div>
  <div class="reg-inputAria2">
    <p class="reg-inputP">電子郵件:</p>
    <input type="text" class="reg-inputText" placeholder="xxx@xxx.com" />
  </div>
  <div class="reg-inputAria2">
    <p class="reg-inputP">密碼:</p>
    <input
      type="text"
      class="reg-inputText"
      placeholder="包含大寫字母、小寫字母、數字"
    />
  </div>
  <div class="reg-inputAria2">
    <p class="reg-inputP">確認新密碼:</p>
    <input type="text" class="reg-inputText" placeholder="重複確認新密碼" />
  </div>

  <RouterLink to="emailCheck">
    <div class="reg-loginbtnBox">
    <button class="reg-loginbtn">註冊</button>
  </div>
</RouterLink>

</template>
