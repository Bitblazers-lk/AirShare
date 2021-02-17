#!/bin/bash

echo "commit name '$1' "

cd AirShareRelease
git reset --hard
cd ..

./copy-linux-fd.sh      

cd AirShareRelease
git add .
git commit -m "$1-auto"
cd ..
 
./copy-linux-sc.sh     

cd AirShareRelease
git add .
git commit -m "$1-auto"
cd ..
  
./copy-osx-sc.sh     

cd AirShareRelease
git add .
git commit -m "$1-auto"
cd ..
    
./copy-win64-fd.sh  

cd AirShareRelease
git add .
git commit -m "$1-auto"
cd ..
     
./copy-win64-sc.sh  

cd AirShareRelease
git add .
git commit -m "$1-auto"
cd ..
