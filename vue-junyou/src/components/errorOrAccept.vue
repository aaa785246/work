<script setup lang="ts">
import { ref, defineProps, watch,watchEffect,onMounted,nextTick } from "vue";
import { useRouter } from "vue-router";
import "@/assets/errorOrAccept.css";

const props = defineProps<{
  state: boolean;
  content:string;
}>();
// prop轉成watchEffect可以偵測的ref
const stateRef = ref(props.state);
const content = ref(props.content)
const falseDialog = ref<HTMLDialogElement | null>(null);

const test = ()=>{
  console.log(falseDialog)
}

onMounted(()=>{
  watchEffect(() => {
  if (stateRef.value) {
    console.log("1");
  } else {
    if (falseDialog.value !== null) {
      falseDialog.value.showModal();
    } else {
      console.error("falseDialog 加載失敗");
    }
  }
});
})


</script>

<template>
  <!-- 失敗給0 成功給1 -->
  <div v-if="stateRef">
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
  <div v-else>
    <!-- <button @click="test">測試</button> -->
    <dialog ref="falseDialog" id="dialog">
      <img src="@/img/warning.png" alt="" class="imgOn" />
      <div class="content">
        <p v-html="content" ></p>
      </div>
    </dialog>
  </div>
</template>
