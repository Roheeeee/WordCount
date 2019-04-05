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
        private int iLinecount;  //定义文件名、参数数组、字符数、单词数、总行数
        char[] symbol = { ' ', '\t', ',', '.', '?', '!', ':', ';', '\'', '\"', '\n', '{', '}', '(', ')', '+', '-', '*', '=' };                            //定义一个字符数组
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

        public void BaseCount(List<string> ls)
        {
            try
            {
                int sChar;
                int charcount = 0;
                int linecount = 0;
                FileStream fs = new FileStream(@"D:\第三次作业\WordCount\WordCount\bin\Debug\input.txt", FileMode.Open, FileAccess.Read); // 打开文件（运用指令）
                StreamReader sr = new StreamReader(fs);
                while ((sChar = sr.Read()) != -1)
                {
                    charcount++;     // 统计字符数
                    if (sChar == '\n')
                    {
                        linecount++; // 统计行数
                    }
                }
                iCharcount = charcount;
                iWordcount = ls.Count - 1;
                iLinecount = linecount + 1;

                sr.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }
        public void Word(List<string> ls)
        {

            string[] IWord1 = new string[] { };
            string[] ILine = File.ReadAllLines(@"D:\第三次作业\WordCount\WordCount\bin\Debug\input.txt");
            for (int i = 0; i < ILine.Length; i++)
            {
                IWord1 = ILine[i].Split(' ');
                for (int j = 0; j < IWord1.Length; j++)
                {
                    ls.Add(IWord1[j]);
                }
            }
        }
        public void SaveResult(List<string> ls)    //将输出结果保存数据到该地址下的resul.text中
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
            sw.WriteLine("文件中包含单词有：");
            for (int j = 0; j < ls.Count; j++)
            {
                sw.WriteLine(ls[j]);
            }
            ls.Sort(); //排序
            sw.WriteLine("按字典序排序之后：");
            for (int j = 0; j < ls.Count; j++)
            {
                //按小写输出
                sw.WriteLine(ls[j].ToLower());
            }
            int max = -1;
            List<string> temp = new List<string>();
            int maxt = 0;
            int[] count = new int[ls.Count + 10];
            for (int i = 0; i < ls.Count; i++)
            {
                count[i] = 1;
            }
            for (int i = 0; i < ls.Count; i++)
            {
                for (int j = 0; j < ls.Count; j++)
                {
                    if (i != j && ls[i].Equals(ls[j]))
                    {
                        count[i]++;
                    }
                }
            }
            //  for (int i = 0; i < ls.Count; i++)
            //   {
            //      sw.WriteLine(ls[i]+" "+count[i]);
            //   }
            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] > max)
                {
                    max = count[i];
                    maxt = i;
                }
            }
            temp.Add(ls[maxt]);
            //  sw.Write("max=" + max);
            int t = 0, c = 0;
            sw.WriteLine("文件中单词出现频率最高的10个单词：");
            sw.WriteLine("<" + ls[maxt] + ">：" + count[maxt]);
            while (t < 9)
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    if (t == 9) break;
                    c = 0;
                    for (int j = 0; j < temp.Count; j++)
                    {
                        if (temp[j] == ls[i]) c = 1;
                    }
                    if (c == 0)
                    {
                        if (count[i] == max)
                        {
                            sw.WriteLine("<" + ls[i] + ">：" + count[i]);
                            t++;
                            temp.Add(ls[i]);
                        }
                    }
                }
                max--;
            }

            sw.Close();
            fs.Close();
            Console.ReadKey();
        }
        //主函数实现功能
        static void Main(string[] args)
        {
            Wordcount wc = new Wordcount();
            List<string> ls = new List<string>();
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
                wc.Word(ls);
                wc.BaseCount(ls);
                wc.SaveResult(ls);
            }
        }
    }
}


