#!/bin/bash

platform=$1

framework=sc
target=$platform-$framework

dotnet publish -c Release -o ../AirShareRelease/Publish/$target/ -r $platform --self-contained true

cd ../AirShareRelease/Publish/$target
zip -r -9 ../../zip/$target.zip *
