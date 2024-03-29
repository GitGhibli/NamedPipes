// NamedPipesCPP.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <fstream>
#include <Windows.h>
#include <string>

using namespace std;

struct MyStruct
{
	int MyNumber;

	int MyAnotherNumber;

	char MyText[255];

	char MyAnotherText[255];
};

int main()
{
	// ios::ate does not work. C# apparently writes into file after file is opened
	ifstream file{ "//./pipe/MyTestPipe"};
	if (file.is_open()) {
		
		MyStruct myStruct;

		while (true) {
			file.readsome((char*)(&myStruct), 518);
			
			if (file.eof()) {
				return 0;
			}

			cout << myStruct.MyNumber << '\n';
			cout << myStruct.MyAnotherNumber << '\n';
			cout << myStruct.MyText << '\n';
			cout << myStruct.MyAnotherText << '\n';
		}
		
		}
	else {
		cout << "File is not open";
	}
}