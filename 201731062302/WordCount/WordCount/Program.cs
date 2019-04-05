using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace WordCount
{
    public class Wordcount
    {
        private string[] sParameter;
        private int iCharcount;
        private int iWordcount;
        private int iLinecount;         //定义文件名、参数数组、字符数、单词数、总行数
        public void Operator(string[] sParameter)  //判断输入进去的命令是否合法
        {
            this.sParameter = sParameter;
            foreach (string s in sParameter)
            {
                if (s == "-c" || s == "-w" || s == "-l")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("参数{0}不存在", s);
                    break;

                }
            }
        }

        private void BaseCount()
        {
            try
            {
                int sChar;
                int charcount = 0;
                int wordcount = 0;
                int linecount = 0;
                FileStream fs = new FileStream(@"D:\第三次作业\WordCount\WordCount\bin\Debug\input.txt", FileMode.Open, FileAccess.Read); // 打开文件（运用指令）
                StreamReader sr = new StreamReader(fs);

                char[] symbol = { ' ', '\t', ',', '.', '?', '!', ':', ';', '\'', '\"', '\n', '{', '}', '(', ')', '+' ,'-',
              '*', '='};                            //定义一个字符数组
                while ((sChar = sr.Read()) != -1)
                {
                    charcount++;     // 统计字符数

                    foreach (char c in symbol)
                    {
                        if (sChar == (int)c)
                        {
                            wordcount++;   // 统计单词数
                        }
                    }
                    if (sChar == '\n')
                    {
                        linecount++; // 统计行数
                    }
                }
                iCharcount = charcount;
                iWordcount = wordcount + 1;
                iLinecount = linecount + 1;
                sr.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }
        public void SaveResult()    //将输出结果保存数据到该地址下的resul.text中
        {
            FileStream fs = new FileStream(@"D:\第三次作业\WordCount\WordCount\bin\Debug\result.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string a in sParameter)
            {
                if (a == "-c")
                    sw.WriteLine("字符数:{0}", iCharcount);
                if (a == "-w")
                    sw.WriteLine("单词数:{0}", iWordcount);
                if (a == "-l")
                    sw.WriteLine("行数:{0}", iLinecount);
            }
            sw.Close();
            fs.Close();
            Console.ReadKey();
        }

        //主函数实现功能
        static void Main(string[] args)
        {
            Wordcount wc = new Wordcount();
            string message = "";
            while (message != "exit")
            {
                message = Console.ReadLine();
                string[] arrMessSplit = message.Split(' ');
                int iMessLength = arrMessSplit.Length;
                string[] sParameter = new string[iMessLength];
                for (int i = 0; i < iMessLength; i++)
                {
                    sParameter[i] = arrMessSplit[i];
                }
                wc.Operator(sParameter);
                wc.BaseCount();
                wc.SaveResult();

            }
        }
    }
}

