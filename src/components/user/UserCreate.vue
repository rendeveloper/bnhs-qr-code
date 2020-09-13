<template>
  <v-container
    id="user-profile"
    tag="section"
    style="margin-top: 100px;"
  >
    <v-row justify="center">
      <v-col
        cols="12"
        md="8"
      >
        <v-card>
          <v-card :elevation="8" color="#217BD4" class="mb-n6 pa-7" style="width: 98%; top: -30px; margin-left: 1%; margin-right: 1%;">
            <div style="color: #fff;" class="display-2 font-weight-light">
              Create Profile
            </div>

            <div style="color: rgb(255 255 255 / .87);" class="subtitle-1 font-weight-light">
              Complete your profile
            </div>
          </v-card>

          <v-form class="mt-2 ml-3 mr-3">
            <v-container class="py-0">
              <v-row>
                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Teacher Id"
                    v-model="formData.teacherId"
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="First Name"
                    v-model="formData.firstName"
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Last Name"
                    v-model="formData.lastName"
                  />
                </v-col>
                <v-col
                  cols="12"
                  md="1"
                >
                  <v-text-field
                    label="Middle name"
                    v-model="formData.middleName"
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="3"
                >
                  <v-menu
                    v-model="menuDate"
                    :close-on-content-click="false"
                    :nudge-right="40"
                    transition="scale-transition"
                    offset-y
                    min-width="290px"
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                        v-model="formData.dateOfBirth"
                        label="Date of Birth"
                        prepend-icon="mdi-calendar"
                        readonly
                        v-bind="attrs"
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker v-model="formData.dateOfBirth" @input="menuDate = false">
                       <v-spacer></v-spacer>
                      <v-btn text color="primary" @click="menuDate = false"><strong>Cancel</strong></v-btn>
                      <v-btn text color="primary" @click="menuDate = false"><strong>OK</strong></v-btn>
                    </v-date-picker>
                  </v-menu>
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Department"
                    v-model="formData.department"
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Grade / Admin / Principal"
                    v-model="formData.grade"
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Health Status"
                    v-model="formData.healthStatus"
                  />
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    label="Adress"
                    v-model="formData.address"
                  />
                </v-col>

                <v-col cols="12">
                  
                </v-col>

                <v-col
                  cols="12"
                  class="text-right"
                >
                  <v-btn
                    color="primary"
                    class="mb-3"
                  >
                    Save Profile
                  </v-btn>
                </v-col>
              </v-row>
            </v-container>
          </v-form>
        </v-card>
      </v-col>

      <v-col cols="12" md="4">
        <v-card>
          <v-row justify="center">
            <v-card :elevation="8">
              <v-avatar size="150" style="margin-top: -50px;">
                <v-img
                alt="Image Teacher"
                contain
                src="https://www.w3schools.com/howto/img_avatar.png"
                transition="scale-transition"
                
              />
              </v-avatar>
            </v-card>
          </v-row>
          <v-card-text class="text-center">
            <h6 class="display-1 mb-1 grey--text">
              {{ formData.department }}
            </h6>

            <h4 class="display-2 font-weight-light mb-3 black--text">
              {{ formData.firstName + ' ' + formData.middleName + ' ' + formData.lastName }}
            </h4>

            <p class="font-weight-light grey--text">
              {{ formData.grade }}
            </p>

            <v-btn
              color="primary"
              rounded
              class="mr-0"
            >
              Upload Photo
            </v-btn>
          </v-card-text>
        </v-card>

        <v-card class="mt-5">
          <v-row justify="center">
            <v-card :elevation="8" class="mt-5">
              <qrcode :value="formData.teacherId" :options="{ width: 200, margin: 2 }"></qrcode>
            </v-card>
          </v-row>
          <v-card-text class="text-center">
            <h6 class="display-1 mb-5 grey--text">
              {{ formData.teacherId }}
            </h6>

            <v-btn
              color="primary"
              rounded
              class="mr-0"
            >
              Save QR Code
            </v-btn>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  import '../../assets/css/vuetify_material.css'

  export default {
    //
    data() {
      return {
        date: new Date().toISOString().substr(0, 10),
        menuDate: false,
        formData: {
          id: 2,
          teacherId: "DepEd-6291611",
          firstName: "SHEENA MARIE",
          lastName: "SALVA",
          middleName: "C.",
          dateOfBirth: new Date().toISOString().substr(0, 10),
          address: "Guerrero, Bato, Leyte",
          healthStatus: "Fit to Work",
          department: "SHS Department",
          grade: "Grade 12-HUMSS Philosophy",
          image: ""
        }
      }
    }
  }
</script>
