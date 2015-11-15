using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sha256_Study;

namespace Sha
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcHash();
            CalcHashReverse();            
            Console.ReadKey();


        }

        private static void CalcHashReverse()
        {
            string msg = "The quick brown fox jumps over the lazy ";
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(msg);
            Digest digest = new Digest();
            digest.H0 = 0xD7A8FBB3;
            digest.H1 = 0x07D78094;
            digest.H2 = 0x69CA9ABC;
            digest.H3 = 0xB0082E4F;
            digest.H4 = 0x8D5651E4;
            digest.H5 = 0x6D3CDB76;
            digest.H6 = 0x2D02D0BF;
            digest.H7 = 0x37C9E592;

            var sha = new Sha256DigestReverse();

            List<byte[]> bytesValues = sha.RevertHash(digest, bytes);
        }

        private static void CalcHash()
        {
            var sha = new Sha256Digest();
            string msg = "The quick brown fox jumps over the lazy dog";
            var bytes = ASCIIEncoding.ASCII.GetBytes(msg);
            Digest digest = sha.hash(bytes);
            Console.WriteLine("{0:X} {1:X} {2:X} {3:X} {4:X} {5:X} {6:X} {7:X}", digest.H0, digest.H1, digest.H2, digest.H3,
                digest.H4, digest.H5, digest.H6, digest.H7);


        }
    }
}
