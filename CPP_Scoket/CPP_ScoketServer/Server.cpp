#include<winsock2.h> //бібліотека для роботи з сокетом у Windows
#include<iostream>
#pragma comment(lib, "ws2_32.lib") //Лінкування бібліоткеи сокетів

int main() 
{
	WSADATA wsaData;
	WSAStartup(MAKEWORD(2,2), &wsaData); //Ініціалізація WinSock

	SOCKET server = socket(AF_INET, SOCK_STREAM, 0); //IP v4 TCP протокол
	
	sockaddr_in serverAddress{};
	serverAddress.sin_family = AF_INET; //Вказуємо IP v4
	serverAddress.sin_port = htons(8930); //Вкаємо порт для сервер
	serverAddress.sin_addr.s_addr = INADDR_ANY; //Вкажумає IP 0.0.0.0

	//привзяка сокета до IP і порта
	bind(server, (sockaddr*)&serverAddress, sizeof(serverAddress));
	//Кількість клієнтів 4 байти
	listen(server, SOMAXCONN);

	std::cout << "Waiting for connection ...\n";

	SOCKET clientSocket;
	sockaddr_in clientAddres;
	int clientSize = sizeof(clientAddres);
	//Тут будемо очікувати запитів від клієнтів
	clientSocket = accept(server, (sockaddr*)&clientAddres, &clientSize);

	char buffer[1024]; //char - 1 байт
	//Отримуємо самі байти у повідомлені і записуємо їх у buffer
	int bytesRec = recv(clientSocket, buffer, sizeof(buffer), 0);
	if (bytesRec > 0) {
		buffer[bytesRec] = '\0'; //вказує кінець рядка
		std::cout << "Received: " << buffer << "\n"; //виводимо повідомлення
		char str[] = "Server Baraban Salo - Kovbasa :)";
		send(clientSocket, str, sizeof(str), 0);
	}
	closesocket(clientSocket);
	closesocket(server);
	WSACleanup(); //
	return 0;
}