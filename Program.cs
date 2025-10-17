using Microsoft.VisualBasic;
using SmartKiwi.Models;
using SmartKiwi.Models.Queuef;

var queueList = new List<Queue>();

var prio = new Queue("0", 3);
var comun = new Queue("1", 2);

queueList.Add(prio);
queueList.Add(comun);
var checkIn = new checkIn(queueList);

checkIn.Exec();

var Client = queueList[0].Dequeue();

System.Console.WriteLine(Client.WaiteTicket);

