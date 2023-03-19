#!/bin/bash

platform=$1

framework=fd
target=$platform-$framework

dotnet publish -c Release -o ../AirShareRelease/Publish/$target/ -r $platform --self-contained false

cd ../AirShareRelease/Publish/$target
zip -r -9 ../../zip/$target.zip *
