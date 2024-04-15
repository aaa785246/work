<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { defineProps } from "vue";
import '@/assets/errorOrAccept.css'

const props = defineProps<{
  state: number;
}>();


</script>

<template>
  <!-- 失敗給0 成功給1 -->
  <div class="err-dialogOn" v-if="$props.state === 0">
    <img src='@/img/warning.png' class="err-warningImgOn" />
    <div class="err-dialogTextOn">發生錯誤</div>
    <div class="err-dialogTextOn2">沒有登入或憑證過期</div>
    <RouterLink to="/login">
      <div class="err-btn">
        <div class="err-dialogTextOn3">立即登入</div>
      </div>
    </RouterLink>
    <div id="textOn2" class="err-dialogTextOff"></div>
  </div>
  <div v-else>
    <div class="lg-menuMaskOn"></div>
    <div class="lg-dialogOn"></div>
    <img src="@/img/accept.png" alt="" class= "lg-warningImgOn" />
    <div class="lg-dialogTextOn">
      登入成功
    </div>
    <div id="textOn2" class="lg-dialogTextOff">於三秒後跳轉</div>
  </div>
</template>
