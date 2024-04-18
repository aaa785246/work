<script setup lang="ts">
import { ref, defineProps, watch, watchEffect, onMounted } from "vue";
import { useRouter } from "vue-router";
import { transformRoutes } from "@/js/dialog";
const router = useRouter();

const props = defineProps<{
  state: boolean;
}>();

const dialog = ref<HTMLDialogElement | null>(null);

//修改父層狀態
const emit = defineEmits(["closeDialog"]);

//秒數跑完跳轉網址
const transformSec = ref(1);

onMounted(() => {
  if (props.state === false) {
    dialog.value?.close();
    return;
  }
  dialog.value?.showModal();
  transformRoutes(transformSec, "/member");
});

watch(
  () => props.state,
  () => {
    if (props.state === false) {
      dialog.value?.close();
      return;
    }
    dialog.value?.showModal();
  }
);
</script>

<template>
  <!--  成功給true 失敗給false -->
  <dialog ref="dialog" id="dialog">
    <img src="@/img/accept.png" alt="" class="imgOn" />
    <div class="content">
      <p>登入成功，畫面於{{ transformSec }}後跳轉</p>
    </div>
  </dialog>
</template>

<style scoped>
.imgOn {
  position: relative;
  width: 100px;
  top: 10px;
  left: 60px;
  z-index: 4;
}
#dialog {
  --max-width: 390px;
  width: 220px;
  height: 250px;
  background-color: #f6f1ed;
  border: none;
  border-radius: 15px;
  /* margin-bottom:800px ; */
}

#dialog::backdrop {
  max-width: var(--max-width);
  left: calc(50% - (var(--max-width) / 2));
  background-color: rgba(0, 0, 0, 0.3);
}

.content {
  display: flex;
  justify-content: center;
  margin-top: 30px;
}

.content > p {
  height: 30px;
}

.content2 {
  display: flex;
  justify-content: center;
  margin-top: 10px;
}
.content-btn {
  background-color: #eadfdf;
  border: none;
  border-radius: 5px;
  padding: 20px 50px;
}
</style>
