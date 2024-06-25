<script setup>
import { ref, defineProps, watch, onMounted } from 'vue'
import { setCookie, getCookie } from "../cookie";
import { useRouter } from "vue-router";
const router = useRouter()
const props = defineProps({
  cryptoList: {
    type: Array,
    required: true
  }
});
const twd = ref([]);

onMounted(() => {
  if (props.cryptoList.length > 0) {
    for (let index = 0; index < props.cryptoList.length; index++) {
      // 成交量
      props.cryptoList[index].RAW.USD.VOLUME24HOUR = (props.cryptoList[index].RAW.USD.VOLUME24HOUR / 1000000).toFixed(2)
      props.cryptoList[index].RAW.USD.VOLUME24HOUR = parseFloat(props.cryptoList[index].RAW.USD.VOLUME24HOUR).toLocaleString() + "M";
      //美元
      const price = props.cryptoList[index].RAW.USD.PRICE.toFixed(2);
      props.cryptoList[index].RAW.USD.PRICE = price;
      //台幣
      twd.value.push(parseFloat(price * 32).toLocaleString());
      props.cryptoList[index].RAW.USD.PRICE = parseFloat(price).toLocaleString();
      props.cryptoList[index].RAW.USD.CHANGEPCT24HOUR = parseFloat(props.cryptoList[index].RAW.USD.CHANGEPCT24HOUR.toFixed(2))
    }
  }

}
);
const ConfirmCurrencySelection = (index) =>{
  setCookie("imgUrl",'https://www.cryptocompare.com' + props.cryptoList[index].CoinInfo.ImageUrl,10)
  setCookie("美元當前金額",props.cryptoList[index].RAW.USD.PRICE,10)
  setCookie("台幣當前金額",twd.value[index],10);
  setCookie("漲幅",props.cryptoList[index].RAW.USD.CHANGEPCT24HOUR,10);
  goDetails();
}
const goDetails = () =>{
  router.push("/currencyDetails")
}
</script>

<template>
  <div class="bg-gray-900 text-white p-4" v-for="(crypto, index) in props.cryptoList" v-bind:key="crypto.Data" v-on:click="ConfirmCurrencySelection(index)">
    <div class="flex items-center justify-between py-2 border-b border-gray-700">
      <div class="flex items-center w-40">
        <img :src="'https://www.cryptocompare.com' + crypto.CoinInfo.ImageUrl" alt="Crypto Image" class="w-8">
        <div>
          <!-- 姓名 -->
          <div class="text-base text-left ml-3">{{ crypto.CoinInfo.Name }}</div>
          <!-- vol -->
          <div class="text-sm text-gray-400 ml-3">Vol {{ crypto.RAW.USD.VOLUME24HOUR }}</div>
        </div>
      </div>
      <div class="text-right mr-20 w-40">
        <!-- 價格 -->
        <div>{{ twd[index] }} TWD</div>
        <div>≈{{ crypto.RAW.USD.PRICE }}USD</div>
      </div>
      <div v-if="typeof crypto.RAW.USD.CHANGEPCT24HOUR !== 'undefined'" :class="crypto.RAW.USD.CHANGEPCT24HOUR >= 0 ? 'text-green-500 w-16' : 'text-red-500 w-16'">
      <!-- 24小時變動量 -->
      {{  crypto.RAW.USD.CHANGEPCT24HOUR }}%
    </div>
    </div>
  </div>
</template>

