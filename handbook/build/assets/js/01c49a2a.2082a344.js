"use strict";(self.webpackChunkthingsgateway=self.webpackChunkthingsgateway||[]).push([[1461],{3905:(e,t,a)=>{a.d(t,{Zo:()=>g,kt:()=>m});var n=a(7294);function r(e,t,a){return t in e?Object.defineProperty(e,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):e[t]=a,e}function i(e,t){var a=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),a.push.apply(a,n)}return a}function o(e){for(var t=1;t<arguments.length;t++){var a=null!=arguments[t]?arguments[t]:{};t%2?i(Object(a),!0).forEach((function(t){r(e,t,a[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(a)):i(Object(a)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(a,t))}))}return e}function p(e,t){if(null==e)return{};var a,n,r=function(e,t){if(null==e)return{};var a,n,r={},i=Object.keys(e);for(n=0;n<i.length;n++)a=i[n],t.indexOf(a)>=0||(r[a]=e[a]);return r}(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(n=0;n<i.length;n++)a=i[n],t.indexOf(a)>=0||Object.prototype.propertyIsEnumerable.call(e,a)&&(r[a]=e[a])}return r}var s=n.createContext({}),c=function(e){var t=n.useContext(s),a=t;return e&&(a="function"==typeof e?e(t):o(o({},t),e)),a},g=function(e){var t=c(e.components);return n.createElement(s.Provider,{value:t},e.children)},l={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},d=n.forwardRef((function(e,t){var a=e.components,r=e.mdxType,i=e.originalType,s=e.parentName,g=p(e,["components","mdxType","originalType","parentName"]),d=c(a),m=r,u=d["".concat(s,".").concat(m)]||d[m]||l[m]||i;return a?n.createElement(u,o(o({ref:t},g),{},{components:a})):n.createElement(u,o({ref:t},g))}));function m(e,t){var a=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var i=a.length,o=new Array(i);o[0]=d;var p={};for(var s in t)hasOwnProperty.call(t,s)&&(p[s]=t[s]);p.originalType=e,p.mdxType="string"==typeof e?e:r,o[1]=p;for(var c=2;c<i;c++)o[c]=a[c];return n.createElement.apply(null,o)}return n.createElement.apply(null,a)}d.displayName="MDXCreateElement"},2821:(e,t,a)=>{a.r(t),a.d(t,{assets:()=>s,contentTitle:()=>o,default:()=>l,frontMatter:()=>i,metadata:()=>p,toc:()=>c});var n=a(7462),r=(a(7294),a(3905));const i={id:"opcuademo",title:"ThingsGateway(\u4e09)\u91c7\u96c6OPCUA\u534f\u8bae"},o=void 0,p={unversionedId:"opcuademo/opcuademo",id:"opcuademo/opcuademo",title:"ThingsGateway(\u4e09)\u91c7\u96c6OPCUA\u534f\u8bae",description:"Gitee\u6e90\u4ee3\u7801\u4ed3\u5e93\uff1ahttps://gitee.com/diego2098/ThingsGateway",source:"@site/docs/opcuademo/opcuademo.mdx",sourceDirName:"opcuademo",slug:"/opcuademo/",permalink:"/thingsgateway/docs/opcuademo/",draft:!1,editUrl:"https://gitee.com/dotnetchina/ThingsGateway/tree/master/handbook/docs/opcuademo/opcuademo.mdx",tags:[],version:"current",frontMatter:{id:"opcuademo",title:"ThingsGateway(\u4e09)\u91c7\u96c6OPCUA\u534f\u8bae"},sidebar:"docs",previous:{title:"ThingsGateway(\u4e8c)\u91c7\u96c6OPCDA\u534f\u8bae",permalink:"/thingsgateway/docs/opcdademo/"},next:{title:"ThingsGateway(\u4e94)\u5bf9\u4e8eModbusServer\u63d2\u4ef6\u7684\u5b9e\u6218",permalink:"/thingsgateway/docs/modbusserverdemo/"}},s={},c=[{value:"\u4e00\u3001\u524d\u8a00",id:"\u4e00\u524d\u8a00",level:2},{value:"\u4e8c\u3001\u51c6\u5907\u6d4b\u8bd5\u73af\u5883",id:"\u4e8c\u51c6\u5907\u6d4b\u8bd5\u73af\u5883",level:2},{value:"\u4e09\u3001\u901a\u8baf\u6d4b\u8bd5",id:"\u4e09\u901a\u8baf\u6d4b\u8bd5",level:2},{value:"\u56db\u3001\u8fdb\u9636\u73a9\u6cd5",id:"\u56db\u8fdb\u9636\u73a9\u6cd5",level:2},{value:"1\u3001OPC\u70b9\u4f4d\u6d4f\u89c8\u5668/\u5bfc\u51fa",id:"1opc\u70b9\u4f4d\u6d4f\u89c8\u5668\u5bfc\u51fa",level:3}],g={toc:c};function l(e){let{components:t,...i}=e;return(0,r.kt)("wrapper",(0,n.Z)({},g,i,{components:t,mdxType:"MDXLayout"}),(0,r.kt)("admonition",{title:"\u6587\u7ae0\u5730\u5740",type:"tip"},(0,r.kt)("p",{parentName:"admonition"},"Gitee\u6e90\u4ee3\u7801\u4ed3\u5e93\uff1a",(0,r.kt)("a",{parentName:"p",href:"https://gitee.com/diego2098/ThingsGateway"},"https://gitee.com/diego2098/ThingsGateway")),(0,r.kt)("p",{parentName:"admonition"},"Github\u6e90\u4ee3\u7801\u4ed3\u5e93\uff1a",(0,r.kt)("a",{parentName:"p",href:"https://github.com/kimdiego2098/ThingsGateway"},"https://github.com/kimdiego2098/ThingsGateway")),(0,r.kt)("p",{parentName:"admonition"},"\u4f7f\u7528\u6587\u6863\uff1a",(0,r.kt)("a",{parentName:"p",href:"https://diego2098.gitee.io/thingsgateway-docs/"},"https://diego2098.gitee.io/thingsgateway-docs/"))),(0,r.kt)("h2",{id:"\u4e00\u524d\u8a00"},"\u4e00\u3001\u524d\u8a00"),(0,r.kt)("div",{align:"center"},(0,r.kt)("p",null,(0,r.kt)("a",{parentName:"p",href:"https://www.nuget.org/packages/kimdiego/"},(0,r.kt)("img",{parentName:"a",src:"https://img.shields.io/nuget/v/ThingsGateway.Foundation.svg?label=ThingsGateway.Foundation",alt:"NuGet(ThingsGateway)"})),"\n",(0,r.kt)("a",{parentName:"p",href:"https://www.apache.org/licenses/LICENSE-2.0.html"},(0,r.kt)("img",{parentName:"a",src:"https://img.shields.io/badge/license-Apache%202-4EB1BA.svg",alt:"License"})),"\n",(0,r.kt)("a",{parentName:"p",href:"https://gitee.com/diego2098/ThingsGateway/stargazers"},(0,r.kt)("img",{parentName:"a",src:"https://gitee.com/diego2098/ThingsGateway/badge/star.svg",alt:"star"})),"\n",(0,r.kt)("a",{parentName:"p",href:"https://gitee.com/diego2098/ThingsGateway/members"},(0,r.kt)("img",{parentName:"a",src:"https://gitee.com/diego2098/ThingsGateway/badge/fork.svg",alt:"star"})),"\n",(0,r.kt)("img",{parentName:"p",src:"https://img.shields.io/badge/QQ%E7%BE%A4-605534569-blue",alt:"star"}))),(0,r.kt)("p",null,(0,r.kt)("strong",{parentName:"p"},"ThingsGateway"),"\u662f\u56fd\u5185\u65b0\u751f\u5f00\u6e90\u9879\u76ee,\u5f52\u5c5e\u5de5\u4e1a\u6570\u636e\u91c7\u96c6\u7f51\u5173,\u7ecf\u8fc7\u8fd1\u56db\u4e2a\u6708\u7684\u6d17\u793c,\u5df2\u7ecf\u8d8b\u4e8e\u7a33\u5b9a"),(0,r.kt)("p",null,"\u8fd9\u7bc7\u6587\u7ae0\u5c06\u5b9e\u6d4b",(0,r.kt)("strong",{parentName:"p"},"ThingsGateway"),"\u91c7\u96c6OPCUA\u534f\u8bae\u8bbe\u5907,\u901a\u8fc7\u52a8\u56fe\u6f14\u793a,\u65b9\u4fbf\u7406\u89e3"),(0,r.kt)("h2",{id:"\u4e8c\u51c6\u5907\u6d4b\u8bd5\u73af\u5883"},"\u4e8c\u3001\u51c6\u5907\u6d4b\u8bd5\u73af\u5883"),(0,r.kt)("p",null,"1\u3001",(0,r.kt)("a",{parentName:"p",href:"https://gitee.com/diego2098/ThingsGateway"},"ThingsGateway")),(0,r.kt)("p",null,"2\u3001KepServer"),(0,r.kt)("p",null,"3\u3001Uaexpert"),(0,r.kt)("p",null,"\u8fd0\u884cThingsGateway\u7684\u65b9\u6cd5\u8bf7\u67e5\u770b",(0,r.kt)("a",{parentName:"p",href:"https://diego2098.gitee.io/thingsgateway-docs/docs/startguide"},"\u6e90\u6587\u6863")),(0,r.kt)("h2",{id:"\u4e09\u901a\u8baf\u6d4b\u8bd5"},"\u4e09\u3001\u901a\u8baf\u6d4b\u8bd5"),(0,r.kt)("p",null,(0,r.kt)("img",{src:a(8374).Z,width:"2549",height:"1440"})),(0,r.kt)("p",null,"\u6709\u4e0a\u4e00\u7bc7\u7684\u57fa\u7840\uff0c\u8fd9\u91cc\u5feb\u901f\u8fc7\u4e00\u904d\uff0c\u65b0\u5efa\u8bbe\u5907-\u65b0\u5efa\u53d8\u91cf-\u91cd\u542f\u8fd0\u884c\u6001"),(0,r.kt)("h2",{id:"\u56db\u8fdb\u9636\u73a9\u6cd5"},"\u56db\u3001\u8fdb\u9636\u73a9\u6cd5"),(0,r.kt)("admonition",{title:"\u63d0\u793a",type:"tip"},(0,r.kt)("p",{parentName:"admonition"},"\u5173\u4e8e\u6570\u636e\u8f6c\u6362/\u5197\u4f59/\u8fde\u8bfb\u7b49\u7b49\u4e0e\u4e0a\u4e00\u7bc7\u662f\u901a\u7528\u7684\uff0c\u8fd9\u91cc\u4f1a\u4ecb\u7ecd\u7279\u6709\u7684\u529f\u80fd")),(0,r.kt)("h3",{id:"1opc\u70b9\u4f4d\u6d4f\u89c8\u5668\u5bfc\u51fa"},"1\u3001OPC\u70b9\u4f4d\u6d4f\u89c8\u5668/\u5bfc\u51fa"),(0,r.kt)("p",null,(0,r.kt)("img",{src:a(8321).Z,width:"2549",height:"1440"})),(0,r.kt)("p",null,"\u901a\u8fc7\u9a71\u52a8\u8c03\u8bd5\u7684\u5bfc\u5165\u53d8\u91cf\u6309\u94ae\uff0c\u53ef\u4ee5\u5bfc\u51fa\u6240\u9009\u7684OPCITEM"))}l.isMDXComponent=!0},8374:(e,t,a)=>{a.d(t,{Z:()=>n});const n=a.p+"assets/images/opcuademo-1-7159d74dc48212dea3d48f01c7792f1a.gif"},8321:(e,t,a)=>{a.d(t,{Z:()=>n});const n=a.p+"assets/images/opcuademo-2-39ea7e6751fcf0230486037bca900d28.gif"}}]);