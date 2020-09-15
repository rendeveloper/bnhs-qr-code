import Vue from 'vue'
import VueRouter from 'vue-router'
import LandingPage from '@/components/LandingPage'
import UserCreate from '@/components/user/UserCreate'
import AllProfile from '@/components/user/AllProfile'


Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'Home',
    component: LandingPage
  },
  // Pages
  {
    name: 'User Create',
    path: '/create',
    component: UserCreate,
  },
  {
    name: 'All Profile',
    path: '/all',
    component: AllProfile,
  }
  //,
  //{
    //path: '/about',
    //name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    //component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  //}
]

const router = new VueRouter({
  routes
})

export default router
