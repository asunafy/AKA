using System.Runtime.InteropServices;
using System.Collections.Generic;

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
    public class ClassLibrary2
    {
        //use interop services to import c dll. The orginal c source code can be found under Lib folder
        [DllImport(@"Lib\random.dll")]
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
        public static int concat(int m, int n)
        {
            string a = System.Convert.ToString(m, 2);//dec->Binary
            string b = System.Convert.ToString(n, 2);//dec->Binary
            string c = a + b;
            return (System.Convert.ToInt32(c, 2)); //Binary->dec
        }

        public static int XOR(int m, int n)
        {
            return (m ^ n);
        }

        public static int ComputeA_1(int b, int PW)
        {
            return (Hash(concat(b, PW)));
        }
        public static int ComputePID_1(int ID, int b)
        {
            return (Hash(concat(ID, b)));
        }
        public static int ComputeB_1(int PID, int x)
        {
            return (Hash(concat(PID, x)));
        }
        public static int ComputeC_1(int ID, int A)
        {
            return (Hash(concat(ID, A)));
        }
        public static int ComputeD_1(int B, int PID, int A)
        {
            return (XOR(B,Hash(XOR(PID,A))));
        }
        public static int ComputePSID_1(int SID, int d)
        {
            return (Hash(concat(SID, d)));
        }
        public static int ComputeBS_1(int PSD, int y)
        {
            return (Hash(concat(PSD, y)));
        }
        public static int ComputeB_2(int D, int C)
        {
            return (XOR(D, C));
        }
        public static int ComputeF_1(int B, int N1)
        {
            return (XOR(B, N1));
        }
        public static int ComputePij_1(int B, int N1, int SID, int PID, int TS)
        {
            return (Hash(XOR(B, Hash(concat(N1, concat(SID, concat(PID, TS)))))));
        }
        public static int ComputeCID_1(int ID, int B, int N1, int TS)
        {
            return (XOR(ID, Hash(concat(B, concat(N1, TS)) << 2)));
        }
        public static int ComputeG_1(int b, int B, int N1, int TS)
        {
            return (XOR(b, Hash((concat(B, concat(N1, concat(TS, 3)))))));
        }
        public static int ComputeJ_1(int BS, int N2)
        {
            return (XOR(BS, N2));
        }
        public static int ComputeK_1(int N2, int BS, int P, int TS)
        {
            return (Hash(concat(N2, concat(BS, concat(P, TS)))));
        }
        public static int ComputeL_1(int SID, int BS, int N2, int TS)
        {
            return (XOR(SID, Hash(concat(BS, concat(N2, TS)) << 2)));
        }
        public static int ComputeM_1(int d, int BS, int N1, int TS)
        {
            return (XOR(d, (Hash(concat(BS, concat(N1, TS)) << 2) + 3)));
        }

        public static int ComputeN_1(int F, int B)
        {
            return (XOR(F, B));
        }
        public static int ComputeID_1(int CID, int B, int N1, int TS)
        {
            return (XOR(CID, (Hash(concat(B, concat(N1, TS)) << 2))));
        }
        public static int ComputeSID_1(int L, int BS, int N2, int TS)
        {
            return (XOR(L, (Hash(concat(BS, concat(N2, TS)) << 2))));
        }
        public static int Compute_b_1(int G, int B, int N1, int TS)
        {
            return (XOR(G, ((Hash(concat(B, concat(N1, TS)) << 2) + 3))));
        }
        public static int Compute_d_1(int M, int BS, int N1, int TS)
        {
            return (XOR(M, (Hash(concat(BS, concat(N1, TS)) << 2) + 3)));
        }
        public static int ComputeP_1(int N1, int N3, int SID, int N2, int BS)
        {
            return (XOR(N1, XOR(N3, Hash(concat(SID, concat(N2, BS))))));
        }
        public static int ComputeQ_1(int N1, int N3)
        {
            return (Hash(XOR(N1, N3)));
        }
        public static int ComputeR_1(int N2, int N3, int ID, int N1, int B)
        {
            return (XOR(N2, XOR(N3, Hash(concat(ID, concat(N1, B))))));
        }
        public static int ComputeV_1(int N2, int N3)
        {
            return (Hash(XOR(N2, N3)));
        }
        public static int Compute_XOR_N1_N3(int P, int SID, int N2, int BS)
        {
            return (XOR(P, Hash(concat(SID, concat(N2, BS)))));
        }
        public static int ComputeQ_1(int XOR_N1_N3)
        {
            return (Hash(XOR_N1_N3));
        }
        public static int Compute_XOR_N2_N3(int R,int ID, int N1, int B)
        {
            return (XOR(R, Hash(concat(ID, concat(N1, B)))));
        }
        public static int ComputeV_1(int XOR_N2_N3)
        {
            return (Hash(XOR_N2_N3));
        }
        public static int ComputeSK_1(int XOR_N2_N3, int N1, int TS)
        {
            return (Hash(concat(XOR(XOR_N2_N3, N1), TS)));
        }
        public static int ComputeSK_2(int XOR_N1_N3,int N2, int TS)
        {
            return (Hash(concat(XOR(XOR_N1_N3,N2),TS)));
        }
        public static int ComputeSK_3(int N1, int N2, int N3, int TS)
        {
            return (Hash(concat(XOR(XOR(N1,N2), N3), TS)));
        }
        public static int ComputeB_3(int D, int ID, int A)
        {
            return (XOR(D,Hash(concat(ID, A))));
        }

        public static int Compute_Hash_y_x(int E, int B)
        {
            return (XOR(E, B));
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
        public static bool EndSession(int ts1, int ts2,int dm, int ds)
        {
            bool flag = false;
            int h1 = System.Convert.ToInt32(ts1.ToString().Substring(1, 2));
            int m1 = System.Convert.ToInt32(ts1.ToString().Substring(3, 2));
            int s1 = System.Convert.ToInt32(ts1.ToString().Substring(5, 2));
            int h2 = System.Convert.ToInt32(ts2.ToString().Substring(1, 2));
            int m2 = System.Convert.ToInt32(ts2.ToString().Substring(3, 2));
            int s2 = System.Convert.ToInt32(ts2.ToString().Substring(5, 2));
            if ((ts1 > ts2)||(h2 > h1)||((h2 == h1) && (m2 - m1) > dm) || ((h2 == h1) && (m2 == m1) && (s2 - s1) > ds))
                flag = true;
            return flag;
        }
    }
}

