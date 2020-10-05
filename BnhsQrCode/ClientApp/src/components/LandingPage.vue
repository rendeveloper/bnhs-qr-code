<template>
  <v-container class="mt-8">
    <v-row class="text-center">
      <v-col cols="12">
          <div class="alert-fixed" justify="center">
            <v-alert
              :value="showAlert"
              width="380"
              dense
              :prominent="true"
              text
              :dismissible="true"
              transition="slide-y-transition"
              type="success"
            >
              Submit <strong>completed!</strong>
            </v-alert>
          </div>
          <v-card v-if="showQRCode"
            class="mx-auto"
            max-width="650"
            width="580"
            height="665"
            :loading="loading"
          >
            <v-row justify="center">
              <v-img
                id="project-joms"
                alt="Vuetify Image Teacher"
                class="shrink mt-2"
                contain
                src="../assets/project-joms.jpg"
                transition="scale-transition"
                width="450"
                height="207"
              />
              <!-- <div class="mt-15">
                <h1>Welcome to Project JOMS</h1>
                <h4>justifying Online Monitoring System</h4>
              </div> -->
              <div style="height: 350px; width: 350px;">
                
                <p class="decode-result mt-4">Last result: <b>{{ qrResult }}</b></p>
                
                <vue-qr-reader 
                  v-if="showQRStream"
                  ref="qr"
                  v-on:code-scanned="codeScanned"
                  v-on:error-captured="errorScanned"
                  :stop-on-scanned="true"
                  :draw-on-found="true"
                  line-color="#00FF00"
                  :line-width="5"
                  :video-height="350"
                  :video-width="350"
                  :responsive="true">
                </vue-qr-reader>
                <v-btn v-if="!showQRStream"
                    class="ma-2"
                    :loading="loading"
                    :disabled="loading"
                    color="primary"
                    @click="onStartQR()"
                  >
                    Start
                  </v-btn>
                  <v-slide-x-reverse-transition>
                    <v-tooltip
                      v-if="showQRStream"
                      bottom
                    >
                      <template v-slot:activator="{ on, attrs }">
                        <v-btn
                          icon
                          class="my-0"
                          v-bind="attrs"
                          @click="resetForm"
                          v-on="on"
                        >
                          <v-icon>mdi-refresh</v-icon>
                        </v-btn>
                      </template>
                      <span>Refresh QR code scan</span>
                    </v-tooltip>
                  </v-slide-x-reverse-transition>
              </div>
            </v-row>
          </v-card>
          <v-card v-else
            class="mx-auto"
            max-width="650"
            width="580"
            height="665"
            :loading="loading"
          >
            <v-row
              class="mb-6"
              no-gutters
            >
              <v-col
                :lg="6"
                :md="6"
                :sm="6"
              >
              <v-img
                alt="Vuetify Logo"
                class="shrink mr-2 ml-3 mt-8"
                contain
                src="../assets/bnhs-logo.png"
                transition="scale-transition"
                width="250"
              />
              </v-col>
              <v-col
                :lg="6"
                :md="6"
                :sm="6"
              >
              <v-card width="250" height="250" :elevation="3">
                  <v-img
                    alt="Vuetify Image Teacher"
                    class="shrink mr-2 mt-4"
                    contain
                    :src="formData.image"
                    transition="scale-transition"
                    width="250"
                    height="250"
                  />
              </v-card>
            </v-col>
          </v-row>
          <v-row 
            class="mb-6"
            no-gutters
          >
            <v-col
              :lg="6"
              :md="6"
              :sm="6"
            >
            <div style="color: rgb(0 0 0 / 60%); 
                        float: left; 
                        margin-top: -40px; 
                        margin-left: 45px;
                        text-align: left;">
               <v-skeleton-loader
                :loading="skeletonLoading"
                transition="none"
                type="text"
                width="150"
              >         
              <h4> {{ formData.teacherId }} </h4>
              </v-skeleton-loader>

              <v-skeleton-loader
                :loading="skeletonLoading"
                type="text"
                width="150"
              >
              <h4> {{ formData.dateOfBirth }} </h4>
              </v-skeleton-loader>

              <v-skeleton-loader
                :loading="skeletonLoading"
                type="text"
                width="150"
              >
              <h4> {{ formData.address }} </h4>
              </v-skeleton-loader>

              <v-skeleton-loader
                :loading="skeletonLoading"
                type="text"
                width="150"
              >
              <h4> {{ formData.healthStatus }} </h4>
              </v-skeleton-loader>
            </div>
            </v-col>
            <v-col
              :lg="6"
              :md="6"
              :sm="6"
            >
              <div>
                <v-text-field 
                class="mb-6"
                label="Body Temp." 
                :rules="rules" 
                hide-details="auto" 
                type="number"
                v-model="scanHistory.bodyTemp"
                style="width: 250px; margin-top: -5px;"/>
                <p v-if="scanHistory.bodyTemp >= 38" class="v-messages theme--light error--text" style="margin-top: -20px;">You've got a fever! please, isolate yourself.</p>
                <v-select
                  :items="['In', 'Out']"
                  label="Time"
                  style="width: 80px; margin-top: -20px;"
                  v-model="scanHistory.timeStatus"
                ></v-select>
                <span style="color: rgb(0 0 0 / 60%); float: right; margin-top: -45px; margin-right: 40px;">{{ displayDate }}</span>
              </div>
            </v-col>
          </v-row>
          <v-row  justify="center" style="margin-top: -20px;"
            >
            <v-card
            color="#DB6001"
            dark
            width="500"
          >
            <div>
              <div class="mt-6 mb-6">
                <v-skeleton-loader
                  :loading="skeletonLoading"
                  type="text"
                  width="160"
                  class="mx-auto"
                >
                  <span style='font-size: 1.5rem !important;
                              font-weight: 400;
                              line-height: 2rem;
                              letter-spacing: normal !important;
                              font-family: "Roboto", sans-serif !important;
                              '>{{ formData.department }}</span>
                </v-skeleton-loader>

                <v-skeleton-loader
                  :loading="skeletonLoading"
                  type="text"
                  width="200"
                  class="mx-auto"
                >
                  <h3 class="mb-1 mt-2" style="font-size: 1.5rem !important;
                              font-weight: 500;
                              line-height: 2rem;
                              letter-spacing: normal !important;
                  ">{{ (formData.firstName + ' ' + formData.middleName + ' ' + formData.lastName).toUpperCase() }}</h3>
              </v-skeleton-loader>

                <v-skeleton-loader
                  :loading="skeletonLoading"
                type="text"
                width="160"
                class="mx-auto"
              >
                <span class="mb-1" style="font-size: 0.875rem;
                             font-weight: 400;
                             line-height: 1.375rem;
                             letter-spacing: 0.0071428571em;
                             color: rgba(255, 255, 255, 0.7);
                             ">{{ formData.role }}</span>
              </v-skeleton-loader>
              </div>
            </div>
          </v-card>
          </v-row>
          <v-row class="mt-5" justify="center"
            >
            <v-btn
              class="ma-2"
              :loading="loading"
              :disabled="loading"
              color="primary"
              @click="onSubmit()"
            >
              Submit
            </v-btn>
          </v-row>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  import VueQrReader from 'vue-qr-reader/dist/lib/vue-qr-reader.umd.js';
  import { mapState, mapActions } from 'vuex'
  export default {
    name: 'LandingPage',
    components: {
      VueQrReader
    },
    data() {
      return {
        rules: [
          value => !!value || 'Required.',
          value => (value && value.length >= 2) || 'Min 2 digit',
        ],
        loader: null,
        loading: false,
        qrResult: '',
        showQRCode: true,
        showQRStream: false,
        formData: {
          id: 0,
          teacherId: "",
          firstName: "",
          lastName: "",
          middleName: "",
          dateOfBirth: "",
          address: "",
          healthStatus: "",
          department: "",
          role: "",
          image: require("../assets/image/img_avatar.png")
        },
        scanHistory: {
          userProfileId: 0,
          bodyTemp: "",
          timeStatus: "",
          CreatedByDateTime: new Date()
        },
        showAlert: false,
        skeletonLoading: true,
        displayDate: "",
        getOriginalTime: ""
      }
    },
    created() {
    },
    computed: {
      cols () {
        const { lg, sm } = this.$vuetify.breakpoint
        return lg ? [3, 9] : sm ? [9, 3] : [6, 6]
      }
    },
    watch: {
      loader () {
        const lr = this.loader
        this[lr] = !this[lr]

        setTimeout(() => (this[lr] = false), 2000)

        this.loader = null
      },
    },
    methods: {
      ...mapActions(['getScanQRCode', 'saveScanHistory']),
      async onSubmit(){
        this.formatDate()
        if(this.scanHistory.bodyTemp == '' || this.scanHistory.timeStatus == ''){
          this.$message({
            message: "Required fields.",
            type: "error"
          })
          return;
        }

        var self = this
        self.loader = 'loading'
        self.scanHistory.userProfileId = self.formData.id
        self.scanHistory.CreatedByDateTime = self.getOriginalTime
        await self.saveScanHistory(self.scanHistory).then(response => {
          setTimeout(() =>{
            self.showQRStream = true
            self.showQRCode = true
            self.showAlert = true
            setTimeout(() => {
              self.showAlert = false
              self.formData.id = 0
              self.formData.teacherId = ""
              self.formData.firstName = ""
              self.formData.lastName = ""
              self.formData.middleName = ""
              self.formData.dateOfBirth = ""
              self.formData.address = ""
              self.formData.healthStatus = ""
              self.formData.department = ""
              self.formData.role = ""
              self.formData.image = require("../assets/image/img_avatar.png")
              self.scanHistory.userProfileId = 0
              self.scanHistory.bodyTemp = ""
              self.scanHistory.timeStatus = ""
              self.scanHistory.CreatedByDateTime = new Date()
              self.getOriginalTime = ""
              self.displayDate = ""
            }, 500)
          }, 1000)
        }).catch(error => {
          self.$message({
            message: "An unexpected error occurred",
            type: "error"
          })
        })
        setTimeout(() => (this.showQRStream = false), 15000)
      },
      onStartQR(){
        var self = this
        self.loader = 'loading'
        self.formData.id = 0
        self.formData.teacherId = ""
        self.formData.firstName = ""
        self.formData.lastName = ""
        self.formData.middleName = ""
        self.formData.dateOfBirth = ""
        self.formData.address = ""
        self.formData.healthStatus = ""
        self.formData.department = ""
        self.formData.role = ""
        self.formData.image = require("../assets/image/img_avatar.png")
        self.scanHistory.userProfileId = 0
        self.scanHistory.bodyTemp = ""
        self.scanHistory.timeStatus = ""
        self.scanHistory.CreatedByDateTime = new Date()
        self.getOriginalTime = ""
        self.displayDate = ""
        setTimeout(() => (self.showQRStream = true), 2000)
      },
      formatDate() {
        var self = this
        self.getWorldTime().then(x => {
          var newDate = new Date(x);
            
          var sMonth = self.padValue(newDate.getMonth() + 1);
          var sDay = self.padValue(newDate.getDate());
          var sYear = newDate.getFullYear();
          var sHour = newDate.getHours();
          var sMinute = self.padValue(newDate.getMinutes());
          var sAMPM = "AM";

          var iHourCheck = parseInt(sHour);

          if (iHourCheck >= 12) {
              sAMPM = "PM";
              sHour = iHourCheck - 12;
          }
          else if (iHourCheck === 0) {
              sHour = "12";
          }

          sHour = self.padValue(sHour);
          const monthNames = ["January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
          ];

          self.getOriginalTime = monthNames[newDate.getMonth()] + ' ' + sDay + ' ' + sYear + ' ' + sHour + ":" + sMinute + " " + sAMPM;
          self.displayDate = sMonth + "/" + sDay + "/" + sYear + " " + sHour + ":" + sMinute + " " + sAMPM;
        })
      },
      padValue(value) {
          return (value < 10) ? "0" + value : value;
      },
      async codeScanned (result) {
        if(result === undefined || result === "" || result === null){
          return;
        }
        var self = this
        var audio = new Audio(require("../assets/sounds/beep-07.wav"))
        self.formatDate();
        self.qrResult = result
        self.skeletonLoading = true
        self.loading = true
        var scanQRCode = {
          teacherId: result
        }
        await self.getScanQRCode(scanQRCode).then(response => {
          audio.pause();
          audio.currentTime = 0;
          audio.play()
          self.showQRCode = false
          self.loading = false
          const monthNames = ["January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
          ];
          const d = new Date(response.data.dateOfBirth);

          self.formData.id = response.data.id
          self.formData.teacherId = response.data.teacherId
          self.formData.firstName = response.data.firstName
          self.formData.lastName = response.data.lastName
          self.formData.middleName = response.data.middleName
          self.formData.name = response.data.name

          self.formData.dateOfBirth = monthNames[d.getMonth()].toUpperCase() + ' ' + d.getFullYear()

          self.formData.address = response.data.address
          self.formData.healthStatus = response.data.healthStatus
          self.formData.department = response.data.department
          self.formData.role = response.data.role
          self.formData.image = response.data.image === "" ? require("../assets/image/img_avatar.png") : '/images/' +  response.data.image
          self.skeletonLoading = false
        }).catch(() => {
          self.$message({
            message: "An unexpected error occurred",
            type: "error"
          })
        })
      },
      errorScanned(error) {
          switch (error.name) {
              case 'NotAllowedError':
              this.errorMessage = 'Camera permission denied.'
              break;
              case 'NotFoundError':
              this.errorMessage = 'There is no connected camera.'
              break;
              case 'NotSupportedError':
              this.errorMessage = 'Seems like this page is served in non-secure context.'
              break;
              case 'NotReadableError':
              this.errorMessage = 'Couldn\'t access your camera. Is it already in use?'
              break;
              case 'OverconstrainedError':
              this.errorMessage = 'Constraints don\'t match any installed camera.'
              break;
              default:
              this.errorMessage = 'UNKNOWN ERROR: ' + error.message
          }
          console.error(this.errorMessage);
      },
      resetForm(){
        this.showQRStream = false
        setTimeout(() => {
          this.showQRStream = true
        }, 0)
      },
      getWorldTime(){
        return fetch("https://worldtimeapi.org/api/ip")
        .then(response => response.json())
        .then(data => { return data.datetime });
      }
    }
  }
</script>
<style>
 #project-joms{
   border-radius: 5px;
 }
 .alert-fixed {
    position: absolute; 
    top: 80px; 
    z-index:9999; 
}
</style>
