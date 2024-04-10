<script setup lang="ts">
import "@/assets/member.css";
import "@/assets/animate.css";
import { ref } from "vue";
import { defineComponent } from "vue";
import { useRouter } from "vue-router";

const userName = ref("馬克");
const myMenuToggle = ref(false);
const artic = ref("塞內加有說過一句話，勇氣通往天堂，怯懦通往地獄。");
const toggleMenu = () => {
  myMenuToggle.value = !myMenuToggle.value;
};
</script>

<template>
  <div class="memberAria">
    <!-- 回上一頁 -->
    <RouterLink to="/">
      <img
        src="@/img/back.png"
        class="mem-back"
        title="back"
        alt="this is back"
      />
    </RouterLink>
    <div class="userName">{{ userName }}</div>
    <img
      src="@/img/bell.png"
      class="bell"
      title="bell"
      alt="this is bell"
      @click="toggleMenu"
    />
  </div>
  <div>
    <!-- 菜單開關 -->
    <div :class="myMenuToggle ? 'mem-maskdivOn' : 'mem-maskdivOff'">
      <!-- 遮罩 -->
      <div
        :class="myMenuToggle ? 'mem-menuMaskOn' : 'menuMaskOff'"
        @click="toggleMenu"
      ></div>
      <!-- 菜單 -->
      <div :class="myMenuToggle ? 'mem-menuOn' : 'menuOff'">
        <div class="mem-articleBox2">
          <div class="mem-title">{{ artic }}</div>
        </div>
      </div>
      <!-- 使用者名稱 -->
      <div :class="myMenuToggle ? 'userNameMaskOn' : 'userNameMaskOff'">
        {{ userName }}
      </div>

      <!-- 關閉鈕 -->
      <img
        src="@/img/close.png"
        id="close"
        title="close"
        alt="this is close"
        :class="myMenuToggle ? 'mem-closeButtonOn' : 'closeButtonOff'"
        @click="toggleMenu"
      />
    </div>
  </div>
</template>
