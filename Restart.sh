#!/bin/bash

killall screen

cd "$(dirname "$0")"
screen -d -m dotnet run 
