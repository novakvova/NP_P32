#!/bin/bash

set -e

server_up() {
    echo "Server up..."
    docker pull novakvova/p22-asp-api:latest
    docker stop p22-asp_container || true
    docker rm p22-asp_container || true
    docker run -d --restart=always -v /volumes/p22-asp/images:/app/images --name p22-asp_container -p 4221:8080 novakvova/p22-asp-api
}

start_containers() {
    echo "Containers start..."
    docker run -d --restart=always -v /volumes/p22-asp/images:/app/images --name p22-asp_container -p 4221:8080 novakvova/p22-asp-api
}

stop_containers() {
    echo "Containers stop..."
    docker stop p22-asp_container || true
    docker rm p22-asp_container || true
}

restart_containers() {
    echo "Containers restart..."
    docker stop p22-asp_container || true
    docker rm p22-asp_container || true
    docker run -d --restart=always -v /volumes/p22-asp/images:/app/images --name p22-asp_container -p 4221:8080 novakvova/p22-asp-api
}

echo "Choose action:"
echo "1. Server up"
echo "2. Containers start"
echo "3. Containers stop"
echo "4. Containers restart"
read -p "Enter action number: " action

case $action in
    1)
        server_up
        ;;
    2)
        start_containers
        ;;
    3)
        stop_containers
        ;;
    4)
        restart_containers
        ;;
    *)
        echo "Invalid action number!"
        exit 1
        ;;
esac
