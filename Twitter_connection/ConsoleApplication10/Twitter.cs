using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using System.Net;
using System.Net.Mail;
using Autodesk.DesignScript.Runtime;




namespace LINKBIM
{
    public class Twitter
    {
        public static void PublishTweet(string tweetContent) //这是出现在Dynamo中的 function block
        {
            //Add this Auth to every static function.
            Auth.SetUserCredentials("xx", "x", "x", "x");


            Tweet.PublishTweet(tweetContent); //#1  Publish a new tweet   
                                              // ##1 *Improve* Make more parameter of publish tweet, adding location/image/video..

        }

        public static void ReplyTweet(string text, string screeName, long tweetId)
        {
            Auth.SetUserCredentials("x", "x", "x", "x");
            long tweetIdReplyTo = tweetId;
            //var tweetReplyTo = Tweet.GetTweet(tweetIdReplyTo);//通过TweetId拿到整个宏观Tweet
            //string screenName = tweetReplyTo.CreatedBy.ScreenName; //通过宏观Tweet，拿到SreenName
            string UserScreenName = screeName;
            string textReplyTo = string.Format("@{0} {1}", UserScreenName, text); //回复的text，要结合sreen name与text一起

            Tweet.PublishTweetInReplyTo(textReplyTo, tweetIdReplyTo);

        }



        public static List<string> MentionTimeline()
        {
            Auth.SetUserCredentials("x", "x", "x", "x");
            // timeline是以"list"的形式拿到的，故先定义list，再给list赋值。
            // timeline是类似list的结合体形式，所以还需要ToList()一下来转成list
            List<Tweetinvi.Core.Interfaces.ITweet> mentionTimeline = new List<Tweetinvi.Core.Interfaces.ITweet>();
            mentionTimeline = Timeline.GetMentionsTimeline().ToList<Tweetinvi.Core.Interfaces.ITweet>();


            // dynamo不能直接读出结合体list，需要使用foreach遍历timeline list，然后一个个add进新list
            // timeline的list类型，dynamo不能读懂，所以还需转换成string类型的list
            List<string> timeline = new List<string>();
            foreach (var insideTweet in mentionTimeline)
            {
                timeline.Add(insideTweet.ToString());

            }

            return timeline;

        }

        public static List<string> SearchTweet(string searchText)
        {
            Auth.SetUserCredentials("x", "x", "x", "x");
            //search参数分两种: 1.关键字（可以是@，#，text）  2.SearchParameter Object，添加更多限制参数
            var matchingTweets = Search.SearchTweets(searchText);
            List<string> tweets = new List<string>();

            foreach (var inTweet in matchingTweets)
            {
                tweets.Add(inTweet.ToString());

            }
            return tweets;

        }
    }
    //var reply = Tweet.PublishTweetInReplyTo("Have a nice day", newTweet);//#2 Reply to own tweet

    //Console.WriteLine("Run ok");

    ////Console.WriteLine("{0}     {1}", 1, 2);  1,2就根据前面的替换符的格式来，有多少空格就加多少空格，有逗号就加逗号，只是一个格式而已。

    ////var tweetIdReplyTo = 3006764554;
    ////var tweetReplyTo = Tweet.GetTweet(tweetIdReplyTo);
    ////var screenNameReplyTo = tweetReplyTo.CreatedBy.ScreenName; //或者如果我直接使用screenName，而不是从tweetId来获取。

    //// #3  Make a @ mention to another user
    //string screenNameReplyTo = "FMSYSTEM_TEST";
    //var tweetIdReplyTo = 3006764554;
    //string text = "Hey,how are you?";
    //string textReplyTo = string.Format("@{0} {1}", screenNameReplyTo, text); //Text need combine the @screenName with text we wanna say
    ////string.format : 将Object，按照替换符的格式，来变成string。 即为带格式的string
    //// ToStirng(),只是将某一个Object类型变成string类型，不带格式。

    //Tweet.PublishTweetInReplyTo(textReplyTo, tweetIdReplyTo); //@最终格式


    // #4 Timeline

    //List<Tweetinvi.Core.Interfaces.ITweet> timelineTweets = new List<Tweetinvi.Core.Interfaces.ITweet>();

    //timelineTweets = Timeline.GetMentionsTimeline().ToList<Tweetinvi.Core.Interfaces.ITweet>(); //先定义List，然后再给List赋值


    //Console.WriteLine(timelineTweets.GetType());

    //Console.WriteLine(timelineTweets);

    //List<string> tweetPool = new List<string>();


    //foreach (var insideTweet in timelineTweets)  //使用foreach，把返回Tweet List中的Tweet都打印出来。
    //{

    //    tweetPool.Add(insideTweet.ToString());

    //}

    //return tweetPool;


    // #5 search Tweet

    //var matchingTweets = Search.SearchTweets("@FMSYSTEM_TEST"); //输入参数分两种: 1.关键字（可以是@，#，text）  2.SearchParameter Object，添加更多限制参数

