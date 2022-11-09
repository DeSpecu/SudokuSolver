namespace SudokuSolver
{

    internal class Program
    {
        //Checks 3x3 area of diagram
        static bool Check3x3(string[,] data, int row, int column, int toCheck)
        {
            string checking = Convert.ToString(toCheck);
            //This part probably need some change
            //----------------
            if (row < 3)
                row = 0;
            else if (row >= 3 && row < 6)
                row = 3;
            else
                row= 6;
            
            if (column < 3)
                column = 0;
            else if (column >= 3 && column < 6)
                column = 3;
            else
                column = 6;
            //-----------------

            for (int i = row; i < row+3; i++)
            {
                for (int j = column; j < column+3; j++)
                {
                    if (data[i,j]==checking)
                        return false;
                }
            }

            return true;
        }

        //Checks Row of diagram
        static bool CheckRow(string[,] data, int row,int toCheck)
        {
            string checking = Convert.ToString(toCheck);
            for (int i = 0; i < 9; i++)
            {
                if (data[row, i] == checking)
                    return false;
            }
            return true;
        }

        //Checks Column of Diagram
        static bool CheckColumn(string[,] data, int column,int toCheck)
        {
            string checking = Convert.ToString(toCheck);
            for (int i = 0; i <9; i++)
            {
                if (data[i, column] == checking)
                    return false;
            }
            return true;
        }

        //Checks if a field is empty
        static bool IsEmpty(string[,] data, int row, int column)
        {
            if (data[row, column] == "X")
                return true;

            return false;
        }

        //Main solving function
        static bool Solve(string[,] data)
        {
            string[] forCheck = {"0" ,"1", "2", "3", "4", "5", "6", "7", "8", "9"};
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (IsEmpty(data, i, j))
                    {
                        for (int checking = 1; checking < 10; checking++)
                        {
                            if (Check3x3(data,i,j,checking) && CheckColumn(data,j,checking) && CheckRow(data,i, checking))
                            {
                                data[i,j]=forCheck[checking];
                                if (Solve(data))
                                    return true;
                            }
                        }
                        data[i, j] = "X";
                        return false;
                    }
                }
            }
            return true;
        }

        //Checks opened file if data in it is correct
        static bool CheckIfCorrect(string[,] data)
        {
            string[] correct = {"1","2","3","4","5","6","7","8","9","X"};

            foreach (string s in data)
            {
                if (!correct.Contains(s))
                    return false;
            }

            return true;
        }

        //Opens file and returns diagram to solve
        static string[,] Open(string path)
        {
            StreamReader sr = new StreamReader(path);
            string data = sr.ReadToEnd();
            string[,] sudoku = new string[9,9];
            int i =0, j = 0;
            foreach (char c in data)
            {
                if(j>=9)
                {
                    j = 0;
                    i++;
                }
                sudoku[i, j] = Convert.ToString(c);
                j++;
            }
            return sudoku;
        }

        //Saves file
        static void Save(string[,] data, string path)
        {
            StreamWriter sw = new StreamWriter(path);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sw.Write(data[i, j]);
                }
                sw.WriteLine();
            }
            sw.Close();
            Console.WriteLine("Saved!");
        }

        
        static void Main(string[] args)
        {
            try 
            { 
                Console.WriteLine("Path to your file");
                string path = Console.ReadLine();
                Console.WriteLine("Path to save your solved sudoku");
                string savePath = Console.ReadLine();
                string[,] sudoku = Open(path);
                if (CheckIfCorrect(sudoku))
                {
                    Solve(sudoku);
                }
                else
                {
                    Console.WriteLine("Errors in your data");
                }
                Save(sudoku, savePath);
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch(FileNotFoundException fnf)
            {
                Console.WriteLine(fnf.Message);
            }
            catch(IndexOutOfRangeException ior)
            {
                Console.WriteLine(ior.Message);
            }
            catch(Exception)
            {
                Console.WriteLine("Unexpected error occurred");
            }
        }
    }
}