using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution;

public static class AppEnvironment
{
    const string DEV = "Development";
    const string STAGING = "Staging";
    const string PROD = "Production";

    public static string Current { get; private set; } =
#if Development     // dotnet publish -c Debug
        DEV;
#elif Staging       // dotnet publish -c Release -p:Environment=Staging
        STAGING;
#else               // dotnet publish -c Release
        PROD;
#endif

    public static bool IsDev()
    {
        return Is(DEV);
    }

    public static bool IsProd()
    {
        return Is(PROD);
    }

    public static bool IsStaging()
    {
        return Is(STAGING);
    }

    public static bool Is(string name)
    {
        return Current == name;
    }

    public static void Set(string name)
    {
        Current = name;
    }
}
