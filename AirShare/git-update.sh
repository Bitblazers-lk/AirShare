#!/bin/bash

git reset --hard 
git switch Release
git pull origin Release


git status

echo 
echo "------------------"
echo "Updating Completed"
echo "------------------"
echo 
