using SmartKiwi.Models;
using SmartKiwi.Models.Queuef;

var queueList = new List<Queue>(); //Criando lista de filas

//criando filas para teste
var prio = new Queue("0", 3); 
var comun = new Queue("1", 2);
//Adicionando filas na lista
queueList.Add(prio);
queueList.Add(comun);

//iniciando checkIn
//var checkIn = new checkIn(queueList);
//checkIn.Exec();

//iniciando os clientes para teste
var newClient1 = new Client("danilo", 1);
var newClient2 = new Client("Otto", 2);
var newClient3 = new Client("jenider", 3);

prio.Enqueue(newClient2);
comun.Enqueue(newClient1);
comun.Enqueue(newClient3);

// iniciando queueController
var newQeueController = new QueueController(queueList, 1);

var atendente = new Attendante(newQeueController);

atendente.CallNext();

atendente.CallNext();

atendente.CallNext();
atendente.CallNext();







