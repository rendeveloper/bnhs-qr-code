import axios from 'axios';

const state = {
  baseUrl: 'http://localhost:57176', /* https://bnhs-joms.cyou */
  users: [],
  isLoggedIn: isUserLoggedIn()
};

const mutations = {
    GET_ALL_USERS(state, users){
      state.users = users;
    },
    USER_LOG_IN(state, user){
      debugger
      state.isLoggedIn = user
    }
};

const actions = {
  login({ commit }, teacherId){
    debugger
    return axios.post(`${state.baseUrl}/api/user/authenticate`, teacherId).then(handleResponse)
        .then(user => {
          debugger
            // login successful if there's a user details in the response
            if (user.data.name) {
                // store user details in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user.data));
                commit('USER_LOG_IN', user.data.active);
            }
              debugger
            return user;
        });/* .catch(error => {
          throw new Error(`API ${error}`);
        }); */
  },
  logout({ commit }) {
    // remove user from local storage to log user out
    debugger
    commit('USER_LOG_IN', false);
    localStorage.removeItem('user');
  },
  getUser({ commit }, id){
    return axios.get(`${state.baseUrl}/api/user/${id}`).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  getAllUsers({ commit }) {
    return axios.get(`${state.baseUrl}/api/user`).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  /* getAllUsers({ commit }) {
    return axios.get(`${state.baseUrl}/api/user`).then(handleResponse).then(response => {
        commit('GET_ALL_USERS', response.data);
    }).catch(error => {
      throw new Error(`API ${error}`);
    });
  }, */
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
  },
  updateUser({ commit }, user){
    return axios.put(`${state.baseUrl}/api/User/${user.id}`, user).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  deleteUser({ commit }, id){
    return axios.delete(`${state.baseUrl}/api/User/${id}`).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  saveImageDetails({ commit }, image){
    return axios.post(`${state.baseUrl}/api/UserImageFile`, image).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  updateImageDetails({ commit }, image){
    return axios.put(`${state.baseUrl}/api/UserImageFile/${image.id}`, image).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  },
  getAllTimeUser({ commit }, id){
    return axios.get(`${state.baseUrl}/api/ScanHistory/AllTime/${id}`).then(handleResponse).catch(error => {
      throw new Error(`API ${error}`);
    });
  }
};

function isUserLoggedIn(){
  let user = JSON.parse(localStorage.getItem('user'));
  debugger
  if(user !== null && user.active){
    return true
  }else{
    return false
  }
}

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
