# CtfTools/BruteForceFramework
BruteForceFramework by [Aurecchia Gianfrancesco](https://github.com/GianfriAur)

### how to start

```c#
using BruteForceFramework;
```

### Constructors

#### Base
```c#
BruteForce BT = new BruteForce((a) => { /* TEST */ return true; });
```

#### Whith charset
```c#
string cset=  = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower() + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "1234567890" + "-_<>!\"@#+*()[]{}%&$£&/\\";
BruteForce BT = new BruteForce((a) => { /* TEST */ return true; },cset);
```

#### multi task
```c#
BruteForce BT = new BruteForce((a) => { /* TEST */ return true; },cset, BruteForce.AttackType.MultiThreads, 500);    // -1 for no limits		CAUTION!!!!!
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

### Example - Tested with i7-6700HQ

```c#
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

string s = SHA512("olleh");

BruteForce BT = new BruteForce((a) => {string hash= SHA512(a);return hash == s;}, "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower());  // 0h:00m:41s, ms:459 

BruteForce BT = new BruteForce((a) => {string hash= SHA512(a);return hash == s;}, "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower(),BruteForce.AttackType.MultiThreads,500);  // 0h:01m:20s, ms:625 

BruteForce BT = new BruteForce((a) => {string hash= SHA512(a);return hash == s;}, "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower(),BruteForce.AttackType.MultiThreads,1000);  // 0h:01m:09, ms:459 

BruteForce BT = new BruteForce((a) => {string hash= SHA512(a);return hash == s;}, "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower(),BruteForce.AttackType.MultiThreads,2000);  // 0h:01m:00s, ms:329 

BruteForce BT = new BruteForce((a) => {string hash= SHA512(a);return hash == s;}, "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower(),BruteForce.AttackType.MultiThreads,-1);  // 0h:00m:23s, ms:112
```

