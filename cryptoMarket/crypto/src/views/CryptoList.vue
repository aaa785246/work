<template>
  <panel v-if="acceptData === true" :cryptoList="cryptoList.Data"/>
</template>

<script  setup>
import axios from 'axios';
import { ref ,onMounted} from 'vue'
import panel from '../components/panel.vue'
const cryptoList = ref([]);
// 驗證接收到api資料才開啟component
const acceptData = ref(false);
const callCryptoData = async () => {
  const api = `https://min-api.cryptocompare.com/data/top/totalvolfull?limit=10&tsym=USD`;
  await axios
    .get(api, {
      limit: 10,
      tsym: "USD"
    })
    .then((response) => {
      if (response.data) {
        cryptoList.value = response.data
        console.log(cryptoList.value.Data);
        acceptData.value = true;
      }
    })
    .catch((ex) => {
      console.log("error msg:" + ex)
    });
};
onMounted(callCryptoData);

</script>