<template>
  <v-container>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <div class="d-flex align-center"
      @click.stop="isPopUp()">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="../../assets/bnhs-logo.png"
          transition="scale-transition"
          width="75"
        />
        <h2>Bato National High School</h2>
      </div>

      <v-spacer></v-spacer>

      <v-btn
        text
      >
        <span class="mr-2">Logout</span>
        <v-icon>mdi-open-in-new</v-icon>
      </v-btn>
    </v-app-bar>
  </v-container>
</template>

<script>
  import { mapState, mapActions } from 'vuex'

  export default {
    name: 'NavBar',
    computed: {
      ...mapState({
        drawer: state => state.drawer
      })
    }, 
    methods: {
      ...mapActions({
        newTestt: 'navMenuDrawer'
      }),
      isPopUp(){
        let menuDrawer = this.drawer
        this.newTestt(menuDrawer = !menuDrawer)
      }
    }
  }
</script>
