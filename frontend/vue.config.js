const { defineConfig } = require('@vue/cli-service')
const fs = require('fs');


module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    https: {
      key: fs.readFileSync('.certs/key.pem'),
      cert: fs.readFileSync('.certs/cert.pem'),
    }
  }
})
