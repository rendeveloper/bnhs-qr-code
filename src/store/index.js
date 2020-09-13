import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    drawer: null
  },
  mutations: {
    SET_DRAWER (state, payload) {
      state.drawer = payload
    }
  },
  actions: {
    OPEN_DRAWER({ commit }, drawer){
      commit('SET_DRAWER', drawer)
    }
  },
  modules: {
  }
})
