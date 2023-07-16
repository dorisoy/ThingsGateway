"use strict";(self.webpackChunkthingsgateway=self.webpackChunkthingsgateway||[]).push([[7929],{3905:(t,e,n)=>{n.d(e,{Zo:()=>o,kt:()=>c});var a=n(7294);function r(t,e,n){return e in t?Object.defineProperty(t,e,{value:n,enumerable:!0,configurable:!0,writable:!0}):t[e]=n,t}function l(t,e){var n=Object.keys(t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(t);e&&(a=a.filter((function(e){return Object.getOwnPropertyDescriptor(t,e).enumerable}))),n.push.apply(n,a)}return n}function i(t){for(var e=1;e<arguments.length;e++){var n=null!=arguments[e]?arguments[e]:{};e%2?l(Object(n),!0).forEach((function(e){r(t,e,n[e])})):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(n)):l(Object(n)).forEach((function(e){Object.defineProperty(t,e,Object.getOwnPropertyDescriptor(n,e))}))}return t}function p(t,e){if(null==t)return{};var n,a,r=function(t,e){if(null==t)return{};var n,a,r={},l=Object.keys(t);for(a=0;a<l.length;a++)n=l[a],e.indexOf(n)>=0||(r[n]=t[n]);return r}(t,e);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(t);for(a=0;a<l.length;a++)n=l[a],e.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(t,n)&&(r[n]=t[n])}return r}var d=a.createContext({}),u=function(t){var e=a.useContext(d),n=e;return t&&(n="function"==typeof t?t(e):i(i({},e),t)),n},o=function(t){var e=u(t.components);return a.createElement(d.Provider,{value:e},t.children)},m={inlineCode:"code",wrapper:function(t){var e=t.children;return a.createElement(a.Fragment,{},e)}},g=a.forwardRef((function(t,e){var n=t.components,r=t.mdxType,l=t.originalType,d=t.parentName,o=p(t,["components","mdxType","originalType","parentName"]),g=u(n),c=r,s=g["".concat(d,".").concat(c)]||g[c]||m[c]||l;return n?a.createElement(s,i(i({ref:e},o),{},{components:n})):a.createElement(s,i({ref:e},o))}));function c(t,e){var n=arguments,r=e&&e.mdxType;if("string"==typeof t||r){var l=n.length,i=new Array(l);i[0]=g;var p={};for(var d in e)hasOwnProperty.call(e,d)&&(p[d]=e[d]);p.originalType=t,p.mdxType="string"==typeof t?t:r,i[1]=p;for(var u=2;u<l;u++)i[u]=n[u];return a.createElement.apply(null,i)}return a.createElement.apply(null,n)}g.displayName="MDXCreateElement"},5784:(t,e,n)=>{n.r(e),n.d(e,{assets:()=>d,contentTitle:()=>i,default:()=>m,frontMatter:()=>l,metadata:()=>p,toc:()=>u});var a=n(7462),r=(n(7294),n(3905));const l={id:"pluginrabbitmqclient",title:"RabbitMQClient"},i=void 0,p={unversionedId:"pluginrabbitmqclient",id:"pluginrabbitmqclient",title:"RabbitMQClient",description:"\u4e00\u3001\u8bbe\u5907\u6269\u5c55\u5c5e\u6027",source:"@site/docs/pluginrabbitmqclient.mdx",sourceDirName:".",slug:"/pluginrabbitmqclient",permalink:"/thingsgateway/docs/pluginrabbitmqclient",draft:!1,editUrl:"https://gitee.com/diego2098/ThingsGateway/tree/master/handbook/docs/pluginrabbitmqclient.mdx",tags:[],version:"current",lastUpdatedBy:"2248356998 qq.com",lastUpdatedAt:1689499706,formattedLastUpdatedAt:"Jul 16, 2023",frontMatter:{id:"pluginrabbitmqclient",title:"RabbitMQClient"},sidebar:"docs",previous:{title:"MqttClient",permalink:"/thingsgateway/docs/pluginmqttclient"},next:{title:"Kafka\u6d88\u606f\u751f\u4ea7",permalink:"/thingsgateway/docs/pluginkafka"}},d={},u=[{value:"\u4e00\u3001\u8bbe\u5907\u6269\u5c55\u5c5e\u6027",id:"\u4e00\u8bbe\u5907\u6269\u5c55\u5c5e\u6027",level:2},{value:"\u4e8c\u3001\u53d8\u91cf\u914d\u7f6e",id:"\u4e8c\u53d8\u91cf\u914d\u7f6e",level:2},{value:"\u542f\u7528",id:"\u542f\u7528",level:3},{value:"\u4e09\u3001\u5b9e\u4f53\u811a\u672c",id:"\u4e09\u5b9e\u4f53\u811a\u672c",level:2}],o={toc:u};function m(t){let{components:e,...l}=t;return(0,r.kt)("wrapper",(0,a.Z)({},o,l,{components:e,mdxType:"MDXLayout"}),(0,r.kt)("h2",{id:"\u4e00\u8bbe\u5907\u6269\u5c55\u5c5e\u6027"},"\u4e00\u3001\u8bbe\u5907\u6269\u5c55\u5c5e\u6027"),(0,r.kt)("img",{src:n(6327).Z,width:"300"}),(0,r.kt)("table",null,(0,r.kt)("thead",{parentName:"table"},(0,r.kt)("tr",{parentName:"thead"},(0,r.kt)("th",{parentName:"tr",align:null},"\u5c5e\u6027"),(0,r.kt)("th",{parentName:"tr",align:null},"\u8bf4\u660e"),(0,r.kt)("th",{parentName:"tr",align:null},"\u9ed8\u8ba4\u503c/\u5907\u6ce8"))),(0,r.kt)("tbody",{parentName:"table"},(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"IP"),(0,r.kt)("td",{parentName:"tr",align:null},"\u8fde\u63a5IP"),(0,r.kt)("td",{parentName:"tr",align:null},"127.0.0.1")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u7aef\u53e3"),(0,r.kt)("td",{parentName:"tr",align:null},"\u8fde\u63a5\u7aef\u53e3"),(0,r.kt)("td",{parentName:"tr",align:null},"5672")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u8d26\u53f7"),(0,r.kt)("td",{parentName:"tr",align:null},"\u767b\u5f55\u8d26\u53f7"),(0,r.kt)("td",{parentName:"tr",align:null},"guest")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u5bc6\u7801"),(0,r.kt)("td",{parentName:"tr",align:null},"\u767b\u5f55\u5bc6\u7801"),(0,r.kt)("td",{parentName:"tr",align:null},"guest")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u662f\u5426\u53d1\u5e03List"),(0,r.kt)("td",{parentName:"tr",align:null}),(0,r.kt)("td",{parentName:"tr",align:null},"False")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u662f\u5426\u58f0\u660e\u961f\u5217"),(0,r.kt)("td",{parentName:"tr",align:null}),(0,r.kt)("td",{parentName:"tr",align:null},"False")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u865a\u62dfHost"),(0,r.kt)("td",{parentName:"tr",align:null}),(0,r.kt)("td",{parentName:"tr",align:null},"/")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u8def\u7531\u540d\u79f0"),(0,r.kt)("td",{parentName:"tr",align:null}),(0,r.kt)("td",{parentName:"tr",align:null},"TG")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u53d8\u91cf\u961f\u5217\u540d\u79f0"),(0,r.kt)("td",{parentName:"tr",align:null}),(0,r.kt)("td",{parentName:"tr",align:null},"ThingsGateway/Variable")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u8bbe\u5907\u961f\u5217\u540d\u79f0"),(0,r.kt)("td",{parentName:"tr",align:null}),(0,r.kt)("td",{parentName:"tr",align:null},"ThingsGateway/Device")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u7ebf\u7a0b\u5faa\u73af\u95f4\u9694"),(0,r.kt)("td",{parentName:"tr",align:null},"\u4e0a\u4f20\u7ebf\u7a0b\u7684\u5faa\u73af\u95f4\u9694,\u4e00\u822c\u4e0d\u9700\u8981\u66f4\u6539(ms)"),(0,r.kt)("td",{parentName:"tr",align:null},"1000")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u7f13\u5b58\u6700\u5927\u6761\u6570"),(0,r.kt)("td",{parentName:"tr",align:null},"\u79bb\u7ebf\u7f13\u5b58\u65f6\u7684\u6700\u5927\u6761\u6570,\u8fd9\u91cc\u6307\u7684\u662f\u53d1\u5e03\u4e0d\u6210\u529f\u65f6\u7684\u7f13\u5b58\u6b21\u6570"),(0,r.kt)("td",{parentName:"tr",align:null},"2000")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u5217\u8868\u5206\u5272\u5927\u5c0f"),(0,r.kt)("td",{parentName:"tr",align:null},"\u53d1\u5e03\u7684\u5b9e\u4f53\u5217\u8868\u6309\u5927\u5c0f\u5206\u5272"),(0,r.kt)("td",{parentName:"tr",align:null},"1000")))),(0,r.kt)("h2",{id:"\u4e8c\u53d8\u91cf\u914d\u7f6e"},"\u4e8c\u3001\u53d8\u91cf\u914d\u7f6e"),(0,r.kt)("img",{src:n(6186).Z,width:"300"}),(0,r.kt)("h3",{id:"\u542f\u7528"},"\u542f\u7528"),(0,r.kt)("p",null,"\u542f\u7528\u53d8\u91cf\u540e\u624d\u80fd\u8fdb\u884c\u4e0a\u4f20"),(0,r.kt)("h2",{id:"\u4e09\u5b9e\u4f53\u811a\u672c"},"\u4e09\u3001\u5b9e\u4f53\u811a\u672c"),(0,r.kt)("p",null," \u79fb\u81f3 ",(0,r.kt)("a",{parentName:"p",href:"/thingsgateway/docs/pluginmqttserver#%E4%B8%89%E5%AE%9E%E4%BD%93%E8%84%9A%E6%9C%AC"},"\u5b9e\u4f53\u811a\u672c")," \u67e5\u770b"))}m.isMDXComponent=!0},6327:(t,e,n)=>{n.d(e,{Z:()=>a});const a=n.p+"assets/images/pluginrabbitmqclient-1-961aae52adf572173b9613bd87ea142a.png"},6186:(t,e,n)=>{n.d(e,{Z:()=>a});const a=n.p+"assets/images/pluginrabbitmqclient-2-7ede66618ad2cfd47fe2826bfe75724a.png"}}]);