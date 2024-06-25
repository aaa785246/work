<script setup>
import { ref } from 'vue'
import { setCookie, getCookie } from "../cookie";
const imgUrl = ref(getCookie("imgUrl"));
const USD = ref(getCookie("美元當前金額"));
const TWD = ref(getCookie("台幣當前金額"));
const changePct24Hour = ref(parseFloat(getCookie("漲幅")));
</script>
<template>
    <div class="bg-gray-900 text-white p-4">
  <div class="flex items-center justify-between py-2 border-b border-gray-700">
    <div class="flex items-center w-20">
      <img :src="imgUrl" alt="Crypto Image" class="w-8">
    </div>
    <div class="text-right mr-20 w-20">
      <div>{{ USD }}USD</div>
    </div>

    <div class="text-right mr-20 w-30">
        <!-- 價格 -->
        <div>{{ TWD }} TWD</div>
      </div>
    <div :class="changePct24Hour >0? 'text-green-500 w-16':'text-red-500 w-16'">
      <!-- 24小時變動量 -->
      {{changePct24Hour}}%
    </div>
  </div>
</div>
</template>