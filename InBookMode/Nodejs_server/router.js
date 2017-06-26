
var requestHandlers = require("./requestHandlers");
var url = require("url");


function route(postData,request,response){

    var pathName = url.parse(request.url).pathname;
    console.log(`The request url is ${pathName}`);

    if(pathName==="/"){ // Determine the http request url, and reflect them to related requestHandlers function. Test what is the url for the ipad request. 

        requestHandlers.cypherMergeStart(postData,request,response);
   
        console.log("router start-->About to route a request for"+pathName);

    }

    else if(pathName==="/favicon.ico") {
        console.log("favicon.ico")
    }

    else{
        requestHandlers.notFound(response);
        
    };

    
};

exports.route = route;

