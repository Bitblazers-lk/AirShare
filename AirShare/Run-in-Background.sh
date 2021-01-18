#!/bin/bash

cd "$(dirname "$0")"
screen -d -m dotnet watch run 
