<template>
  <v-container>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <div class="d-flex align-center"
      @click="isPopUp()">
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
        <span class="mr-2" style="letter-spacing: 2px;"><strong>Logout</strong></span>
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
        openDrawer: 'OPEN_DRAWER'
      }),
      isPopUp(){
        this.openDrawer(!this.drawer)
      }
    }
  }
</script>
