using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BruteForceFramework
{
    public class BruteForce
    {

        private char[] Charset;

        private bool found = false;

        public AttackType attackType { get; set; }

        public int NumOfWork = 1;

        #region Costancts

        public static string CharSetDefault = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower() + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "1234567890" + "-_<>!\"@#+*()[]{}%&$£&/\\";

        #endregion

        #region Events



        public delegate void OnTrue(String TestPassed);
        public event OnTrue OnTrueEvent = (item) => { };

        public delegate void OnFalse(String TestPassed);
        public event OnFalse OnFalseEvent;// = (item) => { };

        #endregion



        private readonly Func<string, bool> func;

        int maxTask;
        public BruteForce(Func<string, bool> func, string charset = null, AttackType at = AttackType.SingleThreads, int MaxTask = 500)
        {
            // Charset = charset==null? CharSetDefault.ToCharArray():charset.ToCharArray();
            Charset = (charset ?? CharSetDefault).ToCharArray();
            attackType = at;
            this.func = func;
            maxTask = MaxTask;

        }

        private void Do()
        {
            var buff = new List<int> { 0 };
            bool found = false;
            while (!found)
            {
                buff[0] += 1;
                for (int i = 0; i < buff.Count; i++)
                    if (buff[i] == Charset.Length)
                    {
                        if (i == buff.Count - 1)
                        {
                            buff[i] = 0;
                            buff.Add(0);
                        }
                        else
                        {
                            buff[i] = 0;
                            buff[i + 1] += 1;
                        }
                    }
                string p = "";
                foreach (int i in buff)
                    p += Charset[i];
                found = func(p);



                if (!found)

                    OnFalseEvent?.Invoke(p);

                else
                    OnTrueEvent(p);

            }
        }

        private bool _Dot_Found = false;
        private int _Dot_Tcont = 0;
        /// <summary>
        /// E' UN ESPERIMENTO, FATE FINA CHE NON ESISTE
        /// </summary>
        private void DoTT()
        {
            System.Diagnostics.Debug.WriteLine("DOTT");
            var buff = new List<Int16> { 0 };
            while (!_Dot_Found)
            {
                buff[0] += 1;
                for (Int16 i = 0; i < buff.Count; i++)
                    if (buff[i] == Charset.Length)
                    {
                        buff[i] = 0;
                        if (i == buff.Count - 1)
                            buff.Add(0);
                        else
                            buff[i + 1] += 1;
                    }

                string p = ""; foreach (int i in buff) p += Charset[i];
       
                Task.Factory.StartNew(() => { if (!_Dot_Found) { if (func(p)) { OnTrueEvent(p); _Dot_Found = true; } } });
            }
        }
        private void DoT()
        {
            var buff = new List<int> { 0 };
            while (!_Dot_Found)
            {
                buff[0] += 1;
                for (int i = 0; i < buff.Count; i++)
                    if (buff[i] == Charset.Length)
                    {
                        if (i == buff.Count - 1)
                        {
                            buff[i] = 0;
                            buff.Add(0);
                        }
                        else
                        {
                            buff[i] = 0;
                            buff[i + 1] += 1;
                        }
                    }
                string p = "";
                foreach (int i in buff)
                    p += Charset[i];


                _Dot_Tcont++;

                if (maxTask != -1)
                    while (_Dot_Tcont > maxTask) { }

                Task.Factory.StartNew(() =>
                {
                    bool f = func(p);

                    if (!_Dot_Found)
                    {
                        if (!f)
                            OnFalseEvent?.Invoke(p);
                        else
                        {
                            OnTrueEvent(p);
                            _Dot_Found = f;
                        }
                    }
                    _Dot_Tcont--;
                });
            }
        }

        DateTime stat;
        public TimeSpan Duration;
        public void Work()
        {
            stat = DateTime.Now;
            if (attackType == AttackType.SingleThreads)
            {
                Do();
            }
            else
            {
                if (OnFalseEvent != null || maxTask != -1)
                    DoT();
                else
                    DoTT();
            }
            Duration = DateTime.Now.Subtract(stat);
        }

        public enum AttackType
        {
            SingleThreads,
            MultiThreads,
        }


    }
}
