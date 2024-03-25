<script setup lang="ts">
import "@/assets/newPwd.css";
sessionStorage.removeItem("status")
</script>

<template>
  <!-- 回上一頁 -->
  <RouterLink to="/login">
    <img
      src="@/img/back.png"
      className="np-back"
      title="back"
      alt="this is back"
    />
  </RouterLink>
  <div class="npPage">
    <RouterLink to="/">
      <img src="@/img/logo_75.jpg" alt="this is logo" class="np-logo" />
    </RouterLink>
    <p class="np-loginTitle">輕鬆獲取 <br />所需要的面試資訊</p>
  </div>

  <div class="np-inputAria">
    <p class="np-inputP">新密碼:</p>
    <input type="text" class="np-inputText" placeholder="包含大寫字母、小寫字母、數字">
</div>
<div class="np-inputAria2">
    <p class="np-inputP">確認新密碼:</p>
    <input type="text" class="np-inputText" placeholder="重複新密碼">
</div>
<div class="np-loginbtnBox">
    <button class="np-loginbtn">確認</button>
</div>
</template>
