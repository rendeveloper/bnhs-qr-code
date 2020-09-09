<template>
  <v-container>
    <v-row class="text-center">
      <v-col cols="12">
          <div class="alert-fixed">
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
            height="690"
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
                    @click="onSubmitQR()"
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
                        margin-top: -50px; 
                        margin-left: 45px;
                        text-align: left;">
              <h4> {{ formData.dateOfBirth }} </h4>
              <h4> {{ formData.teacherId }} </h4>
              <h4> {{ formData.address }} </h4>
              <h4> {{ formData.healthStatus }} </h4>
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
                v-model="formData.bodyTemp"
                style="width: 250px; margin-top: -5px;"/>
                <p v-if="formData.bodyTemp >= 38" class="v-messages theme--light error--text" style="margin-top: -20px;">You've got a fever! please, isolate yourself.</p>
                <v-select
                  :items="['In', 'Out']"
                  label="Time"
                  style="width: 80px; margin-top: -20px;"
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
                <span style='font-size: 1.5rem !important;
                            font-weight: 400;
                            line-height: 2rem;
                            letter-spacing: normal !important;
                            font-family: "Roboto", sans-serif !important;
                            '>{{ formData.department }}</span>
                
                <h3 class="mb-1 mt-2" style="font-size: 1.5rem !important;
                            font-weight: 500;
                            line-height: 2rem;
                            letter-spacing: normal !important;
                ">{{ formData.name }}</h3>
                <span class="mb-1" style="font-size: 0.875rem;
                             font-weight: 400;
                             line-height: 1.375rem;
                             letter-spacing: 0.0071428571em;
                             color: rgba(255, 255, 255, 0.7);
                             ">{{ formData.grade }}</span>
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
        dataSchool: [
          {
            id: 1,
            teacherId: "DepEd-4657626",
            name: "JOEMY D. TABINAS",
            dateOfBirth: "SEPTEMBER 1981",
            address: "Ponong, Bato, Leyte",
            healthStatus: "Fit to Work",
            department: "SHS Department",
            grade: "Grade 11-HUMSS Religion",
            image: require("../assets/joemy.png"),
            bodyTemp: 0
          },
          {
            id: 2,
            teacherId: "DepEd-6291611",
            name: "SHEENA MARIE C. SALVA",
            dateOfBirth: "APRIL 1987",
            address: "Guerrero, Bato, Leyte",
            healthStatus: "Fit to Work",
            department: "SHS Department",
            grade: "Grade 12-HUMSS Philosophy",
            image: require("../assets/sheena.png"),
            bodyTemp: 0
          },
          {
            id: 3,
            teacherId: "DepEd-6298175",
            name: "EDWIN B. RUALES",
            dateOfBirth: "AUGUST 1994",
            address: "Tabunok, Bato, Leyte",
            healthStatus: "Fit to Work",
            department: "SHS Department",
            grade: "Grade 12-HUMSS History",
            image: require("../assets/edwin.png"),
            bodyTemp: 0
          },
          {
            id: 4,
            teacherId: "DepEd-6294080",
            name: "FERNANDO A. RALLOS JR.",
            dateOfBirth: "SEPTEMBER 1979",
            address: "Guerrero, Bato, Leyte",
            healthStatus: "Fit to Work",
            department: "SHS Department",
            grade: "G12 - ABM Profit",
            image: require("../assets/fernando.jpg"),
            bodyTemp: 0
          },
          {
            id: 5,
            teacherId: "DepEd-6296691",
            name: "DIONITO I. QUISMONDO",
            dateOfBirth: "JUNE 1980",
            address: "Amagos, Bato, Leyte",
            healthStatus: "Fit to Work",
            department: "SHS Department",
            grade: "G11 - SMAW Welders",
            image: require("../assets/dionito.jpg"),
            bodyTemp: 0
          },
          {
            id: 6,
            teacherId: "DepEd-4553619",
            name: "MACARIO S. LELIS",
            dateOfBirth: "FEBRUARY 1979",
            address: "bagongbayan, Bato, Leyte",
            healthStatus: "Fit to Work",
            department: "SHS Department",
            grade: "G12 - SMAW Builders",
            image: require("../assets/macario.jpg"),
            bodyTemp: 0
          },
          {
            id: 7,
            teacherId: "DepEd-6298044",
            name: "WINNIELYN F. JAGUNOS",
            dateOfBirth: "APRIL 1979",
            address: "Tugas, Bato, Leyte",
            healthStatus: "Hypertension with maintenance",
            department: "SHS Department",
            grade: "G11 - ABM Asset",
            image: require("../assets/winnielyn.jpg"),
            bodyTemp: 0
          },
          {
            id: 8,
            teacherId: "DepEd-6303787",
            name: "Ma. MARVIN C. AMATORIO",
            dateOfBirth: "OCTOBER 1971",
            address: "Guerrero, Bato, Leyte",
            healthStatus: "Fit to work",
            department: "SHS Department",
            grade: "G12 - Cookery Chief",
            image: require("../assets/ma.marvin.png"),
            bodyTemp: 0
          },
          {
            id: 9,
            teacherId: "DepEd-6302010",
            name: "JENNIFER G. NADALA",
            dateOfBirth: "SEPTEMBER 1994",
            address: "Taglibas, Matalom, Leyte",
            healthStatus: "Fit to work",
            department: "SHS Department",
            grade: "G11 - HUMSS Culture",
            image: require("../assets/jennifer.jpg"),
            bodyTemp: 0
          },
          {
            id: 10,
            teacherId: "DepEd-6294081",
            name: "MELDIE M. SISON",
            dateOfBirth: "JUNE 1988",
            address: "P. Burgos St., Bato,Leyte",
            healthStatus: "Fit to work",
            department: "SHS Department",
            grade: "G11 - Cookery Cuisine",
            image: require("../assets/meldie.jpg"),
            bodyTemp: 0
          },
          {
            id: 11,
            teacherId: "DepEd-6297715",
            name: "GEORGE N. ESPLANADA",
            dateOfBirth: "February 1991",
            address: "Pob., Hilongos, Leyte",
            healthStatus: "Fit to work",
            department: "Non-Teaching Staff",
            grade: "Admin. Officer II",
            image: require("../assets/george.jpg"),
            bodyTemp: 0
          },
          {
            id: 12,
            teacherId: "DepEd-0002505",
            name: "HERA PAZ B. YAMSON",
            dateOfBirth: "MAY 1996",
            address: "Pob., Hilongos, Leyte",
            healthStatus: "Fit to work",
            department: "School Head",
            grade: "Principal III",
            image: require("../assets/hera.jpg"),
            bodyTemp: 0
          },
          {
            id: 13,
            teacherId: "DepEd-0006345",
            name: "NEMA G. ILAYON",
            dateOfBirth: "SEPTEMBER 1979",
            address: "Tinago, Bato, Leyte",
            healthStatus: "Fit to work",
            department: "JHS  Science Department",
            grade: "Master Teacher II",
            image: require("../assets/nema.jpg"),
            bodyTemp: 0
          },
          {
            id: 14,
            teacherId: "DepEd-4553619",
            name: "EMMYLOU S. SABANDAL",
            dateOfBirth: "SEPTEMBER 1983",
            address: "Bagongbayan, Bato, Leyte",
            healthStatus: "Fit to work",
            department: "JHS Aral. Pan Department",
            grade: "Dept. Head",
            image: require("../assets/emmylou.jpg"),
            bodyTemp: 0
          }
        ],
        qrResult: '',
        error: '',
        showQRCode: true,
        showQRStream: false,
        formData: {
          id: 2,
          teacherId: "DepEd-6291611",
          name: "SHEENA MARIE C. SALVA",
          dateOfBirth: "APRIL 1987",
          address: "Guerrero, Bato, Leyte",
          healthStatus: "Fit to Work",
          department: "SHS Department",
          grade: "Grade 12-HUMSS Philosophy",
          image: require("../assets/sheena.png"),
          bodyTemp: 0
        },
        showAlert: false,
        noStreamApiSupport: false,
        cameraSwitch: 'front',
        noRearCamera: false,
        noFrontCamera: false
      }
    },
    computed: {
      cols () {
        const { lg, sm } = this.$vuetify.breakpoint
        return lg ? [3, 9] : sm ? [9, 3] : [6, 6]
      },
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
      onSubmit(){
        if(this.formData.bodyTemp == ''){
          this.$message({
            message: "Required fields body temp.",
            type: "error"
          })
          return;
        }
        this.loader = 'loading'
        setTimeout(() =>{
          this.showQRStream = false
          this.showQRCode = true
          this.showAlert = true
          setTimeout(() => (this.showAlert = false), 1000)
        }, 2000)
      },
      onSubmitQR(){
        this.loader = 'loading'
        setTimeout(() => (this.showQRStream = true), 2000)
      },
      qrStart(){
        this.showQRStream = true
      },
      onDecode (result) {
        this.qrResult = result
        this.showQRCode = false
        var resultData = this.dataSchool.find(x => x.teacherId == this.qrResult)
        this.formData = {
          id: resultData.id,
          teacherId: resultData.teacherId,
          name: resultData.name,
          dateOfBirth: resultData.dateOfBirth,
          address: resultData.address,
          healthStatus: resultData.healthStatus,
          department: resultData.department,
          grade: resultData.grade,
          image: resultData.image,
          bodyTemp: ""
        }
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
