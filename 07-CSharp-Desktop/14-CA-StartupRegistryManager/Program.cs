using System;
using Microsoft.Win32;

namespace NEW_PRACTICE
{
    internal class Program
    {
        static string reg_path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - Show startup programs");
                Console.WriteLine("2 - Add startup program");
                Console.WriteLine("3 - Delete startup program");
                Console.WriteLine("0 - Exit");
                Console.Write("Input: ");

                string cmd = Console.ReadLine();
                Console.WriteLine();

                if (cmd == "1")
                {
                    ShowPrograms();
                }
                else if (cmd == "2")
                {
                    AddProgram();
                }
                else if (cmd == "3")
                {
                    DeleteProgram();
                }
                else if (cmd == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong command");
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void ShowPrograms()
        {
            Console.WriteLine("HKCU:");
            ShowFromKey(Registry.CurrentUser);
            Console.WriteLine();
            Console.WriteLine("HKLM:");
            ShowFromKey(Registry.LocalMachine);
        }

        static void ShowFromKey(RegistryKey root_key)
        {
            RegistryKey run_key = root_key.OpenSubKey(reg_path, false);

            if (run_key == null)
            {
                Console.WriteLine("Registry key not found.");
                return;
            }

            string[] parameters = run_key.GetValueNames();

            if (parameters.Length == 0)
            {
                Console.WriteLine("No programs.");
                return;
            }

            foreach (string val_name in parameters)
            {
                object val = run_key.GetValue(val_name, "None");
                Console.WriteLine(val_name + " : " + val);
            }

            run_key.Close();
        }

        static void AddProgram()
        {
            RegistryKey root_key = ChooseRootKey();
            if (root_key == null)
                return;

            Console.Write("Program name: ");
            string prog_name = Console.ReadLine();

            Console.Write("Program path: ");
            string prog_path = Console.ReadLine();

            if (prog_name == "" || prog_path == "")
            {
                Console.WriteLine("Empty input.");
                return;
            }

            try
            {
                RegistryKey run_key = root_key.CreateSubKey(reg_path);
                run_key.SetValue(prog_name, prog_path);
                run_key.Close();
                Console.WriteLine("Program added.");
            }
            catch
            {
                Console.WriteLine("Error while adding program.");
            }
        }

        static void DeleteProgram()
        {
            RegistryKey root_key = ChooseRootKey();
            if (root_key == null)
                return;

            Console.Write("Program name: ");
            string prog_name = Console.ReadLine();

            if (prog_name == "")
            {
                Console.WriteLine("Empty input.");
                return;
            }

            try
            {
                RegistryKey run_key = root_key.OpenSubKey(reg_path, true);

                if (run_key == null)
                {
                    Console.WriteLine("Registry key not found.");
                    return;
                }

                run_key.DeleteValue(prog_name, false);
                run_key.Close();
                Console.WriteLine("Program deleted.");
            }
            catch
            {
                Console.WriteLine("Error while deleting program.");
            }
        }

        static RegistryKey ChooseRootKey()
        {
            Console.WriteLine("1 - HKCU");
            Console.WriteLine("2 - HKLM");
            Console.Write("Choose key: ");
            string cmd = Console.ReadLine();

            if (cmd == "1")
                return Registry.CurrentUser;

            if (cmd == "2")
                return Registry.LocalMachine;

            Console.WriteLine("Wrong key.");
            return null;
        }
    }
}
