using K1_Banken_Team1.Domain;
using K1_Banken_Team1.Presentation;
using K1_Banken_Team1.Presentation.Menus;
using System.Diagnostics;
namespace K1_Banken_Team1
{
    internal class Program
    {

       
        static void Main(string[] args)
        {
            var menuHandler = new MenuHandler();
            menuHandler.Start(); // Start program.   
        }
    }
}