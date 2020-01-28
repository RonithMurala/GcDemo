using System;
using System.ComponentModel;
using System.IO;

namespace GcDemo
{

    public class DisposeSample
    {
        class MyResources : IDisposable
        {
           
            TextReader tr = null;
                   
            public MyResources(string path)
            {
           
                Console.WriteLine("Aquiring Managed Resources");
                tr = new StreamReader(path);

        
                Console.WriteLine("Aquiring Unmanaged Resources");

            }

            void ReleaseManagedResources()
            {
                Console.WriteLine("Releasing Managed Resources");
                if (tr != null)
                {
                    tr.Dispose();
                }
            }

            void ReleaseUnmangedResources()
            {
                Console.WriteLine("Releasing Unmanaged Resources");
            }

            public void ShowData()
            {
     
                if (tr != null)
                {
                    Console.WriteLine(tr.ReadToEnd() + " /some unmanaged data ");
                }
            }

            public void Dispose()
            {
                Console.WriteLine("Dispose called from outside");

                Dispose(true);

     
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                Console.WriteLine("Actual Dispose called with a " + disposing.ToString());    
                if (disposing == true)
                {

                    ReleaseManagedResources();
                }
                else
                {

                }   

     
                ReleaseUnmangedResources();

            }

            ~MyResources()
            {
                Console.WriteLine("Finalizer called");

                Dispose(false);
            }
        }
        public static void Main()
        {
            MyResources r = null;

            try
            {
                r = new MyResources(@"Files\test.txt");
                r.ShowData();
            }
            finally
            {
                r.Dispose();
            }
        }
    }
}
