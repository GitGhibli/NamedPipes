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
	ifstream file{ "//./pipe/MyTestPipe" }; // ios::ate does not work. C# apparently writes into file after file is opened
	if (file.is_open()) {
		
		int size = 0;
		while (size == 0) {
			file.seekg(0, ios::end);
			size = file.tellg();
		}

		file.seekg(0, ios::beg);

		MyStruct myStruct;

		file.read((char*)(&myStruct), size);
		
		cout << myStruct.MyNumber << '\n';
		cout << myStruct.MyAnotherNumber << '\n';
		cout << myStruct.MyText << '\n';
		cout << myStruct.MyAnotherText << '\n';

		//char text[263];
		//file.read(text, size);
		//cout << text;
	}
	else {
		cout << "File is not open";
	}
}