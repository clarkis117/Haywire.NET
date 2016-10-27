# Haywire.NET

Haywire bindings and request server for ASP.NET Core

Currently this only works on Linux (specifically the distros that .NET Core supports) and the Windows Subsystem for Linux

###Dependencies (Same as Haywire plus .NET Core)
```
apt-get install git gcc make cmake automake autoconf libssl-dev libtool
```
Follow the instructions at http://dot.net for installing .NET Core for your linux distro

##Setup
Check out this git repo with the haywire submodule. This submodule is needed to produce the shared libraries need for pinvoke in .NET.
```
git clone --recursive https://github.com/clarkis117/Haywire.NET.git
```

##Compiling on Linux - run make.sh in the main directory of Haywire.NET
```
./make.sh
```

##Hello World – Default URL: http://127.0.0.1:8800
```
cd ./HelloWorld
dotnet run
```

###Benchmarking 
Same instructions as Haywire, just be aware that the defaults for the HelloWorld program are different
