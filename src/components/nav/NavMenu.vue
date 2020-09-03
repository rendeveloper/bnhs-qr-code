<template>
  <v-sheet
    height="100%"
    class="overflow-hidden"
    style="position: relative;"
  >
    <v-container class="fill-height">
      <v-row
        align="center"
        justify="center"
      >
        <v-btn
          color="pink"
          dark
          @click.stop="menuDrawer = !menuDrawer"
        >
          Toggle
        </v-btn>
      </v-row>
    </v-container>

    <v-navigation-drawer
      v-model="menuDrawer"
      absolute
      temporary
    >
      <v-list-item>
        <v-list-item-avatar>
          <v-img src="../../assets/joemy.png"></v-img>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>Admin Joemy</v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>

      <v-list dense>

        <v-list-item
          link
          :to="'/'"
        >
          <v-list-item-icon>
            <v-icon>mdi-home</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item
          link
        >
          <v-list-item-icon>
            <v-icon>mdi-account</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Add New</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </v-sheet>
</template>

<script>
  import { mapState, mapActions } from 'vuex'

  export default {
    name: 'NavMenu',
    data () {
      return {
        menuDrawer: false,
        items: [
          { title: 'Home', icon: '' },
          { title: 'Dashboard', icon: '' },
        ],
      }
    },
    watch: {
      drawer(newVal){
        this.menuDrawer = newVal
      }
    },
    computed: {
      ...mapState({
        drawer: state => state.drawer
      })
    },
    methods: {
    }
  }
</script>