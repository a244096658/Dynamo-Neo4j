using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Neo4jClient;

namespace TestForNeo4jConnection
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

            Console.WriteLine(client.IsConnected); // Test the connection status with the Neo4j server. 

            string str1 = " {'Building Name':'smmm building ','Project Name':'da pang gua', GUID:'27PCKGLxT4mxtV9cs6mgBW'} "; //revit type string. 
            string str2 = " {'Project Name':'Duplex project','Building Name':'Big building '} ";

            string newstr = str1.Replace(" ", "");
            string strFromRevitToCOBieParameter = newstr.Replace("BuildingName", "Name");
            //Console.WriteLine(newstr);

            Facility jsonLa = JsonConvert.DeserializeObject<Facility>(newstr); // Transform Json string to .NET objet
            //jsonLa.GUID = "iii";


            string jsonString = JsonConvert.SerializeObject(jsonLa);


            JObject o3 = JsonConvert.DeserializeObject<JObject>(str1);

            JObject o4 = JsonConvert.DeserializeObject<JObject>(newstr);


            Console.WriteLine(o3);

            //Console.WriteLine(JToken.DeepEquals(o4, o3));
            //Neo4jClient.Cypher.ICypherFluentQuery neo4jObject = null;
            //Neo4jClient.Cypher.ICypherFluentQuery neo4jObjectMatchWithGUID = null;
            //IEnumerable<FACILITY> results = null;

           

            //neo4jObject = client.Cypher.Match("(facility:FACILITY)");
            //neo4jObjectMatchWithGUID = neo4jObject.Where((FACILITY facility )=>facility.GUID== "27PCKGLxT4mxtV9cs6mgBW");
            //results = neo4jObjectMatchWithGUID.Return(facility => facility.As<FACILITY>()).Results;

            //Console.WriteLine(results.Count<FACILITY>());


            //string strFromNeo4jToCOBieParameter = JsonConvert.SerializeObject(results.ToList<FACILITY>()[0]);
            //JObject jObjectFromNeo4jWithCOBieParameter = JsonConvert.DeserializeObject<JObject>(strFromNeo4jToCOBieParameter);

            //JObject jObjectFromRevitWithCOBieParameter = JsonConvert.DeserializeObject<JObject>(strFromRevitToCOBieParameter);

            //Boolean compareNeo4jAndRevit = true;

            //compareNeo4jAndRevit = JToken.DeepEquals(jObjectFromNeo4jWithCOBieParameter, jObjectFromRevitWithCOBieParameter);

            //if (compareNeo4jAndRevit)
            //{
            //    return;

            //}
            //else
            //{
            //    neo4jObjectMatchWithGUID.Set("facility={newfacility}")
            //      .WithParam("newfacility", JsonConvert.DeserializeObject<FACILITY>(strFromRevitToCOBieParameter))
            //      .ExecuteWithoutResults();

            //}

            //Console.WriteLine(compareNeo4jAndRevit);

        
            //foreach (var k in results )
            //{
            //    foreach (PropertyInfo p in k.GetType().GetProperties())
            //    {
            //        Console.WriteLine(p.Name);
            //        Console.WriteLine(p.GetValue(k));

            //    }
            //}



            //fo.Set("facility={newf}")
            //  .WithParam("newf", JsonConvert.DeserializeObject<FACILITY>(newstr))
            //  .ExecuteWithoutResults();
            //string[] sa = { "name":"liu","4"};
            //List<string> la = new List<string>(sa);











            //Console.WriteLine(jsonLa.GetType().GetProperties());


            //foreach (PropertyInfo propertyInfo in jo.GetType().GetProperties())
            //{
            //    Console.WriteLine(propertyInfo.GetValue());
            //}



        }
    }


    //Claim revit class. (Use revit parameter)
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

    //Class for Neo4j label, in order to enable the data fetch from Neo4j. (Use COBie parameter)
    public class FACILITY
    {
   
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public string GUID { get; set; }
    }







}
