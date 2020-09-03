import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    drawer: false
  },
  mutations: {
    navMenuDrawer(state, drawer){
      state.drawer = drawer
    }
  },
  actions: {
    navMenuDrawer({ commit }, drawer){
      commit('navMenuDrawer', drawer)
    }
  },
  modules: {
  }
})
