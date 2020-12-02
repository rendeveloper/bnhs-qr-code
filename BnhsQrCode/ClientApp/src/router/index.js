import Vue from 'vue'
import VueRouter from 'vue-router'
import LandingPage from '@/components/LandingPage'
import UserCreate from '@/components/user/UserCreate'
import AllUsers from '@/components/user/AllUsers'
import LoginPage from '@/components/login/LoginPage'
import Overview from '@/components/overview/Overview'


Vue.use(VueRouter)

  const routes = [
  /* {
    path: '/',
    redirect: '/overview'
  }, */
  { 
    path: '/login',
    component: LoginPage
  },
  {
    path: '/overview',
    name: 'Overview',
    component: Overview
  },
  
  {
    path: '/',
    name: 'Dashboard',
    component: LandingPage,
    meta: { authorize: true }
  },
  // Pages
  {
    name: 'User Create',
    path: '/user/create',
    component: UserCreate,
    meta: { authorize: true }
  },
  {
    name: 'User Edit',
    path: '/user/edit/:id',
    component: UserCreate,
    meta: { authorize: true }
  },
  {
    name: 'All Users',
    path: '/user/all',
    component: AllUsers,
    meta: { authorize: true }
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

router.beforeEach((to, from, next) => {
  const { authorize } = to.meta;
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');
  const user = JSON.parse(loggedIn);

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  if(authorize && !user.admin){
    return next({ path: '/' });
  }

  next();
})

export default router
