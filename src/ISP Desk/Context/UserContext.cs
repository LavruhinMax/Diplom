using ISP_Desk.Model;
using System.Reflection.Metadata.Ecma335;

namespace ISP_Desk.Context
{
    public static class UserContext
    {
        public static Lead Lead { get; set; }
        public static Installator Installator { get; set; }
        public static Dispatcher Dispatcher { get; set; }
    }
}
