using SmartKiwi.Models;
using SmartKiwi.Services;
using SmartKiwi.Controller;
using System.Runtime.InteropServices;

var queueList = new List<Queue>(); //Criando lista de filas

//criando filas para teste
var prio = new Queue("Prio", 3); 
var comun = new Queue("Comum", 3);
//Adicionando filas na lista
queueList.Add(prio);
queueList.Add(comun);

//iniciando checkIn

//checkIn.Exec();

//iniciando os clientes para teste

var checkIn = new checkIn();

checkIn.Exec("zexinho", prio);
checkIn.Exec("luizinho", prio);
checkIn.Exec("huginho", prio);
checkIn.Exec("patinhas", prio);
checkIn.Exec("jenifer", comun);
checkIn.Exec("otto", comun);
checkIn.Exec("chaves", comun);
checkIn.Exec("florinda", comun);




//iniciando queueController
var newQeueController = new QueueController(queueList, 1);
//Iniciando atendiento
var atendimento = new AttendantUi(newQeueController);

atendimento.Exec();



// var app = new App();

// app.Run();


