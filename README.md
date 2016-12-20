# Haywire.NET

Haywire bindings and request server for ASP.NET Core

Currently this only works on Linux (specifically the distros that .NET Core supports) and the Windows Subsystem for Linux

###Dependencies (Same as Haywire plus .NET Core)
```
apt-get install git gcc make cmake automake autoconf libssl-dev libtool
```
Follow the instructions at http://dot.net for installing .NET Core for your linux distro. The current version of the of the .NET Core SDK used by the project is:
```
  dotnet-dev-1.0.0-preview2.1-003177
```


##Setup
Check out this git repo with the haywire submodule. This submodule is needed to produce the shared libraries need for pinvoke in .NET.
```
git clone --recursive https://github.com/clarkis117/Haywire.NET.git
```

##Compiling on Linux - run make.sh in the main directory of Haywire.NET
```
./make.sh
```

##Hello World – Default URL: http://localhost:8000
```
cd ./src/HelloWorld
dotnet run --configuration release
```

###Benchmarking 
Same instructions as Haywire, just be aware that the the Hello World example pulls it's configuration from a json file, config.json. In addition, please make sure you're running and building the Hello World program in release mode otherwise perf results may vary.

#### config.json defaults
```
{
  "HttpListenAddress": "localhost",
  "HttpListenPort": 8000,
  "ThreadCount": 0,
  "Balancer": "ipc",
  "Parser": "http_parser",
  "TcpNoDelay": false,
  "ListenBackLog": 0,
  "MaxRequestSize": 1048576
}
```
