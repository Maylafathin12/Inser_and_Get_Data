using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inser_and_Get_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID                  : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password                 : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan          : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik k untuk Terhubung ke Database : ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'k':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data Source= MAYLA; " +
                                "initial catalog = {0}; " +
                                "User ID = {1}; password = {2};"
                                 ;
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat seluruh data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Delete Data");
                                        Console.WriteLine("4. Mencari Data");
                                        Console.WriteLine("5. Mengubah Data");
                                        Console.WriteLine("6. Keluar");
                                        Console.WriteLine("\nEnter your choice (1-3) : ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("RESTAURANT AVATAR\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT MENU\n");
                                                    Console.WriteLine("Masukkan KODE MENU:");
                                                    string kode_menu = Console.ReadLine();
                                                    Console.WriteLine("Masukkan NAMA MENU :");
                                                    string nama_menu = Console.ReadLine();
                                                    Console.WriteLine("Masukkan STOCK MENU :");
                                                    string stok_menu = Console.ReadLine(); 
                                                    Console.WriteLine("Masukkan HARGA MENU :");
                                                    string harga_menu = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(kode_menu, nama_menu, stok_menu,harga_menu, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\n cfcwgyuea");
                                                    }
                                                }

                                                break;
                                            case '2':
                                                {
                                                    Console.Write("\nMasukkan kode menu" + " Menu yang akan dihapus: ");
                                                    int kode_menu = Convert.ToInt32(Console.ReadLine());
                                                    Console.WriteLine();
                                                    if (conn.(kode_menu) == false)
                                                        Console.WriteLine("\nId bARANG " + kode_menu + " dihapus ");
                                                }
                                                break;
                                            case '6':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\n Invalid Option");
                                                }
                                                break;   
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered. ");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\n Invalid Option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses menggunakan database tersebut");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From dbo.menuu", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string kode_menu, string nama_menu, string stok_menu, string harga_menu, SqlConnection con)
        {
            string str = "";
            str = "insert into dbo.menuu (kode_menu, nama_menu, stok_menu, harga_menu) "
                + "values(@kode,@nma,@stok,@hrg)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("kode", kode_menu));
            cmd.Parameters.Add(new SqlParameter("nma", nama_menu));
            cmd.Parameters.Add(new SqlParameter("stok", stok_menu));
            cmd.Parameters.Add(new SqlParameter("hrg", harga_menu));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan.");
        }
    }
}
