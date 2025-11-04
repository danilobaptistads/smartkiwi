using SmartKiwi.Controller;
using SmartKiwi.Models;
using SmartKiwi.Services;

var queueList = new List<Queue>(); //Criando lista de filas
var dinqueueList = new List<Queue>();

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
checkIn.Exec("margarida", prio);
checkIn.Exec("pluto", prio);
checkIn.Exec("donald", prio);
checkIn.Exec("jenifer", comun);
checkIn.Exec("otto", comun);
checkIn.Exec("chaves", comun);
checkIn.Exec("florinda", comun);




//iniciando queueController
var newQeueController = new QueueController(queueList, 1);
//Iniciando atendiento

// var maxPriority = queueList[0].currentPriority;
// var aging = new Aging(queueList, 1, maxPriority);
var pmatcher = new PrioritiesMatcher(queueList,dinqueueList);
//var atendimento = new AttendantUi(newQeueController);
//comun.lastCall = DateTime.Now;
//atendimento.Exec();


var hasPrioritieMatch = false;
System.Console.WriteLine($"TAmanho da fila dinamica: {dinqueueList.Count}");
var retornoPmachr = pmatcher.check(hasPrioritieMatch);

System.Console.WriteLine($"retorno da fun~çao : {retornoPmachr}\n");
System.Console.WriteLine($"TAmanho da fila dinamica: {dinqueueList.Count}");

prio.length = 0;

retornoPmachr = pmatcher.check(hasPrioritieMatch);

System.Console.WriteLine($"retorno da fun~çao : {retornoPmachr}\n");
System.Console.WriteLine($"TAmanho da fila dinamica: {dinqueueList.Count}");

System.Console.WriteLine($"retorno da fun~çao : {retornoPmachr}\n");
System.Console.WriteLine($"TAmanho da fila dinamica: {dinqueueList.Count}");








// var app = new App();

// app.Run();


