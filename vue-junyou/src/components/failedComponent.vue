<script setup lang="ts">
import { ref, defineProps, watch, watchEffect, onMounted, computed } from "vue";
import { useRouter } from "vue-router";
const router = useRouter();

const props = defineProps<{
  state: boolean;
  content: string;
}>();

const dialog = ref<HTMLDialogElement | null>(null);
const content = ref("");
const deleteImg = ref(false);
//修改父層狀態
const emitClose = defineEmits(["closeDialog","callApi"]);
//點擊確定後跳轉網址
const deleteModel = () => {
  if (deleteImg.value == true ) {
    emitClose("callApi");
  }else{
    emitClose("closeDialog");
  }
};
//關閉
const closeModel = () => {
  emitClose("closeDialog");
};


watch(
  () => props.state,
  () => {
    if (props.content == "1") {
      content.value = `登入失敗，帳號或密碼錯誤。`
    } else if (props.content == "2") {
      content.value = `註冊失敗，驗證碼錯誤`
    }else if (props.content == "3") {
      content.value = `驗證失敗，驗證碼錯誤`
    }else if (props.content == "4"){
      content.value = `確定要刪除嗎?`
      deleteImg.value = true;
    }else if (props.content == "5"){
      content.value = `確定要刪除嗎?`
      deleteImg.value = true;
    }
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
      <img src="@/img/warning.png" alt="" class="imgOn"  v-if="deleteImg == false"/>
      <img src="@/img/garbage.png" alt="" class="imgOn"  v-else/>
      <div class="content">
        <p>{{ content }}</p>
      </div>
      <div class="content2">
        <button :class="deleteImg?'content-btnDelete':'content-btn'" @click="deleteModel">確定</button>
        <button class="content-btnCancel" v-if="deleteImg== true" @click="closeModel">取消</button>
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

.content>p {
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

.content-btnDelete {
  background-color: #eadfdf;
  border: none;
  border-radius: 5px;
  padding: 10px 30px;
}
.content-btnCancel {
  background-color: #eadfdf;
  border: none;
  border-radius: 5px;
  padding: 10px 30px;
  margin-left: 20px;
}
</style>
