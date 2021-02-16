#!/bin/bash

cp -r linux-files/* AirShareRelease/
cd AirShareRelease/

git update-index --add --chmod=+x *.sh
git update-index --add --chmod=+x scripts/*.sh
git add .
