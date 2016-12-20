#!/bin/bash
cd ./haywire
./make.sh

cd ../
dotnet restore */**/project.json
dotnet build */**/project.json