<template>
  <v-app>
    <nav-bar/>
    <v-main>
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
