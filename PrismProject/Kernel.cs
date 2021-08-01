using Sys = Cosmos.System;

namespace PrismProject
{
    public class Kernel : Sys.Kernel
    {
        public static string Kernel_build = "POSK Revision 2.5", Codename = "glass";
        public static bool Running = true, canvasRunning = true;

        protected override void Run()
        {
            //uncomment when threading is a thing
            //new Thread(INIT.Run).Start();
            //Driver.Init();
            //Desktop.Start();
            Cmds.Start();
        }
    }
}