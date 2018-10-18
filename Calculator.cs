using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Сохранение имя переменной и ее значения
            Dictionary<string, int> saveValues = new Dictionary<string, int>();
            while (true)
            {
                int result = 0;
                //Удаляем все пробелы
                string inputText = Console.ReadLine().Replace(" ", "");
                if (inputText.Trim() == "0")
                    break;
                int indexOperation = 0;
                //Разделяем строку между =
                string[] division = inputText.Split('=');
                //Если имеется левая часть
                if (division.Length > 1)
                    //Если такая переменная не имеется уже в словаре, добавляем ее
                    if (!saveValues.ContainsKey(division[0]))
                        saveValues.Add(division[0], 0);
                List<int> listVal = new List<int>();
                //Ищем в выражении после равно (если имеется) переменные 
                var resultString = Regex.Matches(division[division.Length - 1], @"[A-Za-z]+");
                //Если нету знаков + - в строке, то присваеваем левой переменной, значение правой
                if (inputText.IndexOfAny(new char[] { '+', '-' }) == -1)
                {
                    saveValues[division[0]] = saveValues[division[1]];
                    result = saveValues[division[0]];
                }
                //Иначе выполняем операцию между двумя числами
                else
                {
                    //В строке одна переменная
                    if (resultString.Count == 1)
                    {
                        listVal.Add(saveValues[resultString[0].Value]);
                        var listValues = Regex.Matches(division[division.Length - 1], @"[+-]{1}[-]{0,1}\d+");
                        listVal.Add(int.Parse(listValues[0].Value.Substring(1, listValues[0].Value.Length - 1)));
                        inputText = inputText.Replace(resultString[0].Value, listVal[0].ToString());

                    }
                    //В строке две переменных
                    else if (resultString.Count == 2)
                    {
                        listVal.Add(saveValues[resultString[0].Value]);
                        listVal.Add(saveValues[resultString[1].Value]);
                        inputText = inputText.Replace(resultString[0].Value, listVal[0].ToString());
                        inputText = inputText.Replace(resultString[1].Value, listVal[1].ToString());
                    }
                    //В строке нету переменных
                    else
                    {
                        var listValues = Regex.Matches(division[division.Length - 1], @"[-]{0,1}\d+");
                        listVal.Add(int.Parse(listValues[0].Value));
                        listValues = Regex.Matches(division[division.Length - 1], @"[+-]{1}[-]{0,1}\d+");
                        listVal.Add(int.Parse(listValues[0].Value.Substring(1)));
                    }
                    //Ищем индекс знака операции между двумя значениями
                    for (int item = 0; item < inputText.Length; item++)
                    {
                        indexOperation = inputText.IndexOfAny(new char[] { '+', '-' }, indexOperation + 1);
                        if (int.TryParse(inputText[indexOperation - 1].ToString(), out int firstValue) && (int.TryParse(inputText[indexOperation + 1].ToString(), out int secondValue) || int.TryParse(inputText[indexOperation + 2].ToString(), out int secondValueAgain)))
                        {
                            break;
                        }
                    }
                    //Получаем знак операции
                    char operation = inputText[indexOperation];
                    if (operation == '+')
                        result = listVal[0] + listVal[1];
                    else
                        result = listVal[0] - listVal[1];
                    //Сохраняем значение переменной
                    if (division.Length > 1)
                        saveValues[division[0]] = result;
                }
                Console.WriteLine(result);
            }
        }
    }
}
