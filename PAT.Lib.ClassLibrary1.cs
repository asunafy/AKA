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
    public class ClassLibrary1
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
            return (m^n); 
        }

        public static int ComputeA_1(int b, int PW)
        {
            return (Hash(concat(b,PW)));
        }
        public static int ComputeB_1(int ID, int x)
        {
            return (Hash(concat(ID,x)));
        }
        public static int ComputeB_2(int D, int ID, int A)
        {
            return (XOR(D,Hash(concat(ID,A))));
        }
        public static int ComputeC_1(int ID, int y, int A)
        {
            return (Hash(concat(concat(ID,Hash(y)),A)));
        }
        public static int ComputeD_1(int B, int ID, int A)
        {
            return (XOR(B,Hash(concat(ID,A))));

        }
        public static int ComputeE_1(int B, int y, int x)
        {
            return (XOR(B,Hash(concat(y, x))));
        }
        public static int ComputeG_1(int B, int A, int N)
        {
            return (Hash(concat(concat(B,A),N)));
        }
        public static int ComputeP_1(int E, int y, int N, int SID)
        {
            return (XOR(E,Hash(concat(concat(Hash(y),N), SID))));
        }
        public static int ComputeCID_1(int A, int B, int F, int N)
        {
            return (XOR(A, Hash(concat(concat(B, F), N))));
        }
        public static int ComputeN2_1(int K, int SID, int y)
        {
            return (XOR(K,Hash(concat(SID,y))));
        }
        public static int ComputeN_2(int F, int y)
        {
            return (XOR(F, Hash(y)));
        }
        public static int ComputeB_3(int P, int y, int N, int SID, int x)
        {
            return (XOR(XOR(P, Hash(concat(concat(Hash(y), N), SID))), Hash(concat(y, x))));
        }
        public static int ComputeA_2(int CID, int B, int F, int N)
        {
            return (XOR(CID, Hash(concat(concat(B, F), N))));
        }
        public static int ComputeQ_1(int N1, int N3, int SID, int N2)
        {
            return (XOR(XOR(N1,N3),Hash(concat(SID,N2))));
        }
        public static int ComputeR_1(int A, int B, int N1, int N2, int N3)
        {
            return (XOR(Hash(concat(A,B)), Hash(XOR(XOR(N1,N2), N3))));
        }
        public static int ComputeV_1(int A, int B, int N1, int N2, int N3)
        {
            return (Hash(concat(Hash(concat(A,B)), Hash(XOR(N1,XOR(N2,N3))))));
        }
        public static int ComputeT_1(int N2, int N3, int A, int B, int N1)
        {
            return (XOR(XOR(N2,N3), Hash(concat(A,concat(B,N1)))));
        }
        public static int ComputeV_2(int hash, int XOR_N1_N3, int N2)
        {
            return (Hash(concat(hash, Hash(XOR(XOR_N1_N3, N2)))));
        }
        public static int ComputeV_3(int A, int B, int XOR_N2_N3, int N1)//论文有误
        {
            return (Hash(concat(Hash(concat(A, B)), Hash(XOR(N1,XOR_N2_N3)))));         
        }
        public static int ComputeHash_SID_y(int SID, int y)
        {
            return (Hash(concat(SID,y)));
        }
        public static int ComputeHash_x_y(int x, int y)
        {
            return (Hash(concat(x,y)));
        }
        public static int ComputeF_1(int hash, int N)
        {
            return (XOR(hash,N));
        }
        public static int ComputeK_1(int hash, int N)
        {
            return (XOR(hash, N));
        }
        public static int ComputeN_1(int K, int hash)
        {
            return (XOR(K, hash));
        }
        public static int ComputeM_1(int hash, int N)
        {
            return (Hash(concat(hash,N)));
        }
        public static int ComputeXOR_N1_N3(int Q, int SID, int N2)
        {
            return (XOR(Q,Hash(concat(SID,N2))));
        }
        public static int ComputeHash_A_B(int R, int XOR_N1_N3, int N2)
        {
            return (XOR(R, Hash(XOR(XOR_N1_N3,N2))));
        }
        public static int ComputeXOR_N2_N3(int T, int A, int B, int N1)
        {
            return(XOR(T, Hash(concat(A, concat(B, N1)))));
         }
        public static int ComputeSK_1(int A, int B, int N1, int XOR_N2_N3)
        {
            return (Hash(concat(Hash(concat(A,B)), XOR(N1,XOR_N2_N3))));
        }
        public static int ComputeSK_2(int hash, int N2, int XOR_N1_N3)
        {
            return (Hash(concat(hash, XOR(N2,XOR_N1_N3))));
        }
        public static int ComputeSK_3(int A, int B, int N1, int N2, int N3)
        {
            return (Hash(concat(Hash(concat(A, B)), XOR(N1, XOR(N2, N3)))));
        }
        public static int ComputeHash_y_x(int E, int B)
        {
            return (XOR(E, B));
        }
        public static int ComputeC_2(int ID, int hash_y, int A)
        {
            return (Hash(concat(concat(ID, hash_y), A)));
        }
        public static int ComputeE_2(int B, int hash_y_x)
        {
            return (XOR(B, hash_y_x));
        }

}        
}

