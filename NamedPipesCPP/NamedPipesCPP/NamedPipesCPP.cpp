// NamedPipesCPP.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <fstream>
#include <Windows.h>

using namespace std;

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

	ifstream file{"//./pipe/MyTestPipe"};

	if (file.is_open()) {
		char line[100];
		file.read(line, 100);
		cout << line << '\n';
	}
	else {
		cout << "File is not open";
	}

	}