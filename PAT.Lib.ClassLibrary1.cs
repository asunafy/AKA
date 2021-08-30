using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

//the namespace must be PAT.Lib, the class and method names can be arbitrary
namespace PAT.Lib
{
    /// <summary>
    /// You can use static library in PAT model.
    /// All methods should be declared as public static.
    /// 
    /// The parameters must be of type "int", "bool", "int[]" or user defined data type
    /// The number of parameters can be 0 or many
    /// 
    /// The return type can be void, bool, int, int[] or user defined data type
    /// 
    /// The method name will be used directly in your model.
    /// e.g. call(max, 10, 2), call(dominate, 3, 2), call(amax, [1,3,5]),
    /// 
    /// Note: method names are case sensetive
    /// </summary>
    public class ClassLibrary1
    {
        //use interop services to import c dll. The orginal c source code can be found under Lib folder
        [DllImport(@"Lib\cat.dll")]
        public static extern int RAND(int max, int min, int n);

        public static int Hash(int varInt)
        {   
            int MAX = 100;
            int size = 23;
            int tmp = (varInt + MAX) % size;
            return tmp;
        }       
        public static int log2(int x)
        {
            int l;
            for (l = 0; x > 1; x >>= 1, l++) ;
            return l;
        }
        // Function to concatenate the binary 
       // numbers and return the decimal result 
        public static int concat(int m, int n) 
        {
            string a = System.Convert.ToString(m, 2);//dec->Binary
            string b = System.Convert.ToString(n, 2);//dec->Binary
            string c = a + b;
            return (System.Convert.ToInt32(c, 2)); //Binary->dec
         }
        public static int concat(int m, int n, int p)
        {
            string a = System.Convert.ToString(m, 2);//dec->Binary
            string b = System.Convert.ToString(n, 2);//dec->Binary
            string c = System.Convert.ToString(p, 2);//dec->Binary
            string d = a + b + c;
            return (System.Convert.ToInt32(d, 2)); //Binary->dec
        }

        public static int XOR(int m, int n)
        {
            return (m^n); 
        }
        public static int XOR(int m, int n, int p)
        {
            return (m ^ n ^ p);
        }

