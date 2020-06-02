using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the divided: ");
            string firstStr = Console.ReadLine();
            Console.Write("Enter the divisor: ");
            string secondStr = Console.ReadLine();

            int numb1, numb2;
            int quot;
            double rem;

            int count = 0;
            if (Int32.TryParse(firstStr, out numb1) && Int32.TryParse(secondStr, out numb2))
            {
                Result result = new Result();
                if (numb1 < 0)
                {
                    count++;
                    numb1 = Math.Abs(numb1);
                }
                if (numb2 < 0)
                {
                    count++;
                    numb2 = Math.Abs(numb2);
                }
                if (Math.Abs(numb1) >= Math.Abs(numb2))
                {
                    result = DivideNumbersInBinary(numb1, numb2);
                }
                else
                {
                    Console.WriteLine("The first number must be greater that the second number");
                    return;
                }
                quot = GetNumbFromBinaryList(result.Quotient);
                rem = GetNumbFromBinaryList(result.Remain);
                if (count == 1)
                {
                    quot = -quot;
                }
                Console.WriteLine();
                Console.WriteLine($"Result: {quot} \t Remainder: {rem}");
            }
            else
            {
                Console.WriteLine("You entered invalid number");
            }

            Thread.Sleep(3000);
            Console.ReadKey();
        }

        static Result DivideNumbersInBinary(int numb1, int numb2)
        {
            Result result = new Result();
            result.Quotient = new List<int>() { 0 };
            result.Remain = new List<int>();

            List<int> numb1InBits = GetBinaryFromNumb(numb1);
            List<int> numb2InBits = GetBinaryFromNumb(numb2);

            Console.WriteLine();
            Console.WriteLine($"Divided: {GetListAsStr(numb1InBits)} \t  Divisor: {GetListAsStr(numb2InBits)}");
            Console.WriteLine();

            int counter = 0;
            List<int> portionOfDivided = new List<int>();
            for (int i = 0; i < numb2InBits.Count; i++)
            {
                portionOfDivided.Add(numb1InBits[i]);
                counter++;
            }

            do
            {
                if (GetNumbFromBinaryList(portionOfDivided) >= GetNumbFromBinaryList(numb2InBits))
                {
                    result.Quotient.Add(1);
                    Console.WriteLine($"Divided: {GetListAsStr(portionOfDivided)} \t  Divisor: {GetListAsStr(numb2InBits)} \t  Quotient: {GetListAsStr(result.Quotient)}");
                    List<int> helper = new List<int>();
                    helper.AddRange(portionOfDivided);
                    portionOfDivided.Clear();
                    portionOfDivided.AddRange(Subtract(helper, numb2InBits));
                    if (counter >= numb1InBits.Count)
                    {
                        result.Remain.AddRange(portionOfDivided);
                        break;
                    }
                    portionOfDivided.Add(numb1InBits[counter]);
                    counter++;
                }
                else
                {
                    result.Quotient.Add(0);
                    Console.WriteLine($"Divided: {GetListAsStr(portionOfDivided)} \t  Divisor: {GetListAsStr(numb2InBits)} \t  Quotient: {GetListAsStr(result.Quotient)}");
                    if (counter >= numb1InBits.Count)
                    {
                        result.Remain.AddRange(portionOfDivided);
                        break;
                    }
                    portionOfDivided.Add(numb1InBits[counter]);
                    counter++;
                }
                RightShift(numb2InBits);
            }
            while (GetNumbFromBinaryList(numb1InBits) >= GetNumbFromBinaryList(numb2InBits));
            Console.WriteLine($"Remainder: {GetListAsStr(result.Remain)}");
            return result;
        }
        static string GetListAsStr(List<int> list)
        {
            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i];
            }
            return result;
        }
        static void RightShift(List<int> divisor)
        {
            List<int> helper = new List<int>() { 0 };
            helper.AddRange(divisor);
            divisor.Clear();
            divisor.AddRange(helper);
        }
        static List<int> Subtract(List<int> portionOfDivided, List<int> divisor)
        {
            List<int> result = new List<int>();

            List<int> helper;
            while (divisor[0] == 0)
            {
                divisor.RemoveAt(0);
            }
            while (divisor.Count < portionOfDivided.Count)
            {
                helper = new List<int>() { 0 };
                helper.AddRange(divisor);
                divisor.Clear();
                divisor.AddRange(helper);
                helper.Clear();
            }

            int i = divisor.Count - 1;
            while (i >= 0)
            {
                if (portionOfDivided[i] == 0 && divisor[i] == 0)
                {
                    result.Add(0);
                }
                else if (portionOfDivided[i] == 1 && divisor[i] == 1)
                {
                    result.Add(0);
                }
                else if (portionOfDivided[i] == 1 && divisor[i] == 0)
                {
                    result.Add(1);
                }
                else
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (portionOfDivided[j] == 1)
                        {
                            for (int k = j + 1; k <= i; k++)
                            {
                                if (portionOfDivided[k] == 0)
                                    portionOfDivided[k] = 1;
                            }
                            portionOfDivided[j] = 0;
                            result.Add(1);
                            break;
                        }
                    }
                }
                i--;
            }

            result.Reverse();
            while (result.Count != 1 && result[0] == 0)
            {
                result.RemoveAt(0);
            }
            return result;
        }
        static int GetNumbFromBinaryList(List<int> list)
        {
            int result = 0;
            int counter = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == 1)
                {
                    result += (int)Math.Pow(2, counter);
                }
                counter++;
            }
            return result;
        }
        static List<int> GetBinaryFromNumb(int numb)
        {
            List<int> result = new List<int>();
            while (numb > 0)
            {
                result.Add(numb % 2);
                numb /= 2;
            }
            result.Reverse();
            return result;
        }
    }

    class Result
    {
        public List<int> Quotient { get; set; }
        public List<int> Remain { get; set; }
    }
}
