#include "pch.h"
#include <Windows.h>
#include <string>
#include "hAddresses.h"
using namespace std;
bool usingwrapper = false; // Do you see any wrapper parser?
const char* lbiname = "Thunder LBI";
namespace interpreter
{
	void compile(std::string hscript)
	{
		if (hscript.c_str(), hscript.length(), lbiname)
		{
			MessageBoxA(NULL, lua_tostring(addies::TL, -1), "Thunder Error", NULL);
			// Caught an error and display in a form of a message box.
		}
		else 
		{
			lua_pcall(addies::TL, 0, 0, 0);
		}
	}

	void convert(std::string hconvert)
	{

	}
	 
	void fcompile(std::string hconvert) // Executes script with a exception, also known as fcompile, forcecompile.
	{
		try
		{
			if (hconvert.c_str(), hconvert.length(), lbiname);
		}
		catch (exception x)
		{
			MessageBoxA(NULL, x.what(), "Thunder LBI", NULL);
			// Caught a error while executing (exception).
		}
	}
}