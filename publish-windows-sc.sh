#!/bin/bash

dotnet publish -c Release -o ./PublishConfig/Publish/win64-sc/ -r win-x64 --self-contained true 
