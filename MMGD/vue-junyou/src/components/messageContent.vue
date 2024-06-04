<script setup lang="ts">
import "@/assets/article.css";
import { ref, onMounted } from "vue";
// import { getMessageList } from "@/js/api";

//定義message物件告訴TS
interface Message {
  userName: string;
  message: string;
  
}

//給予初始值避免API資料還沒抵達前渲染造成的問題
const message = ref<Message[]>([{ userName: "", message: "" }]);
//掛載api
onMounted(async () => {
  //留言api
  // const messageData = await getMessageList();
  // message.value = messageData;
});
</script>
<template>
  <div class="ar-articleBox" v-for="(msg, index) in message" :key="index">
    <div class="ar-messageName">{{ msg.userName }}</div>
    <div class="ar-content">{{ msg.message }}</div>
  </div>
</template>
