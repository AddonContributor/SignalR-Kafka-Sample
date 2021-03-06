#!/bin/bash
docker stop $(docker ps -a -q) \
   && docker container prune -f  \
   && docker image prune -f --filter "until=4h" \
   && docker network prune -f \
   && docker volume prune -f
  
   