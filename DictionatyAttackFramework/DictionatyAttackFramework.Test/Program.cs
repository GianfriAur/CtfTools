using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteKeyWordsDump;

using DictionatyAttackFramework;
using System.Security.Cryptography;

namespace DictionatyAttackFramework.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> DCT = new List<string>(System.IO.File.ReadAllText("20k.txt").Split('\n'));

            string Salt = " cc18-unimi";

            string hash = getHashSha256(Salt + "hacker");

            DictionatyAttack<string> DA = new DictionatyAttack<string>((a) => { return hash == getHashSha256(Salt + a); }, DCT, DictionatyAttack<string>.AttackType.MultiThreads, -1);
            DA.OnTrueEvent += (a) => { Console.WriteLine("la password è :" + a); };
            DA.Work();


            Console.ReadKey();


        }

        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            using (SHA256Managed hashstring = new SHA256Managed())
            {
                byte[] hash = hashstring.ComputeHash(bytes);
                string hashString = string.Empty;
                foreach (byte x in hash)
                {
                    hashString += String.Format("{0:x2}", x);
                }
                return hashString;
            }
        }

    }
}
