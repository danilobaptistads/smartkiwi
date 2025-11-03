using System.Net.NetworkInformation;
using SmartKiwi.Models;
using SmartKiwi.Services;

var queueList = new List<Queue>(); //Criando lista de filas

//criando filas para teste
var prio = new Queue("Prio", 10); 
var comun = new Queue("Comum", 2);
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
// var atendimento = new AttendantUi(newQeueController);

// atendimento.Exec();
var maxPriority = queueList[0].currentPriority;
var aging = new Aging(queueList, 1, maxPriority);

comun.lastCall = DateTime.Now;

var i = 0;
var checkcycle = new CycleChecker(1);
while (true)
{

    checkcycle.exec();

    ++i;
    System.Console.WriteLine($"rodou {i} vezes");
    Thread.Sleep(1000);
    
    
}



// var app = new App();

// app.Run();


