<script setup lang="ts">
import { ref, defineProps, watch, watchEffect, onMounted, computed } from "vue";
import { useRouter } from "vue-router";
import { transformRoutes } from "@/js/dialog";
const router = useRouter();

const props = defineProps<{
  state: boolean;
}>();

const dialog = ref<HTMLDialogElement | null>(null);

//修改父層狀態
const emit = defineEmits(["closeDialog"]);

//點擊確定後跳轉網址
const closeModel = () => {
  emit("closeDialog");
};

onMounted(() => {
  if (props.state === false) {
    dialog.value?.close();
  } else {
    dialog.value?.showModal();
  }
});

watch(
  () => props.state,
  () => {
    if (props.state === false) {
      dialog.value?.close();
    } else {
      dialog.value?.showModal();
    }
  }
);
</script>

<template>
  <dialog ref="dialog" id="dialog2">
    <div>
      <img src="@/img/warning.png" alt="" class="imgOn" />
      <div class="content">
        <p>登入失敗，帳號或密碼錯誤。</p>
      </div>
      <div class="content2">
        <button class="content-btn" @click="closeModel">確定</button>
      </div>
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
#dialog2 {
  --max-width: 390px;
  width: 220px;
  height: 300px;
  background-color: #f6f1ed;
  border: none;
  border-radius: 15px;
}

#dialog2::backdrop {
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