        public static int Compute_Serinfor(int sid, int x)
        {
            return (Hash(concat(sid, x)));
        }
        public static int Compute_EncPass(int id, int p)
        {
            return (Hash(concat(id, Hash(p))));
        }
        public static int Compute_Userinfor(int encpass, int x)
        {
            return (Hash(concat(encpass, x)));
        }
        public static int Compute_A(int userinfor, int hash, int n)
        {
            return (XOR(userinfor, hash, n));
        }
        public static int Compute_Veru(int hash, int n)
        {
            return (Hash(concat(hash, n)));
        }
        public static int Compute_B(int serinfor, int n)
        {
            return (XOR(serinfor, n));
        }
        public static int Compute_Vers(int sid, int x, int n)
        {
            return (Hash(concat(Hash(concat(sid,x)), n)));
        }
        public static int Compute_Vers_1(int s, int n)
        {
            return (Hash(concat(s, n)));
        }
        public static int Compute_Ni2(int serinfor, int b)
        {
            return (XOR(serinfor, b));
        }
        public static int Compute_Ni1(int userinfor, int x, int a)
        {
            return (XOR(userinfor, Hash(x), a));
        }
        public static int Compute_SK(int A, int x, int n1,int n2, int n3)
        {
            return (Hash(XOR(Hash(concat(A, Hash(x))), Hash(XOR(n1, n2, n3)))));
        }
        public static int Compute_C(int n1, int n2, int n3, int sid)
        {
            return (XOR(n1, n3, Hash(XOR(sid,n2))));
        }
        public static int Compute_D(int a, int x, int sid, int n2)
        {
            return (XOR(Hash(concat(a, Hash(x))), Hash(XOR(sid, n2))));
        }
        public static int Compute_E(int n2, int n3, int a, int x)
        {
            return (XOR(n2,n3,Hash(concat(a, Hash(x)))));
        }
        public static int Compute_Ni1_Ni3(int c, int sid, int n2)
        {
            return (XOR(c, Hash(XOR(sid, n2))));
        }
        public static int Compute_Hash_A_x(int d, int sid, int n2)
        {
            return (XOR(d, Hash(XOR(sid, n2))));
        }
        public static int Compute_SK_1(int hash, int xor, int n2)
        {
            return (Hash(XOR(hash, Hash(XOR(xor, n2)))));
        }
        public static int Compute_Ni2_Ni3(int e, int a, int hash)
        {
            return (XOR(e, Hash(concat(a, hash))));
        }
        public static int Compute_SK_2(int a, int hash, int xor, int n1)
        {
            return (Hash(XOR(Hash(concat(a, hash)), Hash(XOR(n1, xor)))));
        }
        public static int Timestamp_1()
        {
            int Second = System.Convert.ToInt32(System.DateTime.Now.AddMinutes(3).Second.ToString());
            int Minute = System.Convert.ToInt32(System.DateTime.Now.AddMinutes(3).Minute.ToString());
            int Hour = System.Convert.ToInt32(System.DateTime.Now.AddMinutes(3).Hour.ToString());
            int TS = Second + 100 * Minute + 10000 * Hour;
            return TS;
        }
        public static int Timestamp()
        {
            int Second = System.Convert.ToInt32(System.DateTime.Now.Second.ToString());
            int Minute = System.Convert.ToInt32(System.DateTime.Now.Minute.ToString());
            int Hour = System.Convert.ToInt32(System.DateTime.Now.Hour.ToString());
            int TS = Second + 100 * Minute + 10000 * Hour + 1000000;
            return TS;
        }
        public static bool EndSession(int ts1, int ts2, int dm, int ds)
        {
            bool flag = false;
            int h1 = System.Convert.ToInt32(ts1.ToString().Substring(1, 2));
            int m1 = System.Convert.ToInt32(ts1.ToString().Substring(3, 2));
            int s1 = System.Convert.ToInt32(ts1.ToString().Substring(5, 2));
            int h2 = System.Convert.ToInt32(ts2.ToString().Substring(1, 2));
            int m2 = System.Convert.ToInt32(ts2.ToString().Substring(3, 2));
            int s2 = System.Convert.ToInt32(ts2.ToString().Substring(5, 2));
            if ((ts1 > ts2) || (h2 > h1) || ((h2 == h1) && (m2 - m1) > dm) || ((h2 == h1) && (m2 == m1) && (s2 - s1) > ds))
                flag = true;
            return flag;
        }
        public static int ModifyTimestamp(int ts1, int dm)
        {
            int Hour = System.Convert.ToInt32(ts1.ToString().Substring(1, 2));
            int Minute = System.Convert.ToInt32(ts1.ToString().Substring(3, 2));
            int Second = System.Convert.ToInt32(ts1.ToString().Substring(5, 2));
            if (Minute >= dm)
            {
                Minute = Minute - dm;
            }
            else
            {
                Hour = Hour - 1;
                Minute = Minute + 60 - dm;
            }
            int TS = Second + 100 * Minute + 10000 * Hour + 1000000;
            return TS;

        }

