<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { defineProps } from "vue";
import "@/assets/errorOrAccept.css";

const props = defineProps<{
  state: boolean;
}>();
</script>

<template>
  <!-- 失敗給0 成功給1 -->
  <div v-if="props.state">
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
  </div>
  <div class="test" v-else>
    <!-- <div class="menuMaskOn"></div>
    <div class="dialogOn"></div>
    <img src="@/img/accept.png" alt="" class="imgOn" />
    <div class="dialogTextOn">
      <p>登入成功</p>
      <p>畫面於3秒後跳轉</p>
    </div> -->
    <dialog id="myDialog" class="dialog">
      <p>測試</p>
    </dialog>
  </div>
</template>
