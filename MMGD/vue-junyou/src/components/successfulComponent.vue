<script setup lang="ts">
import { ref, defineProps, watch, watchEffect, onMounted } from "vue";
import { useRouter } from "vue-router";
import { transformRoutes, transformlocation } from "@/js/dialog";
import { setCookie, getCookie } from "@/js/cookie";
const router = useRouter();
//要去的頁面
const arrivedPage = ref(getCookie("arrivedPage"));

const props = defineProps<{
  state: boolean;
  content: string;
}>();

const dialog = ref<HTMLDialogElement | null>(null);

const content = ref("");
//秒數跑完跳轉網址
const transformSec = ref(1);
const returnSec = ref(0.5);
onMounted(() => {
  if (props.state === false) {
    dialog.value?.close();
    return;
  }
  if (props.content == "1") {
    transformRoutes(returnSec, arrivedPage.value);
    content.value = `登入成功，畫面於${transformSec.value}秒後跳轉`
  } else if (props.content == "2") {
    transformRoutes(returnSec, arrivedPage.value);
    content.value = `註冊成功，畫面於${transformSec.value}秒後跳轉`
  } else if (props.content == "3") {
    transformRoutes(returnSec, arrivedPage.value);
    content.value = `驗證成功，畫面於${transformSec.value}秒後跳轉`
  } else if (props.content == "4") {
    transformRoutes(returnSec, arrivedPage.value);
    content.value = `文章發布成功，畫面於${transformSec.value}秒後跳轉`
  } else if (props.content == "5") {
    content.value = `訊息修改成功，畫面於${transformSec.value}秒後跳轉`
    transformlocation(returnSec);
  } else if (props.content == "6") {
    content.value = `訊息刪除成功，畫面於${transformSec.value}秒後跳轉`
    transformlocation(returnSec);
  } else if (props.content == "7") {
    content.value = `密碼更改成功，畫面於${transformSec.value}秒後跳轉`
    transformRoutes(returnSec, arrivedPage.value);
  } else if (props.content == "8") {
    content.value = `文章更改成功，畫面於${transformSec.value}秒後跳轉`
    transformlocation(returnSec);
  } else if (props.content == "9") {
    content.value = `文章刪除成功，畫面於${transformSec.value}秒後跳轉`
    transformRoutes(returnSec, "/shareExp");
  } else if (props.content == "10") {
    content.value = `資料更新成功，畫面於${transformSec.value}秒後跳轉`
    transformRoutes(returnSec, "/member");
  }
  dialog.value?.showModal();
});

watch(
  () => transformSec.value, (newValue, oldValue) => {
    if (transformSec.value != oldValue) {
      if (props.content == "1") {
        content.value = `登入成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "2") {
        content.value = `註冊成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "3") {
        content.value = `驗證成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "4") {
        content.value = `文章發布成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "5") {
        content.value = `訊息修改成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "6") {
        content.value = `訊息刪除成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "7") {
        content.value = `密碼更改成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "8") {
        content.value = `文章更改成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "9") {
        content.value = `文章刪除成功，畫面於${transformSec.value}秒後跳轉`
      } else if (props.content == "10") {
        content.value = `資料更新成功，畫面於${transformSec.value}秒後跳轉`
      }
    }
  }
);

</script>

<template>
  <!--  成功給true 失敗給false -->
  <dialog ref="dialog" id="dialog">
    <img src="@/img/accept.png" alt="" class="imgOn" />
    <div class="content">
      <p>{{ content }}</p>
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
  /* height: 100vh; */
  height: 940px;
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
</style>
