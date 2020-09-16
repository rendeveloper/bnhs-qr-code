<template>
  <v-app>
    <nav-bar/>
    <nav-menu/>
    <v-main style="padding: 0px 0px 0px;">
      <router-view :key="$route.path"/>
    </v-main>
  </v-app>
</template>

<script>
import NavBar from './components/nav/NavBar';
import NavMenu from './components/nav/NavMenu'

export default {
  name: 'App',

  components: {
    NavBar,
    NavMenu
  },

  data: () => ({
    //
  }),
};
</script>
