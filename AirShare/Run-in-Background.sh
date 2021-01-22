#!/bin/bash

cpath = "$(dirname "$0")"
echo $cpath

cd "$(dirname "$0")"
screen -d -m dotnet watch run 
