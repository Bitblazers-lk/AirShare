#!/bin/bash

rm -rf ../AirShareRelease/Publish
rm -rf ../AirShareRelease/zip

mkdir -p ../AirShareRelease/Publish
mkdir -p ../AirShareRelease/zip

bash publish-fd.sh linux-x64
bash publish-fd.sh linux-musl-x64
bash publish-fd.sh linux-arm
bash publish-fd.sh linux-arm64

bash publish-fd.sh win-x64
bash publish-fd.sh win-x86
bash publish-fd.sh win-arm
bash publish-fd.sh win-arm64

bash publish-fd.sh osx-x64
bash publish-fd.sh osx.12-x64
bash publish-fd.sh osx.12-arm64


bash publish-sc.sh linux-x64
bash publish-sc.sh linux-musl-x64
bash publish-sc.sh linux-arm
bash publish-sc.sh linux-arm64

bash publish-sc.sh win-x64
bash publish-sc.sh win-x86
bash publish-sc.sh win-arm
bash publish-sc.sh win-arm64

bash publish-sc.sh osx-x64
bash publish-sc.sh osx.12-x64
bash publish-sc.sh osx.12-arm64

