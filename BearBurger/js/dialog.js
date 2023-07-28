console.log(document.getElementById("navHeight").clientHeight);
//  添加class 做出滑鼠hover後的特效
produceButton.addEventListener('mouseenter', function () {
  produce.classList.add('animate__fadeInDown');
})
produceButton.addEventListener('mouseleave', function () {
  produce.classList.remove('animate__fadeInDown');
})
// =============================================================== 動畫
// var newOffSetTop = document.getElementById("setofftop").offsetTop
// function dog() {
//   console.log(scrollY)
// }
// var newProduct = document.getElementById("newProduct").offsetTop
// function cat() {
//   console.log(newProduct);
//   console.log(scrollY)
// }


document.addEventListener("scroll", function () {      //事件監聽 當捲動時判斷
  if (scrollY >= 240 ) {
    document.getElementById("setofftop").style.visibility = 'visible';
    setofftop.classList.add('animate__rollIn');
    // setTimeout(() => {                                //if成立後 幾秒判斷一次
    //   setofftop.classList.remove('animate__rollIn');
    // }, 5000);
  }
})

document.addEventListener("scroll", function () {
  if (scrollY >= 480 ) {
    document.getElementById("newProduct").style.visibility = 'visible';
    newProduct.classList.add('animate__rollIn');
    // setTimeout(() => {                                //if成立後 幾秒判斷一次
    //   newProduct.classList.remove('animate__rollIn');
    // }, 5000);
  }
})
//---------------------------------------------------------------------------------------------
// 牛肉頁面的動畫
document.addEventListener("scroll", function () {     //事件監聽 當捲動時判斷
  // console.log(scrollY);
  if (scrollY >= 80) {
    document.getElementById("newProductBeef").style.visibility = 'visible';
    newProductBeef.classList.add('animate__rollIn');
    // setTimeout(() => {                                //if成立後 幾秒判斷一次
    //   newProduct.classList.remove('animate__rollIn');
    // }, 5000);
    // console.log("a")
  }
})
// 雞肉頁面的動畫
document.addEventListener("scroll", function () {     //事件監聽 當捲動時判斷
  // console.log(scrollY);
  if (scrollY >= 80) {
    document.getElementById("newProductCh").style.visibility = 'visible';
    newProductCh.classList.add('animate__rollIn');
    // setTimeout(() => {                                //if成立後 幾秒判斷一次
    //   newProduct.classList.remove('animate__rollIn');
    // }, 5000);
    // console.log("a")
  }
})
// 豬肉頁面動畫

document.addEventListener("scroll", function () {     //事件監聽 當捲動時判斷
  // console.log(scrollY);
  if (scrollY >= 80) {
    document.getElementById("newProductPork").style.visibility = 'visible';
    newProductPork.classList.add('animate__rollIn');
    // setTimeout(() => {                                //if成立後 幾秒判斷一次
    //   newProduct.classList.remove('animate__rollIn');
    // }, 5000);
    // console.log("a")
  }
})
// ======================================== 最新訊息
let btn1 = document.querySelector("#storeOpen1");
let infoModal1 = document.querySelector("#storeOpen2");
let close1 = document.querySelector("#storeOpen3");
btn1.addEventListener("click", function () {
  infoModal1.showModal();
})

close1.addEventListener("click", function () {
  infoModal1.close();
})

let btn2 = document.querySelector("#storeOpen4");
let infoModal2 = document.querySelector("#storeOpen5");
let close2 = document.querySelector("#storeOpen6");
btn2.addEventListener("click", function () {
  infoModal2.showModal();
})

close2.addEventListener("click", function () {
  infoModal2.close();
})

let btn3 = document.querySelector("#storeOpen7");
let infoModal3 = document.querySelector("#storeOpen8");
let close3 = document.querySelector("#storeOpen9");
btn3.addEventListener("click", function () {
  infoModal3.showModal();
})

close3.addEventListener("click", function () {
  infoModal3.close();
})

let btn4 = document.querySelector("#storeOpen10");
let infoModal4 = document.querySelector("#storeOpen11");
let close4 = document.querySelector("#storeOpen12");
btn4.addEventListener("click", function () {
  infoModal4.showModal();
})

close4.addEventListener("click", function () {
  infoModal4.close();
})

let btn5 = document.querySelector("#storeOpen13");
let infoModal5 = document.querySelector("#storeOpen14");
let close5 = document.querySelector("#storeOpen15");
btn5.addEventListener("click", function () {
  infoModal5.showModal();
})

close5.addEventListener("click", function () {
  infoModal5.close();
})

let btn6 = document.querySelector("#storeOpen16");
let infoModal6 = document.querySelector("#storeOpen17");
let close6 = document.querySelector("#storeOpen18");
btn6.addEventListener("click", function () {
  infoModal6.showModal();
})

close6.addEventListener("click", function () {
  infoModal6.close();
})



