using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class DamageType
{
    private static Dictionary<Type, int> damages = new Dictionary<Type, int>();

    public static void Init()
    {
        if (!damages.ContainsKey(typeof(Bullet)))
            damages.Add(typeof(Bullet), 1);
        if (!damages.ContainsKey(typeof(Enemy)))
            damages.Add(typeof(Enemy), 1);
    }

    public static int Get(Type t)
    {
        if (damages.ContainsKey(t))
        {
            return damages[t];
        }
        else
        {
            throw new Exception("There is no type that deals such damage");
        }
    }
}

