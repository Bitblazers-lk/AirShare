#!/bin/bash

dotnet publish -c Release -o ./PublishConfig/Publish/win64-fd/ -r win-x64 --self-contained false 
