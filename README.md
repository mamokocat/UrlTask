# TinyUrl

## Project setup
```
npm install
```

install [chocolatey](https://chocolatey.org/install#individual)

install [mkcert](https://github.com/FiloSottile/mkcert) by using commands below (with admin rights):
```
choco install mkcert
mkcert -install
```

in the project root folder run these commands:
```
mkdir -p .certs
mkcert -key-file ./.certs/key.pem -cert-file ./.certs/cert.pem "localhost"
```


### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```
