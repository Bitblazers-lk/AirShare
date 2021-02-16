#!/bin/bash

dotnet publish -c Release -o ./PublishConfig/Publish/osx64-sc/ -r osx-x64 --self-contained true 
