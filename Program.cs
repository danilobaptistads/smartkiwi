using SmartKiwi.Models;
using SmartKiwi.Services;
using SmartKiwi.Controller;
using System.Runtime.InteropServices;

var queueList = new List<Queue>(); //Criando lista de filas

//criando filas para teste
var prio = new Queue("Prio", 3); 
var comun = new Queue("Comum", 2);
//Adicionando filas na lista
queueList.Add(prio);
queueList.Add(comun);

//iniciando checkIn
//var checkIn = new checkIn(queueList);
//checkIn.Exec();

//iniciando os clientes para teste
var newClient4 = new Client("zexinho", 1);
var newClient5 = new Client("luizinho", 2);
var newClient3 = new Client("luluzinha", 3);
var newClient2 = new Client("Otto", 4);
var newClient1 = new Client("danilo", 5);

var newClient6 = new Client("jenider", 6);
var newClient7 = new Client("hughuino", 7);
var newClient8 = new Client("patinhas", 8);
var newClient9 = new Client("florinda", 9);
var newClient10 = new Client("chavez", 10);
var newClient11 = new Client("kiko", 11);
var newClient12 = new Client("rafa", 12);

prio.Enqueue(newClient4);
prio.Enqueue(newClient5);
prio.Enqueue(newClient3);
prio.Enqueue(newClient2);
prio.Enqueue(newClient1);

comun.Enqueue(newClient6);
comun.Enqueue(newClient7);
comun.Enqueue(newClient8);
comun.Enqueue(newClient9);
comun.Enqueue(newClient10);
comun.Enqueue(newClient11);
comun.Enqueue(newClient12);







// iniciando queueController
 var newQeueController = new QueueController(queueList, 1);

 var atendimento = new AttendantUi(newQeueController);

 atendimento.Exec();



// var app = new App();

// app.Run();


