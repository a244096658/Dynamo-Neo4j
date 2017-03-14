using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Newtonsoft.Json;

namespace Dynamo_Neo4j_Connection_New_Development
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

            Console.WriteLine(client.IsConnected); // Test the connection status with the Neo4j server. 

            List<List<string>> facility = new List<List<string>>();

            List<string> arr = new List<string>();

            string str = " {'BuildingName':' liu' , 'ProjectName':' wan'} ";

            arr.Add(str);
            facility.Add(arr);

            List<string> ifc_GUID = new List<string>();
            ifc_GUID.Add("123");

            string newStr = string.Join("", facility[0].ToArray());
            Console.WriteLine(newStr);

            string str1 = string.Join("", arr);

            for (int i = 0; i < ifc_GUID.Count; i += 1)
            {

                Facility facilityJson = JsonConvert.DeserializeObject<Facility>(string.Join("", facility[i].ToArray())); // Transform Json string to .NET objet
                facilityJson.GUID = ifc_GUID[i];

                Console.WriteLine(facilityJson.ProjectName);

                
                //Two points need to be aware: 1.{{ and }} will be format as string { and }  2. The value must be put ''. Even it is alreay a string.
                string MergeData2 = string.Format("(facility:FACILITY  {{ Name:'{0}', ProjectName:'{1}', GUID:'{2}'  }})", facilityJson.BuildingName, facilityJson.ProjectName, facilityJson.GUID);
                Console.WriteLine(MergeData2);
                
                client.Cypher
                    .Merge(MergeData2)  
                    .ExecuteWithoutResults();
            }

            

        }
    }


    public class COBie
    {
        public static void Merge(List<List<string>> facility, List<string> ifc_GUID) //Difference between list<string> and string[] ???
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();
      
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";

            for (int i = 0; i < ifc_GUID.Count; i += 1)
            {
                // 1. Transform Json string to .NET objet  2. facility[i] is a List, we convert it to Array and then to string. Because JsonConvert can only convert string format to c# object.
                // 3. facility is accepting element parameters. It is in a format of List--List0(key:value...), List1(key:value...)... So it is a List<List<string>>

                //Process original string to Json string. e.g: "{key1:value1, key2:value2}"  to "{key1:'value1',key2:'value2'}". 
                //As a consequence, we can use JsonConvert to convert this Json string to c# object.
                str1 = string.Join("", facility[i]);//Convert array to string
                str2 = str1.Replace(" ", "");  //Eliminate space.
                str3 = str2.Replace(":", ":'"); //Replace : with :'
                str4 = str3.Replace(",", "',"); //Replace , with ',
                str5 = str4.Insert(str4.Length - 1, "'"); // Insert ' in the end. 

                Facility facilityJson = JsonConvert.DeserializeObject<Facility>(str5); 
                facilityJson.GUID = ifc_GUID[i];

                //Two points need to be aware: 1.{{ and }} will be format as string { and }  2. The value must be put ''. Even it is alreay a string.
                string MergeData2 = string.Format("(facility:FACILITY  {{ Name:'{0}', ProjectName:'{1}', GUID:'{2}'  }})", facilityJson.BuildingName, facilityJson.ProjectName, facilityJson.GUID);
              
                client.Cypher
                    .Merge(MergeData2)
                    .ExecuteWithoutResults();
            }
        }
    }


    public class Test
    {
        public static string Merge(List<List<string>> facility, List<string> ifc_GUID) //Difference between list<string> and string[] ???
        {
            string data2 = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";

            for (int i = 0; i < ifc_GUID.Count; i += 1)
            {

                // 1. Transform Json string to .NET objet  2. facility[i] is a List, we convert it to Array and then to string. Because JsonConvert can only convert string format to c# object.
                // 3. facility is accepting element parameters. It is in a format of List--List0(key:value...), List1(key:value...)... So it is a List<List<string>>
                str1 = string.Join("", facility[i]);
                str2 = str1.Replace(" ", "");
                str3 = str2.Replace(":", ":'");
                str4 = str3.Replace(",","',");
                str5 = str4.Insert(str4.Length-1,"'");

                Facility facilityJson = JsonConvert.DeserializeObject<Facility>(str5);
                facilityJson.GUID = ifc_GUID[i];

                Console.WriteLine(facilityJson.ProjectName);


                //Two points need to be aware: 1.{{ and }} will be format as string { and }  2. The value must be put ''. Even it is alreay a string.
                data2 = string.Format("(facility:FACILITY  {{ Name:'{0}', ProjectName:'{1}', GUID:'{2}'  }})", facilityJson.BuildingName, facilityJson.ProjectName, facilityJson.GUID);
                
 
            }

            return data2;
        }
    }








    // Class to store Json data from Revit 

    public class Facility
    {

        public string BuildingName; //---Map to COBie---  Name
        public string ProjectName;//---Map to COBie--- ProjectName
        public string GUID;


        public Facility(string _BuildingName, string _ProjectName)
        {
            this.BuildingName = _BuildingName;
            this.ProjectName = _ProjectName;

        }
    }


    public class Floor
    {
        public string Name;//---Map to COBie---  Name
        public string Category;//---Map to COBie---  Name
        public string Elevation; //---Map to COBie---  Name

        public Floor(string _Name, string _Category, string _Elevation)
        {
            this.Name = _Name;
            this.Category = _Category;
            this.Elevation = _Elevation;

        }
    }


    public class Space
    {
        public string Name; //---Map to COBie---  Name
        public string Number; //---Map to COBie---  Name
        public string Category; //---Map to COBie---  Name
        public string CategoryDescription;//---Map to COBie---  Name
        public string Level; //---Map to COBie---  Name
        public string RoomTag; //---Map to COBie---  Name
        public string UnboundedHeight; //---Map to COBie---  Name
        public string Area;//---Map to COBie---  Name


        public Space(string _Name, string _Number, string _Category, string _CategoryDescription, string _Level, string _RoomTag, string _UnboundedHeight, string _Area)
        {
            this.Name = _Name;
            this.Number = _Number;
            this.Category = _Category;
            this.CategoryDescription = _CategoryDescription;
            this.Level = _Level;
            this.RoomTag = _RoomTag;
            this.UnboundedHeight = _UnboundedHeight;
            this.Area = _Area;


        }
    }


    public class Component
    {
        public string FamilyandType; //---Map to COBie---  Name
        public string Category; //---Map to COBie---  Name
        public string Room; //---Map to COBie---  Name
        public string SerialNumber; //---Map to COBie---  Name
        public string InstallationDate; //---Map to COBie---  Name
        public string WarrantyStartDate; //---Map to COBie---  Name
        public string TagNumber; //---Map to COBie---  Name
        public string BarCode; //---Map to COBie---  Name
        public string AssetIdentifier; //---Map to COBie---  Name


        public Component(string _FamilyandType, string _Category, string _Room, string _SerialNumber, string _InstallationDate, string _WarrantyStartDate, string _TagNumber, string _BarCode, string _AssetIdentifier)
        {
            this.FamilyandType = _FamilyandType;
            this.Category = _Category;
            this.SerialNumber = _SerialNumber;
            this.InstallationDate = _InstallationDate;
            this.WarrantyStartDate = _WarrantyStartDate;
            this.TagNumber = _TagNumber;
            this.BarCode = _BarCode;
            this.AssetIdentifier = _AssetIdentifier;

        }
    }



}
