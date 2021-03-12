#!/bin/bash

dotnet publish -c Release -o ../PublishConfig/Publish/linux-fd/ -r linux-x64 --self-contained false 
