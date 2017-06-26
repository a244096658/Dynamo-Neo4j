var http = require("http");

var url = require("url");


function serverStart(route){ //Make a function as a parameter. 将函数作为参数传递。 route成为回调函数

    function onRequest(request,response){


    response.setHeader("Content-Type", "text/plain");
    response.setHeader("Access-Control-Allow-Origin", "*");

    console.log("HTTP Request received");

    var postData=""; //For accepting the income data, so the Json data from our HTML .

    request.addListener("data",function(data){ //The data is not accepted once, they are accepted piece by piece. 
        postData +=data;
    })

    request.addListener("end",function(){ //When data accepting is finished, run the route function. 

        route(postData,request,response);
    })

    

};

http.createServer(onRequest).listen(3030);
console.log("Server start at port 3030"); //Log in console when server function run correctly.


};

exports.serverStart = serverStart;//把serverStart这个方法暴露出来，成为public可访问的。

// The first serverStart is the function name. The second serverStart is the name that we export .

// Put all the server code inside the function serverStart. So in the index.js , we can run the server code. 


