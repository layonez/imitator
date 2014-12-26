using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace imitator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream("Const.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            //formatter.Serialize(stream, new Const());
            //stream.Close();
        }
    }
}
