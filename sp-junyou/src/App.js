// import logo from './logo.svg';
//import './App.css';
import * as React from "react";
import colors from "./theme/colors";
import "./theme/homePage.css";
// import {
//   AlertDialog,
//   AlertDialogBody,
//   AlertDialogFooter,
//   AlertDialogHeader,
//   AlertDialogContent,
//   AlertDialogOverlay,
//   AlertDialogCloseButton,
//   useDisclosure,
//   Button,
//   ChakraProvider,
//   styled,
//   Box
// } from "@chakra-ui/react";

function App() {
  return (
    <div>
      <div className="header">
        <div id="header1">面面相覷</div>
        <div>
        <img rc="https://raw.githubusercontent.com/aaa785246/work/main/sp-junyou/src/theme/img/menu.png" alt="menu"></img>
        <div id="header2">面試經驗</div>
        <div id="header3">討論區</div>
        <div id="header4">登入</div>
        </div>
        
      </div>
      <div className="bgsup">
        <div className="title">123</div>    
      </div>
    </div>
  );
}

// function LoginAlert({ state }) {
//   const { isOpen, onOpen, onClose } = useDisclosure();
//   const cancelRef = React.useRef();
//   var dialogWork = "";
//   var dialogColorSelect = "";
//   var dialogHover = "";

//   if (state == 1) {
//     dialogWork = "登入";
//     dialogColorSelect = "colors.gray.sup";
//     dialogHover = "colors.gray.900";
//     return (
//       <>
//         <Button
//           bg={dialogColorSelect}
//           onClick={onOpen}
//           _hover={{ bg: dialogHover }}
//         >
//           {dialogWork}
//         </Button>

//         <AlertDialog
//           isOpen={isOpen}
//           leastDestructiveRef={cancelRef}
//           onClose={onClose}
//         >
//           <AlertDialogOverlay>
//             <AlertDialogContent>
//               <AlertDialogHeader fontSize="20px" fontWeight="bold">
//                 確定要{dialogWork}嗎?
//               </AlertDialogHeader>

//               <AlertDialogBody>{dialogWork}後將無法復原</AlertDialogBody>

//               <AlertDialogFooter>
//                 <Button ref={cancelRef} onClick={onClose}>
//                   取消
//                 </Button>
//                 <Button
//                   bg={dialogColorSelect}
//                   _hover={{ bg: dialogHover }}
//                   onClick={onClose}
//                   ml={3}
//                 >
//                   {dialogWork}
//                 </Button>
//               </AlertDialogFooter>
//             </AlertDialogContent>
//           </AlertDialogOverlay>
//         </AlertDialog>
//       </>
//     );
//   }
// }

export default App;
