<script setup lang="ts">
import { ref, defineProps, watch,watchEffect,onMounted,nextTick } from "vue";
import { useRouter } from "vue-router";
import "@/assets/errorOrAccept.css";
const router = useRouter();
const props = defineProps<{
  state: boolean;
  content:string;
}>();

const emit=defineEmits(['closeDialog'])

// prop轉成watchEffect可以偵測的ref
const stateRef = ref(props.state);
const content = ref(props.content)
const dialog = ref<HTMLDialogElement | null>(null);
// const model = defineModel();
onMounted(()=>{
  watchEffect(() => {
  if (stateRef.value) {
    if (dialog.value !== null) {
      dialog.value.showModal();
    } else {
      console.error("dialog 加載失敗");
    }
  } else {
    if (dialog.value !== null) {
      dialog.value.showModal();
    } else {
      console.error("dialog 加載失敗");
    }
  }
});
})

const closeModel = () => {
dialog.value?.close()
dialog.value = null;
emit('closeDialog')
}

//跳轉秒數
const acceptSecond = ref(3);
//秒數跑完跳轉網址
const acceptSecondDown = () => {
  acceptSecond.value--
  
  if (acceptSecond.value==-1) {
      router.push("/shareExp")
  }
}

</script>

<template>
  <!--  成功給true 失敗給false -->
  <div v-if="stateRef">
    <dialog ref="dialog" id="dialog" >
      <img src="@/img/accept.png" alt="" class="imgOn" />
      <div class="content">
        <p v-html="content"></p>
      </div>
      <div class="content2">
        <button class="content-btn" @click="closeModel" >確定</button>
      </div>
    </dialog>
    
  </div>
  <div v-else>
    <dialog ref="dialog" id="dialog" >
      <img src="@/img/warning.png" alt="" class="imgOn" />
      <div class="content">
        <p v-html="content"></p>
      </div>
      <div class="content2">
        <button class="content-btn" @click="closeModel" >確定</button>
      </div>
    </dialog>
  </div>
</template>