       static void Main(string[] args)
       {
           //byte[] hash;
          // using (Stream fs = File.OpenRead("checkme.doc"))
            //   hash = MD5.Create().ComputeHash(fs); // hash is 16 bytes long
           int SIDj, x,IDi, Pi, Ni1, Ni2, Ni3, Hash_x;
           SIDj = 89;
           x = 80;
           IDi = 60;
           Pi = 40;
           Ni1 = 20;
           Ni2 = 21;
           Ni3 = 19;
           Hash_x = Hash(x);
           int Serinforj, EncPassi, Userinfori, Ai, Verui, Bi, Versi, Versi_1, Verui_1, Ni1_1, Ni2_1, Ni3_1;
           int SKi, SKi_1, SKi_2, Ci, Di, Ei, Ni1_Ni3, Ni2_Ni3, Hash_A_x;

          

           Serinforj = Compute_Serinfor(SIDj, x);
           EncPassi = Compute_EncPass(IDi, Pi);
           Userinfori = Compute_Userinfor(EncPassi, x);
           Ai = Compute_A(Userinfori, x, Ni1);
           Verui = Compute_Veru(x, Ni1);
           Bi = Compute_B(Serinforj, Ni2);
           Versi = Compute_Vers(SIDj, x, Ni2);
           Ni2_1 = Compute_Ni2(Serinforj, Bi);
           Versi_1 = Compute_Vers(SIDj, x, Ni2_1);
           Ni1_1 = Compute_Ni1(Userinfori, x, Ai);
           Verui_1 = Compute_Veru(x, Ni1_1);
           SKi = Compute_SK(Ai, x, Ni1, Ni2, Ni3);
           Ci = Compute_C(Ni1, Ni2, Ni3, SIDj);
           Di = Compute_D(Ai, x, SIDj, Ni2);
           Ei = Compute_E(Ni2, Ni3, Ai, x);
           Ni1_Ni3 = Compute_Ni1_Ni3(Ci, SIDj, Ni2);
           Hash_A_x = Compute_Hash_A_x(Di, SIDj, Ni2);
           SKi_1 = Compute_SK_1(Hash_A_x, Ni1_Ni3, Ni2);
           Ni2_Ni3 = Compute_Ni2_Ni3(Ei, Ai, Hash_x);
           SKi_2 = Compute_SK_2(Ai, Hash_x, Ni2_Ni3, Ni1);


           int test1 = Hash(XOR(Hash(concat(Ai, Hash(x))), Hash(XOR(Ni1, Ni2, Ni3))));

           int t1 = XOR(Ei, Hash(concat(Ai, Hash(x))));

           int t2 = XOR(Ni2, Ni3);


           int test2 = Hash(XOR(Hash_A_x, Hash(XOR(Ni1_Ni3, Ni2))));

           int test3 = Hash(XOR(Hash(concat(Ai, Hash_x)), Hash(XOR(Ni1, Ni2_Ni3))));

           int ts1, ts2, dm, ds;
           ts1 = 1235053;
           ts2 = 1235046;
           dm = 2;
           ds = 0;
           bool flag = false;
           int h1 = System.Convert.ToInt32(ts1.ToString().Substring(1, 2));
           int m1 = System.Convert.ToInt32(ts1.ToString().Substring(3, 2));
           int s1 = System.Convert.ToInt32(ts1.ToString().Substring(5, 2));
           int h2 = System.Convert.ToInt32(ts2.ToString().Substring(1, 2));
           int m2 = System.Convert.ToInt32(ts2.ToString().Substring(3, 2));
           int s2 = System.Convert.ToInt32(ts2.ToString().Substring(5, 2));

           System.Console.WriteLine("h1:" + h1);
           System.Console.WriteLine("m1:" + m1);
           System.Console.WriteLine("s1:" + s1);
           System.Console.WriteLine("h2:" + h2);
           System.Console.WriteLine("m2:" + m2);
           System.Console.WriteLine("s2:" + s2);

          

           if ((ts1 > ts2) || (h2 > h1) || ((h2 == h1) && (m2 - m1) > dm) || ((h2 == h1) && (m2 == m1) && (s2 - s1) > ds))
               flag = true;

          ;

           System.Console.WriteLine("b:" + flag);

           System.Console.WriteLine("test1:" + test1);
           System.Console.WriteLine("test2:" + test2);
           System.Console.WriteLine("test3:" + test3);
           System.Console.WriteLine("t1:" + t1);
           System.Console.WriteLine("t2:" + t2);



           System.Console.WriteLine("Serinforj:" + Serinforj);
           System.Console.WriteLine("EncPassi:" + EncPassi);
           System.Console.WriteLine("Userinfori:" + Userinfori);
           System.Console.WriteLine("Ai:" + Ai);
           System.Console.WriteLine("Verui:" + Verui);
           System.Console.WriteLine("Bi:" + Bi);

           System.Console.WriteLine("Ni1:" + Ni1);
           System.Console.WriteLine("Ni1_1:" + Ni1_1);

           System.Console.WriteLine("Ni2:" + Ni2);
           System.Console.WriteLine("Ni2_1:" + Ni2_1);
           System.Console.WriteLine("Versi:" + Versi);
           System.Console.WriteLine("Versi_1:" + Versi_1);
           System.Console.WriteLine("Ni2:" + Ni2);
           System.Console.WriteLine("Ni2_1:" + Ni2_1);
           System.Console.WriteLine("Verui:" + Verui);
           System.Console.WriteLine("Verui_1:" + Verui_1);
           System.Console.WriteLine("SKi:" + SKi);
           System.Console.WriteLine("SKi_1:" + SKi_1);
           System.Console.WriteLine("SKi_2:" + SKi_2);













  
            System.Console.WriteLine("Hello World!");


            System.Console.ReadKey(true);
            
 
            
        }

} 
  
       

}

