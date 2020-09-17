const state = {
  isOpen: null
};
  
const mutations = {
  SET_DRAWER (state, payload) {
    state.isOpen = payload
  }
};
  
const actions = {
  OPEN_DRAWER({ commit }, isOpen){
    // do something async
    commit('SET_DRAWER', isOpen)
  }
};
  
 /*  const getters = {
    search: state => state.search
  }; */
  
 export default {
   state,
   mutations,
   actions
 };
  