    //foreach(var mTweet in matchingTweets)
    //{
    //    Console.WriteLine(mTweet);
    //    Console.WriteLine(mTweet.GetType());
    //}

    //static void PublishTweet()
    //{
    //    Tweet.PublishTweet("Hello World again!");
    //    Console.WriteLine("OK");


    //}






    public class Email
    {
        public static void SendMail(string mailAdress, string subject, string body) //Adress three parameter which recieve input from Dynamo
        {

            string fromAddress = "XXXXX"; //Type in your email address here
            string mailPassword = "XXXXX";       // Type in your password here. Mail id password from where mail will be sent.
            string messageBody = body;


            // Create smtp connection.
            SmtpClient client = new SmtpClient();
            client.Port = 587;//outgoing port for the mail.
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);


            // Fill the mail form.
            var send_mail = new MailMessage();

            send_mail.IsBodyHtml = true;
            //address from where mail will be sent.
            send_mail.From = new MailAddress("lxxnewcgr@gmail.com");
            //address to which mail will be sent.           
            send_mail.To.Add(new MailAddress(mailAdress));
            //subject of the mail.
            send_mail.Subject = subject;

            send_mail.Body = messageBody;
            client.Send(send_mail);

        }

    }
    public class Plan
    {
        public static string MaintenancePlan(string time, string location, string problemDescription, string elementInformation)
        {
            string text = string.Format("The time you need to maintain is {0}. The location of the building is {1}. \nThe problem is displayed here:\n{2}. \nThe target element information is followed below:\n{3}. \nThank you very much! \nBest Regards, \nLiu Xi ", time, location, problemDescription, elementInformation);

            return text;



        }

    }

    public class DataType
    {

        //Return mutiple values
        [MultiReturn(new[] { "FamilyParameter", "ElementParameter" })]

        public static Dictionary<string, string> DecideOverallParameterType(string parameterName)
        {
            string[] familyParameters = { "Assembly Code", "Cost", "Description", "Keynote", "Manufacturer", "Model", "Type", "Type Mark", "URL" };

            List<string> familyParameterList = new List<string>(familyParameters);

            string[] elementParameters = { "Radius", "Base Offset", "Top Offset", "Structural Material" };
            List<string> elementParameterList = new List<string>(elementParameters);


            string value1 = "";
            string value2 = "";

            if (familyParameterList.Contains(parameterName))
            {
                // 想在if内部对外部变量的值进行修改，外部变量必须一开始就赋一个初始值。 不能直接 var ***。 需要 var *** = " " ;
                value1 = parameterName;
                value2 = null;
            }
            else if (elementParameterList.Contains(parameterName))
            {
                value1 = null;
                value2 = parameterName;
            }

            // 对于有return值得函数，return的值必须是在整体外部。不能在if，for等循环内部return值。 可以在if等函数内部进行值的修改，最后在外部return值。
            return new Dictionary<string, string>
                {
                    {"FamilyParameter",value1 },
                    {"ElementParameter",value2 }
                };

        }


    }

    public class FamilyDataType
    {
        [MultiReturn(new[] { "stringParameter", "doubleParameter" })]

        public static Dictionary<string, string> DecideFamilyParameterType(string parameterName)
        {
            string[] familyStringParameters = { "Assembly Code", "Description", "Keynote", "Manufacturer", "Model", "Type", "Type Mark", "URL" };
            List<string> familyStringParameterList = new List<string>(familyStringParameters);

            string[] familyDoubleParameters = { "Cost" };
            List<string> familyDoubleParameterList = new List<string>(familyDoubleParameters);

           
            string value1 = "";
            string value2 = "";

            if (familyStringParameterList.Contains(parameterName))
            {
                value1 = parameterName;
                value2 = null;
            }
            else if (familyDoubleParameterList.Contains(parameterName))
            {
                value1 = null;
                value2 = parameterName;
            }


            return new Dictionary<string, string>
                {
                    {"stringParameter",value1 },
                    {"doubleParameter",value2 }
                };


        }


    }

    public class ElementDataType
    {
        [MultiReturn(new[] { "doubleParameter", "MaterialParameter" })]

        public static Dictionary<string, string> DecideFamilyParameterType(string parameterName)
        {
            
            string[] elementDoubleParameters = { "Radius", "Base Offset", "Top Offset" };
            List<string> elementDoubleParameterList = new List<string>(elementDoubleParameters);

            string[] elementMaterialIdParameters = { "Structural Material" };
            List<string> elementMaterialIdParameterList = new List<string>(elementMaterialIdParameters);

            string value1 = "";
            string value2 = "";

            if (elementDoubleParameterList.Contains(parameterName))
            {
                value1 = parameterName;
                value2 = null;
            }
            else if (elementMaterialIdParameterList.Contains(parameterName))
            {
                value1 = null;
                value2 = parameterName;
            }


            return new Dictionary<string, string>
                {
                    {"doubleParameter",value1 },
                    {"MaterialParameter",value2 }
                };


        }


    }



}

