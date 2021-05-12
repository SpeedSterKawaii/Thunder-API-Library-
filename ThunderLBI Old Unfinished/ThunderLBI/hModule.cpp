#include "pch.h"
#include <Windows.h>
#include <string>
#include <iostream>
#include <Psapi.h>
#include <Wininet.h>
#include "hAddresses.h"
using namespace std;
#pragma warning(disable : 4996)
bool AutoUpdating = false;
bool UsingConsole = true;

void LBIThread()
{
	Sleep(1000);
	CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)pipes::luapipename, NULL, NULL, NULL);
}

BOOL APIENTRY DllMain(HMODULE Module, DWORD Reason, void* Reserved)
{
	switch (Reason)
	{
	case DLL_PROCESS_ATTACH:
		DisableThreadLibraryCalls(Module);
		CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)LBIThread, NULL, NULL, NULL);
		break;
	case DLL_PROCESS_DETACH:
		break;
	default: break;
	}

	return TRUE;
}

void ReverseString(char* str)
{
	int last, limit, first;
	char temp;
	last = strlen(str) - 1;
	limit = last / 2;
	first = 0;

	while (first < last) {
		temp = str[first];
		str[first] = str[last];
		str[last] = temp;
		first++;
		last--;
	}
	return;
}

/*

  _______ _                     _             _      ____ _____
 |__   __| |                   | |           | |    |  _ \_   _|
	| |  | |__  _   _ _ __   __| | ___ _ __  | |    | |_) || |
	| |  | '_ \| | | | '_ \ / _` |/ _ \ '__| | |    |  _ < | |
	| |  | | | | |_| | | | | (_| |  __/ |    | |____| |_) || |_
	|_|  |_| |_|\__,_|_| |_|\__,_|\___|_|    |______|____/_____|


  Hello, You probably got this source from a person or from my discord server.
  This was made by speedsterkawaii mainly, no other devs couldn't help me rip.

  Just wanted to say a few quick notes before you use my exploit dll source.
  This isnt skidded at all, it is a custom lbi that i made for my exploit.

  You cannot modify the code and add your digusting code to it, that is bad.
  To get it start working, update the addresses and fix some stuff in hAddresses.h

  I didn't add functions due to the lack of skidding, so ye just enjoy this lbi.
  No don't you dare try to skid this, add credits to the owner or we will find you.

  If you need any help my username is SpeedSterKawaii#5233 on discord so yeah.
  Enjoy this lbi that i developed, you can add some functions but still have fun.

  EyeStep Stuff:
  I added eyestep for no reason, but it can be used for a new print pipe in void.

  Addresses Stuff:
  Update or fix the h file, its not hard, just requires some cpp knowledge bruh.

  Main Module:
  This has the DLL Attach so we would not put all the stuff in the dll cpp attach.

  Interpreter:
  That is the engine basically, we would not want to go there unless your modding.
*/
