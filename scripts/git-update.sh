#!/bin/bash


cd "$(dirname "$0")"
cd ..

git reset --hard 
# git switch Release
git config pull.rebase false
git pull origin 


git status

echo 
echo "------------------"
echo "Updating Completed"
echo "------------------"
echo 
