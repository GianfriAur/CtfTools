# CtfTools/DictionatyAttackFramework
DictionatyAttackFramework by [Aurecchia Gianfrancesco](https://github.com/GianfriAur)

### how to start

```c#
using DictionatyAttackFramework;
```

### Constructors

#### Base
```c#
DictionatyAttack<T> DA = new DictionatyAttack<T>((a) => {  /* TEST */ return true; }, List<T> DCT);
```

#### multi task
```c#   
DictionatyAttack<T> DA = new DictionatyAttack<T>((a) => {  /* TEST */ return true; }, List<T> DCT, DictionatyAttack<T>.AttackType.MultiThreads, -1);  // -1 for no limits		CAUTION!!!!!
```

### Events

#### Test True
```c#
BT.OnTrueEvent += (a) => { /* Found */ };
```

#### Test False // not use for more speed
```c#
BT.OnFalseEvent += (a) => {  /* Not found */ };
```

### Example 

```c#

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

List<string> DCT = new List<string>(System.IO.File.ReadAllText("20k.txt").Split('\n'));

string Salt = " cc18-unimi";

string hash = getHashSha256(Salt + "hacker");

DictionatyAttack<string> DA = new DictionatyAttack<string>((a) => { return hash == getHashSha256(Salt + a); }, DCT, DictionatyAttack<string>.AttackType.MultiThreads, -1);
DA.OnTrueEvent += (a) => { Console.WriteLine("password:" + a); };
DA.Work();

```
