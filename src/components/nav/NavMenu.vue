<template>
  <v-sheet>
  <v-navigation-drawer
      v-model="drawer"
      absolute
      temporary
      dark
      id="core-navigation-drawer"
    >
     <v-divider class="mb-1" />
      
      <v-list-item>
        <v-list-item-avatar>
          <v-img src="../../assets/joemy.png"></v-img>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title><strong>Admin Joemy</strong></v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider class="mb-2" />

      <v-list>
        <v-list-item link :to="'/'">
          <v-list-item-icon>
            <v-icon>mdi-view-dashboard</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Dashboard</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        
        <v-list-group
          v-for="item in menuItems"
          :key="item.title"
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
        menuItems: [
          {
            icon: 'mdi-account',
            title: 'User Profile',
            items: [
              { 
                title: 'All Users',
                icon: 'mdi-account-multiple', 
                link: '/user/all' 
              },
              { title: 'Create',
                icon: 'mdi-plus', 
                link: '/user/create' 
              }
            ]
          }
        ]
      }
    },
    watch: {
    },
    computed: {
      drawer: {
        get () {
          return this.$store.state.drawer
        },
        set (val) {
          this.$store.commit('SET_DRAWER', val)
        },
      }
    },
    methods: {
    }
  }
</script>
<style>
  div.v-list-group.v-list-group--active.v-list-group--no-action.primary--text {
    color: #ffffff !important;
  }
</style>
