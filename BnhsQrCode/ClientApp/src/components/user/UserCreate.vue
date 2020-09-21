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
                    label="Grade / Admin / Principal / Role"
                    v-model="formData.role"
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

                <v-col cols="12">
                                  
                </v-col>
                <v-col
                  cols="12"
                  class="text-right"
                >
                  <v-btn
                    color="primary"
                    class="mb-3"
                    :loading="loading"
                    :disabled="loading"
                    @click="onSubmit"
                  >
                    <!-- <v-icon class="mr-1">mdi-content-save</v-icon> -->
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
                :src="imageBase"
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
              :disabled="formData.teacherId == '' && formData.firstName == '' && formData.lastName == ''"
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
          image: []
        },
        imageBase: require("../../assets/image/img_avatar.png")
      }
    },
    methods: {
      ...mapActions(['saveUser']),
      async onSubmit(){
        var self = this
        self.loading = true

        await self.saveUser(self.formData).then(response => {
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
              image: []
            }
            self.imageBase = require("../../assets/image/img_avatar.png")
            self.$router.push('/user/all')
            self.loading = false
          }, 1000)
        }).catch(error => {

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
      async onFileChange(e){
        var files = e.target.files || e.dataTransfer.files;
        var self = this;

        if (!files.length)
          return;

        let customJsonFiles = await this.getFiles(files);
        
        self.formData.image = customJsonFiles.bytes
        self.imageBase = 'data:image/png;base64,' +  customJsonFiles.base64String
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
      }
    }
  }
</script>
