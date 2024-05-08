<script setup lang="ts">
import "@/assets/post.css"
import { ref, watch } from "vue";
import { useRouter } from "vue-router";
const router = useRouter();
const goHomePage = () => {
router.push("/")
}
const titleInput=ref("")
</script>
<template>
    <img src="@/img/back.png" class="post-back" title="back" alt="this is back" @click="goHomePage"/>
    <div class="post-title">標題</div>
    <textarea name="title" id="" cols="40" rows="5" class="post-input" placeholder="請輸入標題">{{ titleInput }}</textarea>
</template>