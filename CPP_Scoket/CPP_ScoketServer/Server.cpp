#include<winsock2.h> //�������� ��� ������ � ������� � Windows
#include<iostream>
#pragma comment(lib, "ws2_32.lib") //˳�������� �������� ������

int main() 
{
	WSADATA wsaData;
	WSAStartup(MAKEWORD(2,2), &wsaData); //����������� WinSock

	SOCKET server = socket(AF_INET, SOCK_STREAM, 0); //IP v4 TCP ��������
	
	sockaddr_in serverAddress{};
	serverAddress.sin_family = AF_INET; //������� IP v4
	serverAddress.sin_port = htons(8930); //����� ���� ��� ������
	serverAddress.sin_addr.s_addr = INADDR_ANY; //������� IP 0.0.0.0

	//�������� ������ �� IP � �����
	bind(server, (sockaddr*)&serverAddress, sizeof(serverAddress));
	//ʳ������ �볺��� 4 �����
	listen(server, SOMAXCONN);

	std::cout << "Waiting for connection ...\n";

	SOCKET clientSocket;
	sockaddr_in clientAddres;
	int clientSize = sizeof(clientAddres);
	//��� ������ ��������� ������ �� �볺���
	clientSocket = accept(server, (sockaddr*)&clientAddres, &clientSize);

	char buffer[1024]; //char - 1 ����
	//�������� ��� ����� � ��������� � �������� �� � buffer
	int bytesRec = recv(clientSocket, buffer, sizeof(buffer), 0);
	if (bytesRec > 0) {
		buffer[bytesRec] = '\0'; //����� ����� �����
		std::cout << "Received: " << buffer << "\n"; //�������� �����������
		char str[] = "Server Baraban Salo - Kovbasa :)";
		send(clientSocket, str, sizeof(str), 0);
	}
	closesocket(clientSocket);
	closesocket(server);
	WSACleanup(); //
	return 0;
}