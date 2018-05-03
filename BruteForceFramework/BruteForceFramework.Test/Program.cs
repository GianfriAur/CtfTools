using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BruteForceFramework;
namespace BruteForceFramework.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            string s = SHA512("hello");

            BruteForce BT = new BruteForce((a) => {string hash= SHA512(a);return hash == s;}, "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower(), BruteForce.AttackType.MultiThreads,-1);

            BT.OnTrueEvent += (a) => { System.Diagnostics.Debug.WriteLine("Si -- Trovato:" + a); };
         //   BT.OnFalseEvent += (a) => { Console.WriteLine("No -- Testato:" + a); };

              BT.Work();

           System.Diagnostics.Debug.WriteLine(String.Format("{0}h:{1:D2}m:{2:D2}s, ms:{3:D2}", (int)BT.Duration.TotalHours, BT.Duration.Minutes, BT.Duration.Seconds, BT.Duration.Milliseconds));
           Console.WriteLine(String.Format("{0}h:{1:D2}m:{2:D2}s, ms:{3:D2}", (int)BT.Duration.TotalHours, BT.Duration.Minutes, BT.Duration.Seconds, BT.Duration.Milliseconds));
            Console.ReadKey();
        }
        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {

                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
            
        }

    }
}
