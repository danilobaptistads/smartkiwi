// using SmartKiwi.Models;
// using SmartKiwi.Services;
// using SmartKiwi.Controller;


// var queueList = new List<Queue>(); //Criando lista de filas

// //criando filas para teste
// var prio = new Queue("Prio", 3); 
// var comun = new Queue("Comum", 2);
// //Adicionando filas na lista
// queueList.Add(prio);
// queueList.Add(comun);

// //iniciandocheckin
// var checkIn = new checkIn();

// checkIn.Exec("zexinho", prio);
// checkIn.Exec("luizinho", prio);
// checkIn.Exec("huginho", prio);
// checkIn.Exec("patinhas", prio);
// checkIn.Exec("margarida", prio);
// checkIn.Exec("pluto", prio);
// checkIn.Exec("donald", prio);
// checkIn.Exec("jenifer", comun);
// checkIn.Exec("otto", comun);
// checkIn.Exec("Danilo", comun);
// checkIn.Exec("Alef", comun);



// // //iniciando queueController
// var newQeueController = new QueueController(queueList, 1);
// //Iniciando atendiento


// var atendimento = new AttendantUi(newQeueController);
//  atendimento.Exec();


using SmartKiwi.Controller;

var app = new App();

app.Run();

