using System;
using System.Collections.Generic;

namespace PrismProject
{
    public class Cmds
    {
        

        #region stuff
        public static int PixelHeight;
        public static int PixelWidth;
        public struct Command
        {
            public string Name, HelpDesc;
            public function func;
        }

        public static List<Command> cmds = new List<Command>();
        public delegate void function(string[] args);
        #endregion

        public static void Parse(string input)
        {
            string[] args = input.Split(new char[0]);
            string[] cmdargs = { };
            if (input.Contains(" ")) { cmdargs = input.Remove(0, input.IndexOf(' ') + 1).Split(new char[0]); }

            foreach (Command cmd in cmds)
            {
                if (args[0].Equals(cmd.Name))
                {
                    cmd.func(cmdargs);
                    return;
                }
            }

            Utils.Error("Invalid command.");
        }

        private static void AddCommand(string name, string desc, function func)
        {
            Command cd = new Command();
            cd.Name = name;
            cd.HelpDesc = desc;
            cd.func = func;
            cmds.Add(cd);
        }

        public static void Init()
        {
            AddCommand("print", "Print any string of text, used for console applications.", print);
            AddCommand("about", "About prism OS", about);
            AddCommand("help", "List all available commands", help);
            AddCommand("shutdown", "Shuts down the system.\nArguments\n==========\n-r restarts the system instead", shutdown);
            AddCommand("clear", "clear entire console", clear);
            AddCommand("sysinfo", "Prints system information", sysinfo);
            AddCommand("tone", "used to make sounds in an app", tone);
        }

        #region Misc Commands
        static void print(string[] args)
        {
            if (args.Length < 1)
            {
                Utils.Error("Insufficient arguments.");
            }
            string content = String.Join(" ", args);
            Console.WriteLine(content);
        }
        static void tone(string[] args)
        {
            Utils.argcheck(args, 2, 2);
            int beep1 = int.Parse(args[0]);
            int beep2 = int.Parse(args[1]);
            Console.Beep(beep1, beep2);
        }
        static void help(string[] args)
        {
            if (args.Length < 1)
            {
                Utils.colorCache = Console.ForegroundColor;
                Utils.SetColor(ConsoleColor.Cyan);
                Console.WriteLine("________________________________________");
                Console.WriteLine("---- List of all available commands ----");
                Console.WriteLine("=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.WriteLine();
                Utils.SetColor(ConsoleColor.Blue);
                foreach (Command cmd in cmds)
                {
                    Console.WriteLine(cmd.Name);
                }
                Utils.SetColor(Utils.colorCache);
                Console.WriteLine();
                Console.WriteLine("You can get more specific help for each command by using: HELP <COMMAND_NAME>");
                Console.WriteLine();
            }
            else
            {
                foreach (Command cmd in cmds)
                {
                    if (args[0] == cmd.Name)
                    {
                        Console.WriteLine(cmd.HelpDesc);
                        Console.WriteLine();
                        return;
                    }
                }
            }
        }
        static void about(string[] args)
        {
            Utils.SetColor(ConsoleColor.Yellow);
            Console.WriteLine(@"
_______________________________________________
    ____       _                   ____  _____
   / __ \_____(_)________ ___     / __ \/ ___/
  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ 
 / ____/ /  / (__  ) / / / / /  / /_/ /___/ / 
/_/   /_/  /_/____/_/ /_/ /_/   \____//____/                                                
_______________________________________________
");
            Utils.SetColor(ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Prism OS (c) 2021, release " + Kernel.versionID);
            Console.WriteLine("Created by bad-codr and deadlocust");
            Utils.Warn("This is a closed beta version of Prism OS, we are not responsible for any damages caused by it.");
            Console.WriteLine();
            Utils.SetColor(ConsoleColor.White);
        }
        static void shutdown(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(@"Shutdown machine?
 ____       ____ 
||y ||   / ||n ||
||__||  /  ||__||
|/__\| /   |/__\|"
                );
                ConsoleKeyInfo input = Console.ReadKey(false);
                if (input.KeyChar == 'Y' || input.KeyChar == 'y') { Cosmos.System.Power.Shutdown(); }
                Console.WriteLine();
                return;
            }
            else if (args[0] == "-r")
            {
                Console.WriteLine(@"Reboot machine?
 ____       ____ 
||y ||   / ||n ||
||__||  /  ||__||
|/__\| /   |/__\|");
                ConsoleKeyInfo input = Console.ReadKey(false);
                if (input.KeyChar == 'Y' || input.KeyChar == 'y') { Cosmos.System.Power.Reboot(); }
                Console.WriteLine();
                return;
            }
            else if (args[0] == "-t")
            {
                Utils.Warn("shutting down in " + args[1] + " seconds");
                int time = Convert.ToInt32(args[1]);
                Utils.Sleep(time);
                Cosmos.System.Power.Shutdown();
            }
            return;
        }
        static void clear(string[] args)
        {
            Console.Clear();
        }
        static void sysinfo(string[] args)
        {
            var cspeed = Cosmos.Core.CPU.GetCPUCycleSpeed();
            var ram = Cosmos.Core.CPU.GetAmountOfRAM();
            Utils.syetem_message("CPU clock speed: " + cspeed + " Mhz");
            Utils.syetem_message("Total ram: " + ram + " MB");
        }
        #endregion
    }
}
