#!/bin/bash

echo -e "Hi $USER"
echo "I am the sample output"


spscr="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"
sp="$(dirname "$spscr")"

echo Current Application Directory is at 
echo $sp
