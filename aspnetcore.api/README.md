# CASNApp.API - ASP.NET Core 2.0 Server

This is a test CASN API

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
cd CASNApp.API/CASNApp.API
docker build -t casnapp.api .
docker run -p 5000:5000 casnapp.api
```
