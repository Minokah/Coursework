using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "DialogueGraph", menuName = "Dialogue/New Dialogue Graph", order = 1)]
public class DialogueGraph : NodeGraph
{
    // Get node using id if possible
    public DialogueNode GetNode(string id)
    {
        foreach (DialogueNode node in nodes)
        {
            if (node.id.Equals(id)) return node;
        }

        return null;
    }

    // Feeling lucky? Get a random node
    public DialogueNode GetRandomNode() {
        int r = UnityEngine.Random.Range(0, nodes.Count);
        return nodes[r] as DialogueNode;
    }
}
