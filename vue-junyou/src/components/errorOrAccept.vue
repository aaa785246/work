<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { defineProps } from "vue";
import "@/assets/errorOrAccept.css";

const props = defineProps<{
  state: number;
}>();
</script>

<template>
  <!-- 失敗給0 成功給1 -->
  <div v-if="$props.state === 0">
    <!-- <div class="menuMaskOn"></div>
    <div class="dialogOn"></div>
    <img src="@/img/warning.png" class="imgOn" />
    <div class="dialogTextOn" > 
    <p>發生錯誤</p>
    <p>沒有登入或憑證過期</p>
    </div>
    <RouterLink to="/login">
      <div class="err-btn">
        <div class="err-dialogTextOn3">立即登入</div>
      </div>
    </RouterLink> -->
    <dialog id="myDialog">
      <p>測試</p>
      <button id="closeBtn">Close</button>
    </dialog>
  </div>
  <div v-else>
    <div class="menuMaskOn"></div>
    <div class="dialogOn"></div>
    <img src="@/img/accept.png" alt="" class="imgOn" />
    <div class="dialogTextOn">
      <p>登入成功</p>
      <p>畫面於3秒後跳轉</p>
    </div>
  </div>
</template>
