// Fill out your copyright notice in the Description page of Project Settings.

#include "NamedPipesReaderActorComponent.h"
#include <fstream>
#include <thread>

void Foo() {
	while (true) {

	}
}

UNamedPipesReaderActorComponent::UNamedPipesReaderActorComponent()
{
	PrimaryComponentTick.bCanEverTick = true;
	//std::thread t(Foo);
}

void UNamedPipesReaderActorComponent::BeginPlay()
{	
	Super::BeginPlay();
}

void UNamedPipesReaderActorComponent::Foo() {
	return;
}

void UNamedPipesReaderActorComponent::ConnectToNamedPipe() {
	File.open("//./pipe/MyTestPipe"); // ios::ate does not work. C# apparently writes into file after file is opened
}

struct MyStruct
{
	int MyNumber;

	int MyAnotherNumber;

	char MyText[255];

	char MyAnotherText[255];
};

void UNamedPipesReaderActorComponent::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);

	if (File.is_open()) {

		struct MyStruct myStruct;
		
		File.read((char*)(&myStruct), 518);

		onFoo.Broadcast();
	}
}

