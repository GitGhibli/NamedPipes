// NamedPipesCPP.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <fstream>
#include <Windows.h>

using namespace std;

struct MyStruct
{
	int MyNumber;
	int MyAnotherNumber;
};

int main()
{
	/*HANDLE hPipe;
	char buffer[1024];

	HANDLE lpBuffer;
	LPDWORD bytesRead;
	LPOVERLAPPED isOverlapped;

	DWORD dwRead;

	hPipe = CreateFile(TEXT("\\\\.\\pipe\MyTestPipe"), GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

	if (hPipe != INVALID_HANDLE_VALUE) {
		ReadFile(hPipe, lpBuffer, 1024, bytesRead);
	}*/

	ifstream file{"//./pipe/MyTestPipe"}; // ios::ate does not work. C# apparently writes into file after file is opened
	if (file.is_open()) {
		file.seekg(0, ios::end);
		int size = file.tellg();
		file.seekg(0);

		MyStruct myStruct;

		file.read((char*)(&myStruct), size);

		cout << myStruct.MyNumber << '\n';
		cout << myStruct.MyAnotherNumber << '\n';
	}
	else {
		cout << "File is not open";
	}

	}