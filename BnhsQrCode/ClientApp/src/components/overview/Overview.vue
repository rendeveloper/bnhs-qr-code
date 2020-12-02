<template>
  <v-container
    id="user-profile"
    tag="section"
    style="margin-top: 100px;"
    v-loading="screenLoading"
  >
    <v-row justify="center">
      <v-col
        cols="12"
        md="8"
      >
        <v-card>
          <v-card :elevation="8" color="#217BD4" class="mb-n6 pa-7" style="width: 98%; top: -30px; margin-left: 1%; margin-right: 1%;">
            <div style="color: #fff;" class="display-2 font-weight-light">
              Personal info
            </div>

            <div style="color: rgb(255 255 255 / .87);" class="subtitle-1 font-weight-light">
               Personal information that cannot be viewed by other users    
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
                    :error-messages="errorMessages"
                    ref="teacherId"
                    :rules="[
                      value => !!formData.teacherId || 'Required.',
                      value => (formData.teacherId && formData.teacherId.length >= 3) || 'Min 3 characters',
                      value => checkTeacherId()
                    ]"
                    readonly
                    required
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
                    ref="firstName"
                    :rules="[
                      value => !!formData.firstName || 'Required.',
                      value => (formData.firstName && formData.firstName.length >= 3) || 'Min 3 characters'
                    ]"
                    readonly
                    required
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Last Name"
                    v-model="formData.lastName"
                    ref="lastName"
                    :rules="[
                      value => !!formData.lastName || 'Required.',
                      value => (formData.lastName && formData.lastName.length >= 3) || 'Min 3 characters'
                    ]"
                    readonly
                    required
                  />
                </v-col>
                <v-col
                  cols="12"
                  md="1"
                >
                  <v-text-field
                    label="Middle name"
                    v-model="formData.middleName"
                    ref="middleName"
                    :rules="[
                      value => !!formData.middleName || 'Required.'
                    ]"
                    readonly
                    required
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="3"
                >
                
                    <v-text-field
                    v-model="formData.dateOfBirth"
                    label="Date of Birth"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                    ref="dateOfBirth"
                    :rules="[
                        value => !!formData.dateOfBirth || 'Required.'
                    ]"
                    required
                    ></v-text-field>
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Department"
                    v-model="formData.department"
                    ref="department"
                    :rules="[
                      value => !!formData.department || 'Required.',
                      value => (formData.department && formData.department.length >= 3) || 'Min 3 characters'
                    ]"
                    readonly
                    required
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Grade / Admin / Principal / Role"
                    v-model="formData.role"
                    ref="role"
                    :rules="[
                      value => !!formData.role || 'Required.',
                      value => (formData.role && formData.role.length >= 3) || 'Min 3 characters'
                    ]"
                    readonly
                    required
                  />
                </v-col>

                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    label="Health Status"
                    v-model="formData.healthStatus"
                    ref="healthStatus"
                    :rules="[
                      value => !!formData.healthStatus || 'Required.',
                      value => (formData.healthStatus && formData.healthStatus.length >= 3) || 'Min 3 characters'
                    ]"
                    readonly
                    required
                  />
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    label="Adress"
                    v-model="formData.address"
                    ref="address"
                    :rules="[
                      value => !!formData.address || 'Required.',
                      value => (formData.address && formData.address.length >= 3) || 'Min 3 characters'
                    ]"
                    readonly
                    required
                  />
                </v-col>

                <v-col cols="12">
                  
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
                        @click="LogoutUser()"
                    >
                    <v-icon class="mr-1">mdi-logout</v-icon>
                        Logout
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
                :src="formData.image"
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
              {{ (formData.firstName + ' ' + formData.middleName + ' ' + formData.lastName).toUpperCase()  }}
            </h4>

            <p class="font-weight-light grey--text">
              {{ formData.role }}
            </p>
            <v-btn
              color="primary"
              rounded
              class="mr-0"
              disabled
            >
              Upload Photo
            </v-btn>
          </v-card-text>
        </v-card>

        <v-card class="mt-5">
          <v-row justify="center">
            <v-card :elevation="8" class="mt-5">
              <qrcode :value="formData.teacherId || null" :options="{ width: 200, margin: 2 }"></qrcode>
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
              :disabled="formData.teacherId === '' || formData.firstName === '' || formData.lastName === ''"
              @click="SAVE_QR_CODE"
            >
              Download QR Code
            </v-btn>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  import { mapState, mapActions } from 'vuex'
  import '../../assets/css/vuetify_material.css'
  var QRCode = require('qrcode')
  var FileSaver = require('file-saver');
  var CryptoJS = require("crypto-js");

  export default {
    //
    data() {
      return {
        date: new Date().toISOString().substr(0, 10),
        loading: false,
        screenLoading: false,
        menuDate: false,
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
          image: require("../../assets/image/img_avatar.png"),
          imageDto: {
            id: 0,
            fileName: "",
            originalFileName: "",
            fileSize: 0,
            uploadDate: new Date(),
            UserProfileId: 0,
            dataBytes: []
          }
        },
        errorMessages: ''
      }
    },
    created(){
      this.get()
    },
    methods: {
      ...mapActions(['getUser', 'logout']),
      async get(){
        var self = this
        let user = JSON.parse(localStorage.getItem('user'));
        if(user === null){ 
          return;
        }
        
        self.screenLoading = true
        await self.getUser(user.id).then(response => {
          if(response.status === 200){
            self.formData.id = response.data.id
            self.formData.teacherId = response.data.teacherId
            self.formData.firstName = response.data.firstName
            self.formData.lastName = response.data.lastName
            self.formData.middleName = response.data.middleName
            self.formData.dateOfBirth = new Date(response.data.dateOfBirth).toISOString().substring(0,10);
            self.formData.address = response.data.address
            self.formData.healthStatus = response.data.healthStatus
            self.formData.department = response.data.department
            self.formData.role = response.data.role
           
            self.formData.image = response.data.image === "" ? require("../../assets/image/img_avatar.png") : '/images/' + response.data.image
          }

          setTimeout(() => {
            self.screenLoading = false
          }, 500)
        }).catch(error => {
          self.$message({
            message: "An unexpected error occurred",
            type: "error"
          })
        })
      },
      SAVE_QR_CODE(){
        var self = this
        var opts = {
          width: 300, 
          margin: 2
        }

        QRCode.toDataURL(self.formData.teacherId, opts, function (err, url) {
          if (err) throw err
          
          FileSaver.saveAs(url, self.formData.teacherId + '-' + self.formData.lastName +".png");
        })
      },
      checkTeacherId(){
        this.errorMessages = ''
        return true
      },
      LogoutUser(){
        this.logout()
        this.$router.push('/login')
      }
    }
  }
</script>
<style>
  .el-loading-mask{
    margin: -50px !important;
    background-color: rgba(238,238,238,.9) !important;
    z-index: auto !important;
  }
</style>
