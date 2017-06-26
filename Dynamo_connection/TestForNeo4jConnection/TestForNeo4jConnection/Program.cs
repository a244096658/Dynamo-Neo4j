using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Neo4jClient;
using System.Text.RegularExpressions;

namespace TestForNeo4jConnection
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

            string TwitterData = "{Command:Suggest ,Name: Liu ,Comment: The furniture in kitchen is broken}"; 
            //TwitterData format is {Command:*** , Name:***, Comment:****}. For Ask command,GUID and Lable should be included in Comment within < >.
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string strFromTwitter = "";
            string returnedData = "";

            str1 = TwitterData;
            str2 = str1.Replace(":", ":'"); //Replace : with :'
            str3 = str2.Replace(",", "',"); //Replace , with ',
            strFromTwitter = str3.Replace("}", "'}"); // Insert ' in the end. 
            TWITTER twitterObj = JsonConvert.DeserializeObject<TWITTER>(strFromTwitter);

            //Match 22-length GUID by Regex.
            Regex numberRegex = new Regex(@"\w{22}");
            //Match uppercase label by Regex.
            Regex capitalLetterRegex = new Regex(@"[A-Z]{2,}");

            string IFCGUID = numberRegex.Match(strFromTwitter).Value;
            string LABEL = capitalLetterRegex.Match(strFromTwitter).Value;
            //Match match = numberRegex.Match(s);

            Console.WriteLine(twitterObj.Command);

            //if (twitterObj.Command.Contains("Ask"))
            //{


            //    if (LABEL == "COMPONENT")
            //    {


            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObject = null;
            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObjectMatchWithGUID = null;
            //        IEnumerable<COMPONENT> results = null;

            //        neo4jObject = client.Cypher.Match("(n:COMPONENT)");
            //        neo4jObjectMatchWithGUID = neo4jObject.Where((COMPONENT n) => n.GUID == IFCGUID);
            //        results = neo4jObjectMatchWithGUID.Return(n => n.As<COMPONENT>()).Results;
            //        returnedData = JsonConvert.SerializeObject(results.ToList<COMPONENT>()[0]);

            //    }
            //    else if (LABEL == "FACILITY")
            //    {

            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObject = null;
            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObjectMatchWithGUID = null;
            //        IEnumerable<FACILITY> results = null;

            //        neo4jObject = client.Cypher.Match("(n:FACILITY)");
            //        neo4jObjectMatchWithGUID = neo4jObject.Where((FACILITY n) => n.GUID == IFCGUID);
            //        results = neo4jObjectMatchWithGUID.Return(n => n.As<FACILITY>()).Results;
            //        returnedData = JsonConvert.SerializeObject(results.ToList<FACILITY>()[0]);

            //    }

            //    else if (LABEL == "FLOOR")
            //    {

            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObject = null;
            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObjectMatchWithGUID = null;
            //        IEnumerable<FLOOR> results = null;

            //        neo4jObject = client.Cypher.Match("(n:FLOOR)");
            //        neo4jObjectMatchWithGUID = neo4jObject.Where((FLOOR n) => n.GUID == IFCGUID);
            //        results = neo4jObjectMatchWithGUID.Return(n => n.As<FLOOR>()).Results;
            //        returnedData = JsonConvert.SerializeObject(results.ToList<FLOOR>()[0]);

            //    }

            //    else if (LABEL == "SPACE")
            //    {

            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObject = null;
            //        Neo4jClient.Cypher.ICypherFluentQuery neo4jObjectMatchWithGUID = null;
            //        IEnumerable<SPACE> results = null;

            //        neo4jObject = client.Cypher.Match("(n:SPACE)");
            //        neo4jObjectMatchWithGUID = neo4jObject.Where((SPACE n) => n.GUID == IFCGUID);
            //        results = neo4jObjectMatchWithGUID.Return(n => n.As<SPACE>()).Results;
            //        returnedData = JsonConvert.SerializeObject(results.ToList<SPACE>()[0]);
            //    }


            //}

            //else if (twitterObj.Command.Contains("Suggest"))
            //{

            //    //client.Cypher
            //    // .Match("(e:EXTERNALSOURCE)")
            //    // .Where((EXTERNALSOURCE e) => e.Name == "Twitter")
            //    // .Create("(t:TWITTER {newtwitterObject})-[:BELONG_TO]->(e)")
            //    // .Create("(e)-[:INCLUDES]->(t)")
            //    // .WithParam("newtwitterObject", JsonConvert.DeserializeObject<TWITTER>(strFromTwitter))
            //    // .ExecuteWithoutResults();
            //    Console.WriteLine(strFromTwitter);

            //}

            //Console.WriteLine( returnedData);




        }
    }
 


        //onsole.WriteLine( faci.SiteName==null );

        //client.Cypher
        //   .Create("(faci2:FACILITY {newfaciObject})")
        //   .WithParam("newfaciObject", parent)
        //   .ExecuteWithoutResults();





        //Console.WriteLine(string.Join(" ",a));

        // Console.WriteLine(testfaci);

        //var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
        //client.Connect();

        //Console.WriteLine(client.IsConnected); // Test the connection status with the Neo4j server. 

        //string str1 = " {'Building Name':'smmm building ','Project Name':'da pang gua', GUID:'27PCKGLxT4mxtV9cs6mgBW'} "; //revit type string. 
        //string str2 = " {'Project Name':'Duplex project','Building Name':'Big building '} ";

        //string newstr = str1.Replace(" ", "");
        //string strFromRevitToCOBieParameter = newstr.Replace("BuildingName", "Name");
        ////Console.WriteLine(newstr);

        //Facility jsonLa = JsonConvert.DeserializeObject<Facility>(newstr); // Transform Json string to .NET objet
        ////jsonLa.GUID = "iii";


        //string jsonString = JsonConvert.SerializeObject(jsonLa);


        //JObject o3 = JsonConvert.DeserializeObject<JObject>(str1);

        //JObject o4 = JsonConvert.DeserializeObject<JObject>(newstr);


        //Console.WriteLine(o3);

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



    //Claim revit class. (Use revit parameter)
    public class Facility
    {

        public string BuildingName; //---Map to COBie---  Name
        public string ProjectName;//---Map to COBie--- ProjectName
        public string GUID;

        public string SiteName;
        public Facility(string _BuildingName, string _ProjectName,string _SiteName )
        {
            this.BuildingName = _BuildingName;
            this.ProjectName = _ProjectName;
            this.SiteName = _SiteName;
         
        }
    }

 


    public class NEWFACILITY  
    {
        

        [JsonProperty(PropertyName ="BuildingFirstName")] //here is jason property name, so revit parameter name
        public string Name { get; set; }// here is our class property name, so cobie parameter name.

        [JsonProperty("ProjectKaName")]
        public string ProjectName { get; set; }

        [JsonProperty("Area")]
        public string GrossArea { get; set; }

        public string GUID { get; set; }

        public string SiteName { get; set; }


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





    public class TWITTER
    {
        public string Command { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }



    }

    //Neo4j lable for AR and Twitter. 
    public class EXTERNALSOURCE
    {

        public string Name { get; set; }
        public string Facility { get; set; }
        public string Category { get; set; }


    }


    public class FACILITY
    {

        public string Name { get; set; }

        public string ProjectName { get; set; }
        public string Category { get; set; }
        public string GUID { get; set; }

        public string SiteNmae { get; set; }
        public string LinearUnits { get; set; }
        public string AreaUnits { get; set; }
        public string VolumeUnits { get; set; }
        public string CurrencyUnit { get; set; }
        public string AreaMeasurement { get; set; }
        public string Description { get; set; }
        public string ProjectDescription { get; set; }
        public string SiteDescription { get; set; }
        public string Phase { get; set; }

    }


    public class FLOOR
    {

        public string Name { get; set; }
        public string Category { get; set; }
        public string Elevation { get; set; }
        public string GUID { get; set; }

        public string Description { get; set; }
        public string Height { get; set; }

    }

    public class SPACE
    {

        public string Name { get; set; }
        public string Category { get; set; }
        public string FloorName { get; set; }
        public string Description { get; set; }
        public string RoomTag { get; set; }
        public string UsableHeight { get; set; }
        public string GrossArea { get; set; }
        public string Number { get; set; }
        public string GUID { get; set; }


    }


    public class COMPONENT
    {

        public string Name { get; set; }
        public string TypeName { get; set; }
        public string Space { get; set; }
        public string Category { get; set; }
        public string SerialNumber { get; set; }
        public string InstallationDate { get; set; }
        public string WarrantyStartDate { get; set; }
        public string TagNumber { get; set; }
        public string BarCode { get; set; }
        public string AssetIdentifier { get; set; }
        public string GUID { get; set; }

        public string Description { get; set; }


    }






}

