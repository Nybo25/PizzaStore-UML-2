using System;
using System.Collections.Generic;

public class UserDialog
{
    public int MenuChoice(List<string> menuItems)
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {menuItems[i]}");
        }

        while (true)
        {
            Console.Write("Please select an option: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= menuItems.Count)
            {
                return choice;
            }
            Console.WriteLine("Invalid input, please try again.");
        }
    }
}

