using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using XNode;

public class DialogueNode : Node
{
    [Input] public DialogueNode input;
    [Output] public DialogueNode output;

    public string id;

    public string speaker;
    public List<string> statements;

    // Must line up
    public List<string> outIds;
    public List<string> selections;

    public override object GetValue(NodePort port)
    {
        return id;
    }


    public List<DialogueNode> GetNodes()
    {
        List<DialogueNode> outputs = new List<DialogueNode>();

        foreach (NodePort port in Outputs.ElementAt(0).GetConnections())
        {
            outputs.Add(port.node as DialogueNode);
        }

        return outputs;
    }

    public DialogueNode GetNode(int i)
    {
        return Outputs.ElementAt(0).GetConnections()[i].node as DialogueNode;
    }

    public DialogueNode GetNextNode()
    {
        if (Outputs.ElementAt(0).GetConnections().Count > 0) return Outputs.ElementAt(0).GetConnections()[0].node as DialogueNode;
        else return null;
    }
}
