using System;
using System.Reflection;

namespace NoMainExample
{
    // Application - Core logic class
    public static class Application
    {
        // Core business logic (no self-call)
        private static void CoreLogic()
        {
            Console.WriteLine("App core business logic executed!");
            
            var applicationType = typeof(Application);
            Console.WriteLine("\nApplication class members:");
            foreach (var m in applicationType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                Console.WriteLine($"  - {m.Name}");
            }
        }
        
        // Business logic started via reflection (no self-call)
        public static void StartApplication()
        {
            Console.WriteLine("Starting application...");
            
            var applicationType = typeof(Application);
            var coreMethod = applicationType.GetMethod("CoreLogic", 
                BindingFlags.Static | BindingFlags.NonPublic);
            
            if (coreMethod != null)
            {
                Console.WriteLine("\nExecuting CoreLogic via reflection:");
                coreMethod.Invoke(null, null);
            }
        }
    }

    // Console app entry - Main method MUST exist!
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
            Console.WriteLine("\n========================================");
            Console.WriteLine("The Truth About Console Apps:");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("1. Every console app MUST have a Main method");
            Console.WriteLine("2. [assembly:EntryPoint] is for dlls to specify entry point");
            Console.WriteLine("3. .NET Core+ supports EntryPoints.MainMethods discovery");
            Console.WriteLine("4. But in .exe, Main is still the required entry point");
            Console.WriteLine();
            
            // Demo: reflect to call other static methods (no self-recursion)
            var applicationType = typeof(Application);
            var startMethod = applicationType.GetMethod("StartApplication", 
                BindingFlags.Public | BindingFlags.Static);
            
            if (startMethod != null)
            {
                Console.WriteLine("Calling Application.StartApplication() via reflection:");
                startMethod.Invoke(null, null);
            }
        }
    }
}
