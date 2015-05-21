var express = require('express');
var app = express();

app.use(express.static('public'));

app.use('/js/libs', express.static('bower_components/angular'));
app.use('/js/libs', express.static('bower_components/angular-animate'));
app.use('/js/libs', express.static('bower_components/angular-touch'));
app.use('/js/libs', express.static('bower_components/angular-ui-bootstrap-bower'));
app.use('/js/libs', express.static('bower_components/underscore'));

app.use('/css', express.static('bower_components/angular-ui-bootstrap-bower'));
app.use('/css', express.static('bower_components/bootstrap/dist/css'));

app.use('/fonts', express.static('bower_components/bootstrap/dist/fonts'));

var server = app.listen(3000, function () {

  var host = server.address().address;
  var port = server.address().port;

  console.log('Example app listening at http://%s:%s', host, port);

});