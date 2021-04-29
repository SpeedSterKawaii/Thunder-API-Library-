#include "hRetcheck.h"
#include <Windows.h>
extern "C"
{
#include "Lua/lua.h"
#include "Lua/lapi.h"
#include "Lua/lualib.h"
#include "Lua/lauxlib.h"
}

uintptr_t rebase(uintptr_t loli) {
	return (loli - 0x400000 + reinterpret_cast<uintptr_t>(GetModuleHandleA(NULL)));
}

#define ree(x) (x - 0x400000 + (DWORD)GetModuleHandleA(0))

namespace addies
{
	DWORD TRL;
	lua_State* TL;

	int identity1 = 24; 
	int identity2 = 112;

	typedef void(__cdecl* lua_deserilize)(DWORD, void*);
	lua_deserilize  r_lua_deserialize = (lua_deserilize)((ree(0x0)));

	typedef void(__cdecl* lua_newthreadr)(DWORD, void*);
	lua_newthreadr  r_lua_newthread = (lua_newthreadr)((ree(0x0)));

	typedef void(__cdecl* lua_pushcclosuree)(DWORD, void*);
	lua_pushcclosuree  r_lua_pushcclosure = (lua_pushcclosuree)((ree(0x0)));

	typedef void(__cdecl* lua_getfieldd)(DWORD, void*);
	lua_getfieldd  r_lua_getfield = (lua_getfieldd)((ree(0x0)));

	typedef void(__cdecl* lua_setfieldd)(DWORD, void*);
	lua_setfieldd  r_lua_setfield = (lua_setfieldd)((ree(0x0)));

	typedef void(__cdecl* lua_pushstringg)(DWORD, void*);
	lua_pushstringg  r_lua_pushstring = (lua_pushstringg)((ree(0x0)));

	typedef void(__cdecl* lua_pcalll)(DWORD, void*);
	lua_pcalll  r_lua_pcall = (lua_pcalll)((ree(0x0)));

	typedef void(__cdecl* lua_pushvaluee)(DWORD, void*);
	lua_pushvaluee  r_lua_pushvalue = (lua_pushvaluee)((ree(0x0)));

	typedef void(__cdecl* lua_pushlstring)(DWORD, void*);
	lua_pushlstring  r_lua_pushlstring = (lua_pushlstring)((ree(0x0)));

	typedef void(__cdecl* lua_tolstring)(DWORD, void*);
	lua_tolstring  r_lua_tolstring = (lua_tolstring)((ree(0x0)));
}

namespace pipes
{
    #define setlbipipename "\\\\.\\pipe\\ThunderLBIPipe" // This is the LBI Pipe Name.

	DWORD WINAPI luapipename(PVOID lvpParameter)
	{
		std::string WholeScript = "";
		HANDLE hPipe;
		char buffer[999999];
		DWORD dwRead;
		hPipe = CreateNamedPipe(TEXT(setlbipipename),
			PIPE_ACCESS_DUPLEX | PIPE_TYPE_BYTE | PIPE_READMODE_BYTE,
			PIPE_WAIT,
			1,
			999999,
			999999,
			NMPWAIT_USE_DEFAULT_WAIT,
			NULL);
		while (hPipe != INVALID_HANDLE_VALUE)
		{
			if (ConnectNamedPipe(hPipe, NULL) != FALSE)
			{
				while (ReadFile(hPipe, buffer, sizeof(buffer) - 1, &dwRead, NULL) != FALSE)
				{
					buffer[dwRead] = '\0';
					try {
						try {
							WholeScript = WholeScript + buffer;
						}
						catch (...) {
						}
					}
					catch (std::exception e) {

					}
					catch (...) {

					}
				}
				// Execute Script.
				WholeScript = "";
			}
			DisconnectNamedPipe(hPipe);
		}
	}
}