import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import initialize from './initialize'

import VueAwesome from 'vue-awesome'
import VueImg from 'v-img'
import Element from 'element-ui'
import Multiselect from 'vue-multiselect'
import VuejQuery from 'vue-jquery'
import jQuery from 'jquery'

import 'font-awesome/css/font-awesome.min.css'
import vuetify from './plugins/vuetify';

global.jQuery = jQuery
global.$ = jQuery

initialize()

Vue.config.productionTip = false

Vue.use(VueImg)
Vue.use(VuejQuery)
Vue.component('icon', VueAwesome)
Vue.component('multiselect', Multiselect)

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
