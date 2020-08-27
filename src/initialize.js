import axios from 'axios'

export default function () {
  axios.defaults.withCredentials = true
  axios.defaults.headers.common.Accept = 'application/json'
}