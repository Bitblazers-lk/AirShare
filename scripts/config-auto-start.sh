#!/bin/bash

cd "$(dirname "$0")"
cd ..

spscr="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"
sp="$(dirname "$spscr")""/scripts/Run-in-Background.sh"

echo Autostart script is at 
echo $sp

mkdir -p ~/.config/autostart

echo -e "[Desktop Entry]\nName=sysmon\nExec=$sp\nType=Application\nX-GNOME-Autostart-enabled=true" > ~/.config/autostart/sysmon.desktop

echo 
echo Autostart configured
echo
cat ~/.config/autostart/sysmon.desktop
echo
echo Succesful
