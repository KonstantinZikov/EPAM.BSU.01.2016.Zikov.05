namespace Task1Console
{
    public static class StringSet
    {        
        public static string welcomeMessage = "Application initialized successfully.";
        public static string helpMessage = string.Concat
        (
            "Input digit to action:\r\n",
            "1. Add book.\r\n",
            "2. Remove book.\r\n",
            "3. Find by...\r\n",
            "4. Sort by...\r\n",
            "5. Show all books.\r\n",
            "6. Forced save catalogue.\r\n",
            "7. Exit."
        );

        public static string wrongYearInputMessage = "Year must be the integer number in the range from 0 to 2020.";

        public static string findByMessage = string.Concat
        (
            "Find by:\r\n",
            "1. Title.\r\n",
            "2. Author.\r\n",
            "3. Year of publication\r\n"
        );

        public static string sortByMessage = string.Concat
        (
            "Sort by:\r\n",
            "1. Title.\r\n",
            "2. Author.\r\n",
            "3. Year of publication\r\n"
        );
    }
}
