module.exports = {
  chainWebpack: config => {
    config.module
      .rule('sass')
      .test(/\.sass$/)
      .use('sass-loader')
        .loader('sass-loader')
        .loader('css-loader')
        .loader('style-loader')
      .end()
  },
  configureWebpack:{
      performance: {
        hints: false
      },
      optimization: {
        splitChunks: {
          minSize: 10000,
          maxSize: 250000,
        }
      }
  },
  css: {
    // Enable CSS source maps.
    sourceMap: true
  },
  "transpileDependencies": [
    "vuetify"
  ]
}