using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Program
    {
        public static List<string> Main(List<string> revitUniqueId)
        {
            List<string> guidList = new List<string>();


            foreach (string id in revitUniqueId)
            {


                int elementId = int.Parse(id.Substring(37), System.Globalization.NumberStyles.AllowHexSpecifier);
                //Console.WriteLine("elementId is " + elementId);

                int last_32_bits = int.Parse(id.Substring(28, 8), System.Globalization.NumberStyles.AllowHexSpecifier);
                //Console.WriteLine("last_32_bits is " + last_32_bits);

                int xor = last_32_bits ^ elementId;

                string guid = id.Substring(0, 28) + xor.ToString("x8");

                guidList.Add(guid);

            }

            return guidList;

            //Convert revit uniqueId to .NET GUID/

                //string revitId = "60f91daf-3dd7-4283-a86d-24137b73f3da-0001fd0b";
                //string subId = revitId.Substring(0, 36); // Substring from 0 - 36.  60f91daf-3dd7-4283-a86d-24137b73f3da
                //Console.WriteLine(subId);

                //string subId2 = revitId.Substring(37); //Start from 37 to the end.  0001fd0b
                //Console.WriteLine(subId2);

                //int elementId = int.Parse(subId2, System.Globalization.NumberStyles.AllowHexSpecifier);
                //Console.WriteLine("elementId is " +elementId);

                //string subId3 = revitId.Substring(28, 8);//Start from 28, length is 8.  7b73f3da
                //Console.WriteLine(subId3);

                //int last_32_bits = int.Parse(subId3, System.Globalization.NumberStyles.AllowHexSpecifier);
                //Console.WriteLine("last_32_bits is " + last_32_bits);

                //int xor = last_32_bits ^ elementId;

                //Console.WriteLine("xor is "+xor);
                //Console.WriteLine(" xor.ToString(x8) is " + xor.ToString("x8"));
                //string guid = revitId.Substring(0, 28) + xor.ToString("x8"); // (0,28) is 60f91daf-3dd7-4283-a86d-2413
                //Console.WriteLine("guid is " + guid);


            //Sample
                //string a = "0001fd0b";

                //int b = int.Parse(a,System.Globalization.NumberStyles.AllowHexSpecifier);

                //Console.WriteLine(b); // Show as 130315


                //int c = 130315;

                //string d = c.ToString("x8");
                //Console.WriteLine(d); // Show as 0001fd0b

        }
    }
}
