using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;



namespace Neo4j
{
    public class Cypher
    {

       

        // ***NEW DEVELOPMENT***
        // The block specifically for creating , according to the input message. So can create single node, or nodes with relationship.
        // Merge. Can only merge a single node.
        public static void MERGE(string MergeData)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

            client.Cypher
                    .Merge(MergeData) //Merge can also be used for creating the nodes with relationship.
                    .ExecuteWithoutResults();

        }


        // Create nodes. Allow creating multiple nodes. or multiple nodes with relationships.
        public static void CREATE(params string[] createData)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

            foreach (string i in createData)
            {
                client.Cypher
                    .Create(i)
                    .ExecuteWithoutResults();

            }

        }



        //Make update of the node, get input from NodeMessage (above)
        // SET can update one single property or multiple properties.
        // SET can update an existing property or add properties to this node.
        public static void SET(string matchData,string identifier, params string[] PropertyOrValue)
        {

            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

            //string setDataRevised = string.Format("({0})", SetData);

            //Update. It is good and safe use paramater to pass the value. But it is also ok to just use string text to pass value.
            for (int i = 0; i <= PropertyOrValue.Length-2; i += 2)
            {
                string setMessage = string.Format("{0}.{1}={{ {2} }}",identifier,PropertyOrValue[i],"pram");

                client.Cypher
               .Match(matchData) //Match the node
               .Set(setMessage)
               .WithParam("pram", PropertyOrValue[i+1])
               //.WithParam("propertyValue", value)
               .ExecuteWithoutResults();


            }
          

        }




        //#4 Delete single node
        // Currently only make Delete one single node.
        public static void DELETE(string matchData, params string[] identifier)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

            // Split the matchMessage,format is--> "({0}:{1} {{ {2}:'{3}',{4} }})". 
            // So to gain the identifier by spliting this string,the first item is identifier. 

            //string[] matchArray = matchData.Split(new char[] { '(', ':' }, StringSplitOptions.RemoveEmptyEntries);
            //string identifier = string.Format("{0}", matchArray[0]);

            foreach (var iden in identifier)
            {
                client.Cypher
             .Match(matchData)
             .Delete(iden) //Delete format is : Delete("identifier"). So we need to string.format the identifier. To add the " ". 
             .ExecuteWithoutResults();

            }
          
        }

        //#5 Read node data. This time, only read Movie data.()
        // Make the string input to the class label 
        public static List<string> READ(string matchData, string label,  params string[] property)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();

           //Convert the input string of label to class.
           //The class format is Namespace.class so it is Neo4j.Movie for example.
           // The class that we want to convert to should be pre-defined one. We can not randomly convert any string to a specific class.
            //string labelToClassString = string.Format("{0}.{1}", "Neo4j", label);

            //Type labelToClassType = Type.GetType(labelToClassString);

            //object labelToClass = Activator.CreateInstance(labelToClassType);

            //Movie realClass = (Movie)labelToClass;

            //var result = client.Cypher
            //           .Match(match)
            //           .Return(user => user.As<Movie>())
            //           .Results;


            // We can not assign null to var like: var result = null. This is not valid.
            // So we add (dynamic) , and it seems to work
            var  result=(dynamic) null ;

            // "({0}:{1} {{ {2} }})"

            // Split the matchMessage,format is--> "({0}:{1} {{ {2}:'{3}',{4} }})". 
            // So to gain the identifier by spliting this string,the first item is identifier. 

            //string[] matchArray = matchData.Split(new char[] { '(', ':' }, StringSplitOptions.RemoveEmptyEntries);
            //string identifier = string.Format("{0}", matchArray[0]);

            
           

            // Currently we can not convert a input to a pure class. So I use switch to distingush the lable input and select the associate method.

            //Neo4jClient.Cypher.ICypherResultItem n;

            switch (label)
            {
                case "Movie":
                        result = client.Cypher
                       .Match(matchData)
                       .Return(n => n.As<Movie>()) //The identifer in the matchMessage should be the same as here. So they both need to be n
                       .Results;                   // But I did not figure how to consistent the n here with identifier in matchMessage. 
                                                   // So in matchMessage, we must use n as identifier.
                                                              
                    break;

                case "Person":
                       result = client.Cypher
                       .Match(matchData)
                       .Return(n => n.As<Person>())
                       .Results;
                    break;

                case "People":
                    result = client.Cypher
                    .Match(matchData)
                    .Return(n => n.As<People>())
                    .Results;
                    break;

                case "Student":
                    result = client.Cypher
                    .Match(matchData)
                    .Return(n => n.As<Student>())
                    .Results;
                    break;


                default:
                    break;

            // We can add more class here 

            }

            // There are mutiple nodes object in result and multiple property in params string[] property
            // So we should double foreach to loop all the node.property

            List<string> resultList = new List<string>();

            foreach (var re in result)
            {
                foreach (var pro in property)
                {
                    string value = string.Format("{0}:{1}", pro, re[pro].ToString()); //Make the output more friendly for example >> Title:Independent day.

                    resultList.Add(value);
                }
                
            }


            return resultList;

        }



    }




    public class Data
    {


        //#2
        // Make only the Node creation text message.  NB:The identifier with Node2 should be different
        // Make identifier as a input. 
        // Can we create more options for input as node may have more properties.& Make text field.
        // The output port in Dynamo will only display as String.(The output datatype, not the output variable name )
        // params string[] makes block in Dynamo have plus button.
        public static string Node(string identifier, string label, params string[] PropertyOrValue)
        {
            //The string for the last property/value when click the plus button.And then append this string to message string.
            // Use the method of separator to join the string. 
            string stringAdditional = "";

            for (int i = 0; i < PropertyOrValue.Length; i += 1)
            {
                if (i % 2 == 0)
                {
                    stringAdditional += PropertyOrValue[i] + ":" + "'";
                }
                else if (i != PropertyOrValue.Length - 1)
                {
                    stringAdditional += PropertyOrValue[i] + "'" + ",";

                }
                else
                {
                    stringAdditional += PropertyOrValue[i] + "'";

                }

            }

            string message = string.Format("({0}:{1} {{ {2} }})", identifier, label, stringAdditional);
            return message;


        }


        //Create relationship text message, including the full relationship name,properties....   
        //Relationship format --> [:INVITED]
        public static string Relationship(string identifier, string label, params string[] PropertyOrValue)
        {
            string stringAdditional = "";

            for (int i = 0; i < PropertyOrValue.Length; i += 1)
            {
                if (i % 2 == 0)
                {
                    stringAdditional += PropertyOrValue[i] + ":" + "'";
                }
                else if (i != PropertyOrValue.Length - 1)
                {
                    stringAdditional += PropertyOrValue[i] + "'" + ",";

                }
                else
                {
                    stringAdditional += PropertyOrValue[i] + "'";

                }

            }

            string message = string.Format("[{0}:{1} {{ {2} }}]", identifier, label, stringAdditional);
            return message;

        }

        //Create (Node1 - Relationship - Node2) text message.    
        public static string RelateNodes(string node1, string relationship, string node2)
        {
            string message = string.Format("{0}-{1}->{2}", node1, relationship, node2);
            return message;


        }




    }





    public class Movie
    {

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }



        public string tagline { get; set; }
        public string title { get; set; }
        public int released { get; set; }
        public int id { get; set; }
    }

    public class Person
    {
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        public string name { get; set; }
        public int id { get; set; }
        public int born { get; set; }
    }

    public class People
    {
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        public string name { get; set; }
        public int id { get; set; }
        public int born { get; set; }
    }


    public class Student
    {
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        public string name { get; set; }
        public int id { get; set; }
        public string born { get; set; }
        public string gender { get; set; }
        public string school { get; set; }
    }





    public class COBie
    {
        public static void Merge(List<string> facility, string[] ifc_GUID) //Difference between list<string> and string[] ???
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
            client.Connect();



            for (int i=0; i<ifc_GUID.Length;i+=1)
            {

                Facility facilityJson = JsonConvert.DeserializeObject<Facility>(facility[i].ToString()); // Transform Json string to .NET objet
                facilityJson.GUID = ifc_GUID[i];

                string MergeData = string.Format("(facility:FACILITY  { Name:{0}, ProjectName:{1}, GUID:{3} })",  facilityJson.BuildingName,facilityJson.ProjectName,facilityJson.GUID);

                //string message = string.Format("({0}:{1} {{ {2} }})", identifier, label, stringAdditional);
                client.Cypher
                    .Merge(MergeData) //Merge can also be used for creating the nodes with relationship.
                    .ExecuteWithoutResults();
            }
            
            //(facility:FACILITY {name:{a}, guid:{q}, length:{g}, area:{h}, volume:{i}, currency:{j}, link:{r},created_on:{c}, category:{d}, project_name:{e}, site_name:{f}, facility_desc:{k}, project_desc:{l}, site_desc:{m}, phase:{n} })

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












    //Previous code.

    //#6 I try to return multiple result. And make it to a master block, which can be used for many class/Label
    //[MultiReturn(new[] { "title", "name" })]

    //public static Dictionary<string, string> Read(string label, string property, string value)
    //{
    //    var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "250daowohao");
    //    client.Connect();

    //    string sentence9 = string.Format("(user:{0} {{ {1}:{2} }})", label, property, value);



    //    IEnumerable<Neo4j.Movie> movieResults =null;
    //    IEnumerable<Neo4j.Person> personResults=null;


    //    switch (label)
    //    {
    //        case "Movie":
    //            movieResults = client.Cypher
    //           .Match(sentence9)
    //           .Return(user => user.As<Movie>())
    //           .Results;
    //            break;

    //        case "Person":
    //            personResults = client.Cypher
    //           .Match(sentence9)
    //           .Return(user => user.As<Person>())
    //           .Results;

    //            break;

    //        default:
    //            break;

    //    }

    //    //List<string> movieTitle = new List<string>();

    //    string value1 = null;
    //    string value2 = null;


    //    foreach (var m in movieResults)
    //    {
    //        value1 = m.title;

    //    }

    //    //List<string> personName = new List<string>();

    //    foreach (var p in personResults)
    //    {
    //        value2 = p.name;


    //    }


    //    return new Dictionary<string, string>
    //        {
    //            {"title",value1 },
    //            {"name",value2 }
    //        };


    //}


    ////Match single node or even mutiple nodes.
    ////Match node- relationship - node 
    //public static string MATCH(params string[] matchData)
    //{
    //    string matchMessage = "";

    //    for (int i = 0; i <= matchData.Length - 1; i += 1)
    //    {
    //        if (i <= matchData.Length - 2)
    //        {
    //            matchMessage += string.Format(" {0} ", matchData[i]) + ",";

    //        }
    //        // The final one do not need a comma ,
    //        else
    //        {
    //            matchMessage += string.Format(" {0} ", matchData[i]);

    //        }


    //    }

    //    return matchMessage;

    //}



    //// Make the SET message.
    //// User may want to update more than one properties to the node. So we need params to make plus button for more input.
    //public static string SETProperties(string identifier, params string[] propertyOrValue)
    //{
    //    string stringAdditional = "";
    //    // Use the method of string.format and str+ to join the string.

    //    for (int i = 0; i <= propertyOrValue.Length / 2; i += 2)
    //    {
    //        if (i < propertyOrValue.Length / 2 && propertyOrValue.Length != 1)
    //        {
    //            stringAdditional += string.Format(" ' {0}.{1}= '{2}' ' ", identifier, propertyOrValue[i], propertyOrValue[i + 1]) + ",";

    //        }
    //        // The final one do not need a comma ,
    //        else
    //        {
    //            stringAdditional += string.Format(" ' {0}.{1}= '{2}' ' ", identifier, propertyOrValue[i], propertyOrValue[i + 1]);

    //        }


    //    }


    //    return stringAdditional;


    //}




}
