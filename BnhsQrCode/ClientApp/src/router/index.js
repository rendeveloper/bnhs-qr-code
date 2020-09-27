import Vue from 'vue'
import VueRouter from 'vue-router'
import LandingPage from '@/components/LandingPage'
import UserCreate from '@/components/user/UserCreate'
import AllUsers from '@/components/user/AllUsers'


Vue.use(VueRouter)

  const routes = [
  /* {
    path: '/',
    redirect: '/dashboard'
  }, */
  {
    path: '/',
    name: 'Dashboard',
    component: LandingPage
  },
  // Pages
  {
    name: 'User Create',
    path: '/user/create',
    component: UserCreate,
  },
  {
    name: 'User Edit',
    path: '/user/edit/:id',
    component: UserCreate
  },
  {
    name: 'All Users',
    path: '/user/all',
    component: AllUsers,
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
