#!/bin/bash

if [ -n "$1" ]
then
  echo 'Starting building Docker image. Image name: "service2"'
  fullPath=$(realpath $1)
  docker build -f Web/Dockerfile -t service2 --build-arg SERVICE_DIRECTORY_PATH=$fullPath /
else
  echo 'The output directory path argument is mandatory.'
fi