using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionatyAttackFramework
{
    public class DictionatyAttack<T>
    {

        private bool found = false;

        public AttackType attackType { get; set; }


        #region Costancts

        #endregion

        #region Events



        public delegate void OnTrue(T TestPassed);
        public event OnTrue OnTrueEvent = (item) => { };

        public delegate void OnFalse(T TestPassed);
        public event OnFalse OnFalseEvent = (item) => { };

        #endregion


        List<T> Dictionary;
        private readonly Func<T, bool> func;

        int maxTask;
        public DictionatyAttack(Func<T, bool> func, List<T> Dictionary, AttackType at = AttackType.SingleThreads, int MaxTask = 500)
        {
            this.Dictionary = Dictionary;
            attackType = at;
            this.func = func;
            maxTask = MaxTask;

        }

        private void Do()
        {
            foreach (T obj in Dictionary)
            {
                if (!func(obj))
                    OnFalseEvent(obj);
                else
                    OnTrueEvent(obj);
            }
        }

        private void DoT()
        {
            int _Dot_Tcont = 0;
            foreach (T obj in Dictionary)
            {
                if (maxTask != -1)
                    while (_Dot_Tcont > maxTask) { }

                Task.Factory.StartNew(() =>
                {
                    if (!func(obj))
                        OnFalseEvent(obj);
                    else
                        OnTrueEvent(obj);
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
                DoT();
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
