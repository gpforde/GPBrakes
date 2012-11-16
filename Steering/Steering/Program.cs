using System;

namespace BrakingSystem
{
#if WINDOWS || XBOX
    static class Program
    {
   
        static void Main(string[] args)
        {
            using (XNAGame game = new XNAGame())
            {
                game.Run();
            }
        }
    }
#endif
}

