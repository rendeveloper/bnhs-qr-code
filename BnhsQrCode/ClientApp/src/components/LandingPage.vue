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
                
                <p class="error">{{ error }}</p>
                <p class="decode-result">Last result: <b>{{ qrResult }}</b></p>

                <!-- <qrcode-stream v-if="showQRStream" :track="paintDots" @decode="onDecode" @init="onInit" /> -->
                <div v-if="showQRStream">
                  <p class="error" v-if="noFrontCamera">
                    You don't seem to have a front camera on your device
                  </p>

                  <p class="error" v-if="noRearCamera">
                    You don't seem to have a rear camera on your device
                  </p>
                </div>
                <qrcode-drop-zone v-if="showQRStream" @decode="onDecode" @init="logErrors">
                  <qrcode-stream style="height: 350px; width: 350px;" :camera="cameraSwitch" @decode="onDecode" :track="paintDots" @init="onInit">
                    <button style="width: 30px; height: 30px; float: left; top: 10px; left: 10px;" @click="switchCamera()">
                      <img style="width: 40px; height: 40px;" :src="require('../assets/switch-camera-100.png')" alt="switch camera">
                    </button>
                  </qrcode-stream>
                </qrcode-drop-zone>

                <qrcode-capture v-if="noStreamApiSupport" @decode="onDecode" />
                <v-btn v-if="!showQRStream"
                    class="ma-2"
                    :loading="loading"
                    :disabled="loading"
                    color="primary"
                    @click="onStartQR()"
                  >
                    Start
                  </v-btn>
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
                <span style="color: rgb(0 0 0 / 60%); float: right; margin-top: -45px; margin-right: 40px;">{{ formatDate() }}</span>
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
  import { mapState, mapActions } from 'vuex'
  export default {
    name: 'LandingPage',

    data() {
      return {
        rules: [
          value => !!value || 'Required.',
          value => (value && value.length >= 2) || 'Min 2 digit',
        ],
        loader: null,
        loading: false,
        qrResult: '',
        error: '',
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
        noStreamApiSupport: false,
        cameraSwitch: 'front',
        noRearCamera: false,
        noFrontCamera: false,
        skeletonLoading: true
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
        if(this.scanHistory.bodyTemp == '' || this.scanHistory.timeStatus == ''){
          this.$message({
            message: "Required fields.",
            type: "error"
          })
          return;
        }

        var self = this
        self.scanHistory.userProfileId = self.formData.id
        await self.saveScanHistory(self.scanHistory).then(response => {
          self.loader = 'loading'
          setTimeout(() =>{
            self.showQRStream = false
            self.showQRCode = true
            self.showAlert = true
            setTimeout(() => {
              self.showAlert = false
              self.formData = {
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
              }
              self.scanHistory = {
                userProfileId: 0,
                bodyTemp: "",
                timeStatus: "",
                CreatedByDateTime: new Date()
              }
            }, 1000)
          }, 2000)
        }).catch(error => {

        })
      },
      onStartQR(){
        this.loader = 'loading'
        setTimeout(() => (this.showQRStream = true), 2000)
      },
      qrStart(){
        this.showQRStream = true
      },
      async onDecode (result) {
        if(result === undefined || result === "" || result === null){
          return;
        }
        var self = this
        var audio = new Audio(require("../assets/sounds/beep-07.wav"))
        audio.play()
        self.qrResult = result
        self.showQRCode = false
        self.skeletonLoading = true
        var scanQRCode = {
          teacherId: result
        }
        await self.getScanQRCode(scanQRCode).then(response => {
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
          self.formData.image = 'data:image/png;base64,' + response.data.image
          self.skeletonLoading = false
        }).catch(error => {
        })
      },
      async onInit (promise) {
        try {
          await promise
        } catch (error) {

          if (error.name === 'NotAllowedError') {
            this.error = "ERROR: you need to grant camera access permisson"
          } else if (error.name === 'NotFoundError') {
            this.error = "ERROR: no camera on this device"
          } else if (error.name === 'NotSupportedError') {
            this.error = "ERROR: secure context required (HTTPS, localhost)"
          } else if (error.name === 'NotReadableError') {
            this.error = "ERROR: is the camera already in use?"
          } else if (error.name === 'OverconstrainedError') {
            this.error = "ERROR: installed cameras are not suitable"
          } else if (error.name === 'StreamApiNotSupportedError') {
            this.noStreamApiSupport = true
            this.error = "ERROR: Stream API is not supported in this browser"
          }
          
          const triedFrontCamera = this.cameraSwitch === 'front'
          const triedRearCamera = this.cameraSwitch === 'rear'

          const cameraMissingError = error.name === 'OverconstrainedError'

          if (triedRearCamera && cameraMissingError) {
            this.noRearCamera = true
          }

          if (triedFrontCamera && cameraMissingError) {
            this.noFrontCamera = true
          }

          console.error(error)
        }
      },
      logErrors (promise) {
        promise.catch(console.error)
      },
      paintDots(location, ctx) {
        const {
          topLeftFinderPattern,
          topRightFinderPattern,
          bottomLeftFinderPattern
        } = location

        const pointArray = [
          topLeftFinderPattern,
          topRightFinderPattern,
          bottomLeftFinderPattern
        ]

        ctx.fillStyle = '#60DB01'

        pointArray.forEach(({ x, y }) => {
          ctx.fillRect(x - 5, y - 5, 10, 10)
        })
      },
      formatDate() {
          var newDate = new Date();

          var sMonth = this.padValue(newDate.getMonth() + 1);
          var sDay = this.padValue(newDate.getDate());
          var sYear = newDate.getFullYear();
          var sHour = newDate.getHours();
          var sMinute = this.padValue(newDate.getMinutes());
          var sAMPM = "AM";

          var iHourCheck = parseInt(sHour);

          if (iHourCheck > 12) {
              sAMPM = "PM";
              sHour = iHourCheck - 12;
          }
          else if (iHourCheck === 0) {
              sHour = "12";
          }

          sHour = this.padValue(sHour);

          return sMonth + "/" + sDay + "/" + sYear + " " + sHour + ":" + sMinute + " " + sAMPM;
      },
      padValue(value) {
          return (value < 10) ? "0" + value : value;
      },
      switchCamera () {
        switch (this.cameraSwitch) {
          case 'front':
            this.cameraSwitch = 'rear'
            break
          case 'rear':
            this.cameraSwitch = 'front'
            break
        }
      }
    }
  }
</script>
<style>
 #project-joms{
   border-radius: 5px;
 }
 .alert-fixed {
    position:fixed; 
    top: 80px; 
    z-index:9999; 
}
</style>
