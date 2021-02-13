#!/bin/bash


cd "$(dirname "$0")"

git reset --hard 
git switch Release
git pull origin Release


git status

echo 
echo "------------------"
echo "Updating Completed"
echo "------------------"
echo 
