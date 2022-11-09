namespace SudokuSolver
{

    internal class Program
    {
        static bool Check3x3(string data)
        {
            return true;
        }

        static bool CheckRow(string data)
        {
            return true;
        }

        static bool CheckColumn(string data)
        {
            return true;
        }

        static bool CheckIfCorrect(string data)
        {
            char[] correct = {'1','2','3','4','5','6','7','8','9','0','X'};
            data = data.ToUpper();
            foreach (char c in data)
            {
                if (!correct.Contains(c))
                    return false;
            }
            if (data.Length != 81)
                return false;

            return true;
        }

        static string Open(string path)
        {
            StreamReader sr = new StreamReader(path);

            return sr.ReadToEnd();
        }
        static void Main(string[] args)
        {
            //string path = Console.ReadLine();
            string data = Open(@"C:\Users\szymk\Desktop\kontrola.txt");
            if(CheckIfCorrect(data))
            {
            }
            else
            {
                Console.WriteLine("Errors in your data");
            }

        }
    }
}