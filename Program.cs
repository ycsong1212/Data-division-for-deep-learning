using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Console_File_Division
{
    public class Picture
    {
        // Define a picture class to save information related to the picture
        public FileInfo file;
        public int label; //the class number of the picture
        public int seq; //Number each picture of the category, divided by the method of sequence remainder 
        public Picture(FileInfo file,int label)
        {
            this.file = file;
            this.label = label;
        }
    }

    class Program
    {
        public static int GetFlag(string str)
        {
            /*Initial dataset is named in a special criteria，It contains a string like "flag01", 
             * including flag and a num represents the angle, this may not be helpful or you need 
             * to adjust for your own requirement*/
            // This function aims at extracting the angle number of the picture
            int result = 0;
            string[] sArray = str.Split('_');
            string flag_str = "";
            for(int i=0;i<sArray.Length;i++)
            {
                if (sArray[i].Contains("flag"))
                    flag_str = sArray[i];
            }
            flag_str = flag_str.Remove(0,4);
            result = Str2Num(flag_str);
            return result;
        }

        public static int Str2Num(string str)
        {
            // This function aims at convert a string to number
            int result = 0;
            char[] c = str.ToCharArray();
            int multi = 1;
            for (int i = c.Length - 1; i >= 0; i--)
            {
                result += (c[i] - 48) * multi;
                multi *= 10;
            }
            return result;
        }

        static void Main(string[] args)
        {            
            string path = "Your data's location";
            List<Picture> pictures = AddPictures(path);
            int[] labels_count = new int[16]; 
            foreach(Picture p in pictures)
            {
                switch(p.label)
                {
                    case 1:
                        labels_count[0]++;
                        break;
                    case 2:
                        labels_count[1]++;
                        break;
                    case 3:
                        labels_count[2]++;
                        break;
                    case 4:
                        labels_count[3]++;
                        break;
                    case 5:
                        labels_count[4]++;
                        break;
                    case 6:
                        labels_count[5]++;
                        break;
                    case 7:
                        labels_count[6]++;
                        break;
                    case 8:
                        labels_count[7]++;
                        break;
                    case 9:
                        labels_count[8]++;
                        break;
                    case 10:
                        labels_count[9]++;
                        break;
                    case 11:
                        labels_count[10]++;
                        break;
                    case 12:
                        labels_count[11]++;
                        break;
                    case 13:
                        labels_count[12]++;
                        break;
                    case 14:
                        labels_count[13]++;
                        break;
                    case 15:
                        labels_count[14]++;
                        break;
                    case 16:
                        labels_count[15]++;
                        break;
                }
            }

            for(int i=0;i<16;i++)
            {
                List<Picture> pictures_temp = new List<Picture>();
                int num = 0;
                foreach (Picture p in pictures)
                {
                    if (p.label == i + 1)
                    {
                        pictures_temp.Add(p);
                    }
                }

                foreach (Picture p in pictures_temp)
                {
                    p.seq = num++;
                }

                foreach(Picture p in pictures)
                {
                    foreach(Picture p2 in pictures_temp)
                    {
                        if (p2.file == p.file)
                            p.seq = p2.seq;
                    }
                }
            }

            foreach(Picture p in pictures)
            {
                
                if (p.seq % 10 == 9)
                    p.file.CopyTo("The location you hope to save the valid dataset" + p.file.Name);
                else if(p.seq%10==8||p.seq%10==7||p.seq%10==6)
                    p.file.CopyTo("The location you hope to save the test dataset" + p.file.Name);
                else
                    p.file.CopyTo("The location you hope to save the train dataset" + p.file.Name);
                
            }

            Console.WriteLine("Done");

        }

        private static List<Picture> AddPictures(string dir)
        {
            // Add picture to the list
            DirectoryInfo d = new DirectoryInfo(dir);
            List<Picture> pictures = new List<Picture>();
            List<string> list = new List<string>();
            List<int> flags = new List<int>();
            FileInfo[] files = d.GetFiles();
            for (int i=0;i<files.Length;i++)
            {
                list.Add(files[i].Name);
                flags.Add(GetFlag(list[i]));
                pictures.Add(new Picture(files[i], flags[i]));
            }
            return pictures;
        }
    }
}
