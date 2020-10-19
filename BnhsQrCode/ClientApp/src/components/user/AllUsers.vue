<template> 
  <v-container id="user-profile" fluid tag="section" style="margin-top: 100px;">
    <v-row justify="center">
      <v-col cols="12" md="11">
        <v-data-table :headers="headers" :items="items" :search="searchTbl" sort-by="firstName" :loading="tblLoading" loading-text="Loading... Please wait" class="elevation-1">
          <template v-slot:top>
            <v-toolbar flat >
              <v-card icon="mdi-clipboard-text" :elevation="8" color="#217BD4" class="mb-n6 pa-7" style="top: -25px; margin-right: 1%;">
                <v-icon color="white" large>mdi-clipboard-text</v-icon>
              </v-card>
              <v-toolbar-title>Teacher Profile</v-toolbar-title>
              <v-divider
                class="mx-4"
                inset
                vertical
              ></v-divider>
              <v-text-field
                v-model="searchTbl"
                append-icon="mdi-magnify"
                label="Search"
                single-line
                hide-details
                clearable
              ></v-text-field>
              <v-spacer></v-spacer>
              <v-spacer></v-spacer>
              <v-spacer></v-spacer>
              <v-btn color="primary" dark class="mb-2 mt-1" :loading="btnLoading" @click="newItem()">
                New Item
              </v-btn>
            </v-toolbar>
          </template>
          <template v-slot:item.actions="{ item }">
            <v-slide-x-reverse-transition>
              <v-tooltip
                bottom
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    icon
                    v-bind="attrs"
                    class="my-0"
                    @click.stop="timeItem(item)"
                    v-on="on"
                  >
                    <v-icon>mdi-timetable</v-icon>
                  </v-btn>
                </template>
                <span>Time Table</span>
              </v-tooltip>
            </v-slide-x-reverse-transition>
            <v-slide-x-reverse-transition>
              <v-tooltip
                bottom
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    icon
                    v-bind="attrs"
                    class="my-0"
                    @click="editItem(item)"
                    v-on="on"
                  >
                    <v-icon>mdi-pencil</v-icon>
                  </v-btn>
                </template>
                <span>Edit</span>
              </v-tooltip>
            </v-slide-x-reverse-transition>
            <v-slide-x-reverse-transition>
              <v-tooltip
                bottom
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    icon
                    v-bind="attrs"
                    class="my-0"
                    @click="deleteItem(item)"
                    v-on="on"
                  >
                    <v-icon>mdi-delete</v-icon>
                  </v-btn>
                </template>
                <span>Delete</span>
              </v-tooltip>
            </v-slide-x-reverse-transition>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
    <v-row justify="center">
      <v-dialog
        v-model="dialogTime"
        max-width="600px"
      >
        <v-card>
          <v-card-title>
            <v-icon class="mr-1">mdi-timetable</v-icon>Daily Time Table
            <v-spacer></v-spacer>
            <v-text-field
              v-model="searchTime"
              append-icon="mdi-magnify"
              label="Search"
              single-line
              hide-details
              clearable
            ></v-text-field>
          </v-card-title>
          <v-data-table
            :headers="headersTime"
            :items="itemsTime"
            :search="searchTime"
            :loading="timeTblLoading" 
            loading-text="Loading... Please wait"
          >
          <template v-slot:header.bodyTemp="{ header }">
            <strong>{{ header.text }}</strong> <v-icon small>mdi-temperature-celsius</v-icon>
          </template>
          <template v-slot:item.bodyTemp="{ item }">
            <strong>{{ item.bodyTemp }}</strong>
          </template>
          <template v-slot:item.timeStatus="{ item }">
            <v-chip
              :color="getColorTime(item.timeStatus)"
              label
              dark
            >
              {{ item.timeStatus.toUpperCase() }}
            </v-chip>
          </template>
          <template v-slot:item.createdByDateTime="{ item }">
            <strong>{{ item.createdByDateTime }}</strong>
          </template>
          </v-data-table>
          <v-divider></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
            color="primary"
            text
            @click="dialogTime = false"
          >
            Close
            <v-icon>mdi-close</v-icon>
          </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>
  </v-container>
</template>

