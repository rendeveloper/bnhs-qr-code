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
          alt="bnhs Logo"
          class="shrink mr-2"
          contain
          src="../../assets/bnhs-logo.png"
          transition="scale-transition"
          width="75"
        />
        <h2>Bato National High School</h2>
      </div>

      <v-spacer></v-spacer>
      <v-spacer></v-spacer>
      <v-spacer></v-spacer>
      <v-spacer></v-spacer> 
        <v-text-field
          v-if="isLoggedIn"
          label="Track Your Time, Enter your teacher id"
          single-line
          v-model="teacherId"
          hide-details
          clearable
        ></v-text-field>
        <v-btn
          v-if="isLoggedIn"
          text
          @click="getUser()"
        >
          <span class="mr-2" style="letter-spacing: 2px;"><strong>Track</strong></span>
          <v-icon>mdi-magnify</v-icon>
      </v-btn>
    </v-app-bar>
    
    <v-row justify="center">
      <v-dialog
        v-model="dialogTime"
        max-width="600px"
      >
        <v-card>
          <v-card-title>
            <v-icon class="mr-1">mdi-timetable</v-icon>Daily Time Table
            <v-spacer></v-spacer>
            <v-menu
              v-model="menuDate"
              :close-on-content-click="false"
              :nudge-right="40"
              transition="scale-transition"
              offset-y
              min-width="290px"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  icon
                  v-bind="attrs"
                  v-on="on"
                  ref="dateOfBirth"
                >
                  <v-icon>mdi-calendar</v-icon>
                </v-btn>
              </template>
              <v-date-picker v-model="dateValue" @input="menuDate = false">
                  <v-spacer></v-spacer>
                <v-btn text color="primary" @click="menuDate = false"><strong>Cancel</strong></v-btn>
                <v-btn text color="primary" @click="menuDate = false"><strong>OK</strong></v-btn>
              </v-date-picker>
            </v-menu>
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

  export default {
    name: 'NavBar',
    data() {
      return {
        teacherId: "",
        dialogTime: false,
        timeTblLoading: false,
        searchTime: '',
        menuDate: false,
        dateValue: '',
        headersTime: [
          { text: 'Date & Time', value: 'createdByDateTime' },
          { text: 'Status', value: 'timeStatus' },
          { text: 'Body temperature', align: 'start', value: 'bodyTemp'}
        ],
        itemsTime: []
      }
    },
    computed: {
      ...mapState({
        drawer: state => state.drawer.isOpen,
        isLoggedIn: state => state.api.isLoggedIn
      })
    },
    watch: {
      dateValue: function(val){
        if(val !== null && val !== undefined && val !== ""){
          var self = this
          var newDate = new Date(val);
          var sDay = self.padValue(newDate.getDate());
          var sYear = newDate.getFullYear();
          const monthNames = ["January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
          ];
          self.searchTime = monthNames[newDate.getMonth()] + " " + sDay + " " + sYear
        }
      }
    }, 
    methods: {
      ...mapActions({
        openDrawer: 'OPEN_DRAWER'
      }),
      ...mapActions(['getScanQRCode', 'getAllTimeUser']),
      isPopUp(){
        let user = JSON.parse(localStorage.getItem('user'));
        if(this.isLoggedIn && user !== null && user.admin){
          this.openDrawer(!this.drawer)
        }
      },
      async getUser(){
        var self = this
        self.itemsTime = []
        self.dialogTime = true
        self.timeTblLoading = true
        var scanQRCode = {
          teacherId: self.teacherId
        }
        await self.getScanQRCode(scanQRCode).then(async (response) => {
          await self.getAllTimeUser(response.data.id).then(x => {
            self.itemsTime = x.data
            self.timeTblLoading = false
          }).catch(() => {
            self.$message({
                message: "Please enter a valid teacher id",
                type: "error"
            })
          })
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
      },
      padValue(value) {
          return (value < 10) ? "0" + value : value;
      }
    }
  }
</script>

<style scoped>
.v-text-field {
    padding-top: 0px !important;
    margin-top: 0px !important;
}
</style>

