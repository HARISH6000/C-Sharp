using System;

namespace Task1
{
    class MathOperations
    {
        public static int Fact(int num)
        {
            if (num==0||num==1)
            {
                return 1;
            }
            else
            {
                return num*Fact(num-1);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number to find its factorial:");
            string number=Console.ReadLine();
            int num=int.Parse(number);
            if(num<0){
                Console.WriteLine("Error: Factorial of negative number is not defined.");
            }
            else{
                Console.WriteLine("Factorial of "+num+" is "+MathOperations.Fact(num));
            }
            
        }
    }
}