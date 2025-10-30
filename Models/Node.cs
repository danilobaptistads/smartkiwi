namespace SmartKiwi.Models;

internal class Node
{
    internal Node NextNode;
    internal Client data;
    public Node(Client client)
    {
        NextNode = null;
        data = client;

    }

}