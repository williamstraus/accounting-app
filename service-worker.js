if(!self.define){let e,n={};const i=(i,o)=>(i=new URL(i+".js",o).href,n[i]||new Promise((n=>{if("document"in self){const e=document.createElement("script");e.src=i,e.onload=n,document.head.appendChild(e)}else e=i,importScripts(i),n()})).then((()=>{let e=n[i];if(!e)throw new Error(`Module ${i} didn’t register its module`);return e})));self.define=(o,t)=>{const c=e||("document"in self?document.currentScript.src:"")||location.href;if(n[c])return;let s={};const r=e=>i(e,c),u={module:{uri:c},exports:s,require:r};n[c]=Promise.all(o.map((e=>u[e]||r(e)))).then((e=>(t(...e),s)))}}define(["./workbox-2d118ab0"],(function(e){"use strict";e.setCacheNameDetails({prefix:"accounting-app"}),self.addEventListener("message",(e=>{e.data&&"SKIP_WAITING"===e.data.type&&self.skipWaiting()})),e.precacheAndRoute([{url:"/accounting-app/index.html",revision:"730c81a45abffc17e480028fc8b65230"},{url:"/accounting-app/js/app.f85aeddc.js",revision:null},{url:"/accounting-app/js/chunk-vendors.84f63035.js",revision:null},{url:"/accounting-app/manifest.json",revision:"7561f02820edeb8928f726e89e44e9ef"},{url:"/accounting-app/robots.txt",revision:"b6216d61c03e6ce0c9aea6ca7808f7ca"}],{})}));
//# sourceMappingURL=service-worker.js.map