<script>
  import { mapState, mapActions } from 'vuex'
  import '../../assets/css/vuetify_material.css'
  export default {
    name: 'AllUsers',
    data() {
      return {      
        btnLoading: false,
        tblLoading: false,
        searchTbl: '',
        headers: [
          { text: 'Teacher Id', align: 'start', value: 'teacherId' },
          { text: 'First name', value: 'firstName' },
          { text: 'Middle name', value: 'middleName' },
          { text: 'Last name', value: 'lastName' },
          { text: 'Date of birth', value: 'dateOfBirth' },
          { text: 'Address', value: 'address' },
          { text: 'Health status', value: 'healthStatus' },
          { text: 'Department', value: 'department' },
          { text: 'Grade / Admin / Principal / Role', value: 'role' },
          { text: 'Actions', value: 'actions', sortable: false },
        ],
        items: [],
        dialogTime: false,
        timeTblLoading: false,
        searchTime: '',
        headersTime: [
          { text: 'Body temperature', align: 'start', value: 'bodyTemp'},
          { text: 'Status', value: 'timeStatus' },
          { text: 'Date & Time', value: 'createdByDateTime' }
        ],
        itemsTime: []
      }
    },
    created(){
      this.initialize()
    },
    computed: {
      ...mapState({
        users: state => state.api.users
      })
    },
    watch: {
    },
    methods: {
      ...mapActions(['getAllUsers', 'deleteUser', 'getAllTimeUser']),
      async initialize () {
        var self = this
        self.tblLoading = true
        await self.getAllUsers().then(response => {
          var tempItems = []
          if(response.status === 200){
            response.data.forEach(user => {
              const monthNames = ["January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
              ];
              const d = new Date(user.dateOfBirth);
              tempItems.push({
                id: user.id,
                teacherId: user.teacherId,
                firstName: user.firstName,
                lastName: user.lastName,
                middleName: user.middleName,
                dateOfBirth: monthNames[d.getMonth()].toUpperCase() + ' ' + d.getFullYear(),
                address: user.address,
                healthStatus: user.healthStatus,
                department: user.department,
                role: user.role
              })
            });
            setTimeout(() => {
              self.items = tempItems
              self.tblLoading = false
            }, 500)
          }
        })
      },      
      editItem (item) {
        this.$router.push('/user/edit/' + item.id)
      },
      async deleteItem (item) {
        var self = this
        self.$confirm('This will permanently delete the data. Continue?', 'Warning', {
          confirmButtonText: 'OK',
          cancelButtonText: 'Cancel',
          type: 'warning',
          beforeClose: (action, instance, done) => {
            if (action === 'confirm') {
              instance.confirmButtonLoading = true;
              instance.confirmButtonText = 'Loading...';
              setTimeout(() => {
                done();
                setTimeout(() => {
                  instance.confirmButtonLoading = false;
                }, 300);
              }, 1000);
            } else {
              done();
            }
          }
        }).then(async() => {
          self.tblLoading = true
          await self.deleteUser(item.id).then(async (response) => {
            if(response.status === 200){
              setTimeout(() => {
                const index = self.items.indexOf(item)
                self.items.splice(index, 1)
                self.tblLoading = false
                self.$message({
                  type: 'success',
                  message: 'Delete completed'
                });
              }, 500)
            }
          }).catch(() => {
            self.$message({
                message: "An unexpected error occurred",
                type: "error"
            })
          })
        }).catch(() => {
          self.$message({
            type: 'error',
            message: 'Delete canceled'
          });
        });
      },
      newItem(){
        this.btnLoading = true
        setTimeout(() => {
          this.$router.push('/user/create')
          this.btnLoading = false
        }, 500)
      },
      async timeItem(item){
        var self = this
        self.itemsTime = []
        self.dialogTime = true
        self.timeTblLoading = true
        await self.getAllTimeUser(item.id).then(response => {
          self.itemsTime = response.data
          self.timeTblLoading = false
        }).catch(() => {
          self.$message({
              message: "An unexpected error occurred",
              type: "error"
          })
        })
      },
      getColorTime(timeStatus) {
        if (timeStatus == 'In') return '#4CAF50'
        else return '#FF5252'
      }
    }
  }
</script>
