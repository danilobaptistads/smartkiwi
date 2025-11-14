using SmartKiwi.Models;
using SmartKiwi.Services;
using SmartKiwi.Controller;


// var queueList = new List<Queue>(); //Criando lista de filas

// // //criando filas para teste
//  var prio = new Queue("Prio"); 
//  var comun = new Queue("Comum");
//   var comun2 = new Queue("Comum2");
// // //Adicionando filas na lista

// queueList.Add(comun);
// queueList.Add(prio);
// queueList.Add(comun2);


// // //iniciandocheckin
// // var checkIn = new checkIn();

// // checkIn.Exec("zexinho", prio);
// // checkIn.Exec("luizinho", prio);
// // checkIn.Exec("huginho", prio);
// // checkIn.Exec("patinhas", prio);
// // checkIn.Exec("margarida", prio);
// // checkIn.Exec("pluto", prio);
// // checkIn.Exec("donald", prio);
// // checkIn.Exec("jenifer", comun);
// // checkIn.Exec("otto", comun);
// // checkIn.Exec("Danilo", comun);
// // checkIn.Exec("Alef", comun);



// // // //iniciando queueController
// // var newQeueController = new QueueController(queueList, 1);
// // //Iniciando atendiento


// // var atendimento = new AttendantUi(newQeueController);
// //  atendimento.Exec();

// comun2.IsPriorityQueue();

// var queuebuider = new QueueBuilder(queueList);

// System.Console.Clear();
// foreach(var queue in queueList)
// {
//     System.Console.WriteLine($"{queue.Name}, prioridade {queue.Priority}, é prioridade? {queue.PriorityQueue}");
// }
// queuebuider.SetPriorityQueueFirst();
// queuebuider.AssignPriorityByOrder();
// System.Console.WriteLine("\nApos SetPriorityQueueFirst\n");
// foreach(var queue in queueList)
// {
//     System.Console.WriteLine($"{queue.Name}, prioridade {queue.Priority}, é prioridade? {queue.PriorityQueue}");
// }




var app = new App();

app.Run();

