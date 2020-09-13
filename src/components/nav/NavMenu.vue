<template>
  <v-sheet>
  <v-navigation-drawer
      v-model="menuDrawer"
      absolute
      temporary
      dark
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

      <v-list>
        <v-list-item link :to="'/'">
          <v-list-item-icon>
            <v-icon>mdi-home</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-group
          v-for="item in menuItems"
          :key="item.title"
          v-model="item.active"
          :prepend-icon="item.icon"
          no-action
        >
          <template v-slot:activator>
            <v-list-item-content>
              <v-list-item-title v-text="item.title"></v-list-item-title>
            </v-list-item-content>
          </template>

          <v-list-item
            v-for="subItem in item.items"
            :key="subItem.title"
            link
            :to="subItem.link"
          >

            <v-list-item-content>
              <v-list-item-title v-text="subItem.title"></v-list-item-title>
            </v-list-item-content>
            
            <v-list-item-icon>
              <v-icon>{{ subItem.icon }}</v-icon>
            </v-list-item-icon>
          </v-list-item>
        </v-list-group>
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
        menuItems: [
          {
            icon: 'mdi-account',
            title: 'Users',
            items: [
              { title: 'Create',
                icon: 'mdi-plus', 
                link: '/create' 
              }
            ]
          }
        ]
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