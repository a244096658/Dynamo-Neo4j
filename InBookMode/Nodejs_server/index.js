
var server = require("./server");  // server.js become the module server.  ./server is the relative address for the server.js file
// We store the whole server.js file to the variable server.  So then server will has all the exported/public attributes and function defined in server.js 

var router = require("./router");

var requestHandlers = require("./requestHandlers");


server.serverStart(router.route);

