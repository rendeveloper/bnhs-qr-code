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
              {{ formData.id === 0 ? 'Create' : 'Edit' }} Profile
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
                    :error-messages="errorMessages"
                    ref="teacherId"
                    :rules="[
                      value => !!formData.teacherId || 'Required.',
                      value => (formData.teacherId && formData.teacherId.length >= 3) || 'Min 3 characters',
                      value => checkTeacherId()
                    ]"
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
                    required
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
                        ref="dateOfBirth"
                        :rules="[
                          value => !!formData.dateOfBirth || 'Required.'
                        ]"
                        required
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
                    ref="department"
                    :rules="[
                      value => !!formData.department || 'Required.',
                      value => (formData.department && formData.department.length >= 3) || 'Min 3 characters'
                    ]"
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
                    class="mb-3 mr-3"
                    :to="'/user/all'"
                  >
                    <!-- <v-icon class="mr-1">mdi-content-save</v-icon> -->
                    Cancel
                  </v-btn>
                  <v-btn
                    color="primary"
                    class="mb-3"
                    :loading="loading"
                    :disabled="loading"
                    @click="onSubmit"
                  >
                    <!-- <v-icon class="mr-1">mdi-content-save</v-icon> -->
                    {{ formData.id === 0 ? 'Save' : 'Update' }} Profile
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
            <input type="file" id="file-id" ref="file" @change="onFileChange" accept="image/*" style="display: none">  <!--  -->
            <v-btn
              color="primary"
              rounded
              class="mr-0"
              @click="$refs.file.click()"
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
        teacherRules: [
          value => !!value || 'Required.',
          value => (value && value.length >= 3) || 'Min 3 characters'
        ],
        formHasErrors: false,
        errorMessages: ''
      }
    },
    created(){
      this.get(this.$route.params.id)
    },
    methods: {
      ...mapActions(['saveUser', 'getUser', 'updateUser', 'getScanQRCode', 'saveImageDetails']),
      async get(id){
        var self = this
        if(id == undefined || id == 0){ 
          return;
        }
        
        self.screenLoading = true
        await self.getUser(id).then(response => {
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
      async onSubmit(){
        var self = this
        self.formHasErrors = false
        self.errorMessages = ''
        
        Object.keys(self.formData).forEach(f => {
          if(f !== 'id' && f !== 'image' && f !== 'imageDto'){
            if (!self.formData[f]) self.formHasErrors = true
            
            self.$refs[f].validate(true)
          }
        })
        if(self.formHasErrors){
          return;
        }
        self.loading = true
        
        var resetForm = function(){
            setTimeout(() =>{
              self.$message({
                message: "The profile was saved successfully",
                type: "success"
              })
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
              }
              
              self.$router.push('/user/all')
              self.loading = false
            }, 500)
        }
        if(self.formData.id === 0){
          var chkTeacherId = {
            teacherId: self.formData.teacherId
          }
          await self.getScanQRCode(chkTeacherId).then(response => {
            if(response.status === 200 && response.data){
              self.errorMessages = 'Teacher id already exists'
              self.loading = false
            }
          }).catch(() => {
            
          })
          if(self.errorMessages !== ''){
            return;
          }

          await self.saveUser(self.formData).then(async(response) => {
              var payload = {
                id: self.formData.imageDto.id,
                fileName: self.formData.imageDto.fileName,
                originalFileName: self.formData.imageDto.originalFileName,
                fileSize: self.formData.imageDto.fileSize,
                uploadDate:self.formData.imageDto.uploadDate,
                UserProfileId: response.data.id
              }
              if(response.data.image === "" && self.formData.imageDto.fileSize === 0){
                  resetForm();
              }else{
                await self.saveImageDetails(payload).then(x => {
                  resetForm();
                })
              }
          }).catch(error => {
            self.$message({
              message: "An unexpected error occurred",
              type: "error"
            })
          })
        }else{
          await self.updateUser(self.formData).then(async(response) => {
            var payload = {
              id: self.formData.imageDto.id,
              fileName: self.formData.imageDto.fileName,
              originalFileName: self.formData.imageDto.originalFileName,
              fileSize: self.formData.imageDto.fileSize,
              uploadDate:self.formData.imageDto.uploadDate,
              UserProfileId: response.data.id
            }
            if(response.data.image === "" && self.formData.imageDto.fileSize === 0){
                resetForm();
            }else{
              await self.saveImageDetails(payload).then(x => {
                resetForm();
              })
            }
          }).catch(error => {
            self.$message({
              message: "An unexpected error occurred",
              type: "error"
            })
          })
        }
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
      async onFileChange(e){
        var files = e.target.files || e.dataTransfer.files;
        var self = this;

        if (!files.length)
          return;

        let customJsonFiles = await this.getFiles(files);

        //self.formData.image = customJsonFiles.bytes
        self.formData.image = 'data:image/png;base64,' +  customJsonFiles.base64String

        //self.imageBase = 'data:image/png;base64,' +  customJsonFiles.base64String
        self.formData.imageDto = {
          id: 0,
          fileName: customJsonFiles.fileName + '.' + customJsonFiles.originalFileName.split('.').pop(),
          originalFileName: customJsonFiles.originalFileName,
          fileSize: customJsonFiles.fileSize,
          uploadDate: customJsonFiles.uploadDate,
          UserProfileId: 0,
          dataBytes: customJsonFiles.bytes
        }
      },
      getFiles(files) {
          return this.getFile(files[0]);
      },
      getFile(file) {
        var reader = new FileReader();
        return new Promise((resolve, reject) => {
            reader.onerror = () => { reader.abort(); reject(new Error("Error parsing file"));}
            reader.onload = function () {

                //This will result in an array that will be recognized by C#.NET WebApi as a byte[]
                let bytes = Array.from(new Uint8Array(this.result));

                //if you want the base64encoded file you would use the below line:
                let base64String = btoa(bytes.map((item) => String.fromCharCode(item)).join(""));
                //let base64String = this.result

                //Hash CheckSum MD5
                var wordArray = CryptoJS.lib.WordArray.create(this.result + new Date().toString());
                var hash = CryptoJS.MD5(wordArray).toString();

                //Resolve the promise with your custom file structure
                resolve({
                    bytes: bytes,
                    base64String: base64String,
                    originalFileName: file.name,
                    fileName: hash,
                    fileType: file.type,
                    fileSize: file.size,
                    uploadDate: new Date()
                });
            }
            reader.readAsArrayBuffer(file);
            //reader.readAsDataURL(file);
        });
      },
      checkTeacherId(){
        this.errorMessages = ''
        return true
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
