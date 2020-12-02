<template> 
  <v-container id="user-login" fluid tag="section" style="margin-top: 100px;">
    <v-row justify="center">
      <v-col
        cols="12"
        md="3"
      >
        <v-card>
          <v-card :elevation="8" color="#217BD4" class="mb-n6 pa-4" style="width: 96%; top: -33px; margin-left: 2%; margin-right: 2%;">
            <div style="color: #fff;" class="display-2 font-weight-light">
               Login
            </div>

            <div style="color: rgb(255 255 255 / .87);" class="subtitle-1 font-weight-light">
              Please enter a valid teacher id
            </div>
          </v-card>

          <v-form class="mt-2 ml-3 mr-3">
            <v-container class="py-0">
              <v-row>
                <v-col
                  cols="12"
                  md="12"
                >
                  <v-text-field
                    label="Teacher Id"
                    ref="teacherId"
                    prepend-icon="mdi-lock"
                    v-model="formData.teacherId"
                    :rules="[
                      value => !!formData.teacherId || 'Required.',
                      value => (formData.teacherId && formData.teacherId.length >= 3) || 'Min 3 characters'
                    ]"
                    required
                  />
                </v-col>
              </v-row>
               <v-row>
                <v-col
                    cols="12"
                    md="12"
                >
                  <v-btn
                    block
                    color="primary"
                    class="mb-3"
                    :loading="loading"
                    @click="onSubmit"
                  >
                    <!--  -->
                    <!-- <v-icon class="mr-1">mdi-content-save</v-icon> -->
                    Login
                  </v-btn>
                </v-col>
              </v-row>
            </v-container>
          </v-form>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  import { mapState, mapActions } from 'vuex'
  import '../../assets/css/vuetify_material.css'
  export default {
    name: 'LoginPage',
    data() {
      return {   
          loading: false,
          formData: {
              teacherId: "",
          }
      }
    },
    created(){
        this.logout();
    },
    computed: {
    },
    watch: {
    },
    methods: {
      ...mapActions(['login', 'logout']),
      async onSubmit(){
          var self = this
          self.loading = true
          var scanQRCode = {
            teacherId: self.formData.teacherId
          }

          await self.login(scanQRCode).then(response => {
            if(response.status === 200){
                setTimeout(() => {
                    // display success message after route change completes
                    self.$message({
                        type: 'success',
                        message: "You've successfully logged in!"
                    });
                })
                let user = JSON.parse(localStorage.getItem('user'));
                self.loading = false;

                if(user !== null && user.admin){
                    self.$router.push('/')
                }else{
                    self.$router.push('/overview')
                }
            }
          }).catch(r => {
            self.loading = false;
            if(r.response.data !== null && r.response.data.message.includes("Couldn't")){
                self.$message({
                    message: r.response.data.message,
                    type: "error"
                })
            }
            else if(r.response.data !== null && r.response.data.message.includes("Sorry,")){
                self.$message({
                    message: r.response.data.message,
                    type: "error"
                })
            }else{
                self.$message({
                    message: "An unexpected error occurred",
                    type: "error"
                })
            }
          })
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