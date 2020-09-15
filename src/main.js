import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import initialize from './initialize'

import VueAwesome from 'vue-awesome'
import VueImg from 'v-img'
import Multiselect from 'vue-multiselect'
import VuejQuery from 'vue-jquery'
import jQuery from 'jquery'
import Element from 'element-ui'

import './assets/scss/styles.scss'
import 'element-ui/lib/theme-chalk/index.css'
import 'font-awesome/css/font-awesome.min.css'
import vuetify from './plugins/vuetify';

import VueQrcodeReader from "vue-qrcode-reader";
import VueQrcodeGenerator from '@chenfengyuan/vue-qrcode';

global.jQuery = jQuery
global.$ = jQuery

initialize()

Vue.config.productionTip = false

Vue.use(VueImg)
Vue.use(VuejQuery)
Vue.use(VueQrcodeReader);
Vue.use(Element)
Vue.component(VueQrcodeGenerator.name, VueQrcodeGenerator);
Vue.component('icon', VueAwesome)
Vue.component('multiselect', Multiselect)

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
