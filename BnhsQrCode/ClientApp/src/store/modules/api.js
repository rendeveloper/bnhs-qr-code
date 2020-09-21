import axios from 'axios';

const state = {
  baseUrl: 'http://localhost:57176', /* https://bnhs-joms.cyou */
  users: []
};

const mutations = {
    GET_ALL_USERS(state, users){
      state.users = users;
    }
};

const actions = {
  getAllUsers({ commit }) {
    return axios.get(`${state.baseUrl}/api/user`).then(handleResponse).then(response => {
        commit('GET_ALL_USERS', response.data);
    }).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  getScanQRCode({ commit }, scanQRCode) {
    return axios.post(`${state.baseUrl}/api/user/scan`, scanQRCode).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  saveScanHistory({ commit }, scanHistory){
    return axios.post(`${state.baseUrl}/api/ScanHistory`, scanHistory).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  saveUser({ commit }, user){
    return axios.post(`${state.baseUrl}/api/User`, user).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  }
};

function handleResponse(response) {
  if (response.status !== 200) {
    const error = (response && response.message) || response.status;
    return Promise.reject(error);
  }
  return response;
}

export default {
  state,
  mutations,
  actions
};
