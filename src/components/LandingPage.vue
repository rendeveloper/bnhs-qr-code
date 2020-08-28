<template>
  <v-container>
    <v-row class="text-center">
      <v-col cols="12">
          <div style="float: right;">
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
            <v-row class="mt-5" justify="center">
              <div style="height: 350px; width: 350px; margin-top: 100px;">
                <p class="error">{{ error }}</p>
                <p class="decode-result">Last result: <b>{{ qrResult }}</b></p>

                <qrcode-stream v-if="showQRStream" :track="paintDots" @decode="onDecode" @init="onInit" />

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
                  alt="Vuetify Logo"
                  class="shrink mr-2 mt-8"
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
                style="width: 250px"/>
              </div>
            </v-col>
          </v-row>
          <v-row  justify="center"
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
            teacherId: "TABINAS-2020-01",
            name: "JOEMY D. TABINAS",
            department: "SHS Department",
            grade: "Grade 11-HUMSS Religion",
            image: require("../assets/1.jpg"),
            bodyTemp: 0
          },
          {
            id: 2,
            teacherId: "SALVA-2020-02",
            name: "SHEENA MARIE C. SALVA",
            department: "SHS Department",
            grade: "Grade 12-HUMSS Philosophy",
            image: require("../assets/2.jpg"),
            bodyTemp: 0
          },
          {
            id: 3,
            teacherId: "RUALES-2020-03",
            name: "EDWIN B. RUALES",
            department: "SHS Department",
            grade: "Grade 12-HUMSS History",
            image: require("../assets/3.jpg"),
            bodyTemp: 0
          }
        ],
        qrResult: '',
        error: '',
        showQRCode: true,
        showQRStream: false,
        formData: {
          id: 0,
          teacherId: "SALVA-2020-02",
          name: "SHEENA MARIE C. SALVA",
          department: "SHS Department",
          grade: "Grade 12-HUMSS Philosophy",
          image: require("../assets/2.jpg"),
          bodyTemp: 0
        },
        showAlert: false
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
          department: resultData.department,
          grade: resultData.grade,
          image: resultData.image,
          bodyTemp: null
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
            this.error = "ERROR: Stream API is not supported in this browser"
          }
        }
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
      }
    }
  }
</script>