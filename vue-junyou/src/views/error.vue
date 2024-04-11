<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import '@/assets/error.css'
</script>

<template>
  <div class="err-dialogOn"></div>
  <img src='@/img/warning.png' class="err-warningImgOn" />
  <div class="err-dialogTextOn">發生錯誤</div>
  <div class="err-dialogTextOn2">沒有登入或憑證過期</div>
  <RouterLink to="/login"> <div class="err-dialogTextOn3">立即登入</div></RouterLink>
  <div id="textOn2" class="err-dialogTextOff"></div>
</template>
