#!/bin/bash

mkdir -p sand
cd sand

scrot -z -p -q 40 '%Y-%m-%d.%H-%M.screen.jpg'
scrot -z -k -q 10 '%Y-%m-%d.%H-%M.windows.jpg'
echo Screenshots saved 
