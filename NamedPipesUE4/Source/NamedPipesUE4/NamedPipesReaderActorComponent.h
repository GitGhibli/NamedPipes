// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include <fstream>
#include "CoreMinimal.h"
#include "Components/ActorComponent.h"
#include "NamedPipesReaderActorComponent.generated.h"

USTRUCT(BlueprintType)
struct FMyStruct
{
	GENERATED_BODY()

	UPROPERTY(BlueprintReadWrite)
	int MyNumber;

	UPROPERTY(BlueprintReadWrite)
	int MyAnotherNumber;

	UPROPERTY(BlueprintReadWrite)
	FString MyText;

	UPROPERTY(BlueprintReadWrite)
	FString MyAnotherText;
};

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class NAMEDPIPESUE4_API UNamedPipesReaderActorComponent : public UActorComponent
{
	GENERATED_BODY()

public:	
	// Sets default values for this component's properties
	UNamedPipesReaderActorComponent();

protected:
	// Called when the game starts
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

	DECLARE_DYNAMIC_MULTICAST_DELEGATE(FooDelegate);
	void Foo();

	UPROPERTY(BlueprintAssignable)
	FooDelegate onFoo;

	UFUNCTION(BlueprintCallable)
	void ConnectToNamedPipe();

	UPROPERTY(BlueprintReadWrite)
	FMyStruct MyStruct;

private:
	std::ifstream File;
};
