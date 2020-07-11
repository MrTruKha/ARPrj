using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Common
{
    public class Common
    {
        public static string RandomPassword()
        {
            Random random = new Random();
            string a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] ch = a.ToCharArray();
            string b = "";
            for (int i = 0; i < random.Next(6,20); i++)
            {
                b += ch[random.Next(ch.Length)].ToString();

            }
            return b;
        }
        public static void ExceptionLogger(string methodName, string exception)
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy");
            var path = @"F:\ExceptionLogger\" + date + ".txt";
            try
            {
                if (File.Exists(path))
                {
                    StringBuilder builder = new StringBuilder();
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string text = "";
                        while ((text = reader.ReadLine()) != null)
                        {
                            builder.AppendLine(text);
                        }
                    }
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        builder.AppendLine(methodName + "-" + exception + "-" + DateTime.Now.ToString("hh:mm:ss"));
                        writer.Write(builder);
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create)))
                    {
                        writer.WriteLine(methodName + "-" + exception+"-"+DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
        
        }
    }
}
