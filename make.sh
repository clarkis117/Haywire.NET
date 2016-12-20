#!/bin/bash
./haywire/make.sh

dotnet restore */**/project.json
dotnet build */**/project.json