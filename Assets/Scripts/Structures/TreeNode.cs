public class TreeNode<T> : IEnumerable<TreeNode<T>>
{

    public T data { get; set; }
    public TreeNode<T> parent { get; set; }
    public ICollection<TreeNode<T>> children { get; set; }

    public TreeNode(T data)
    {
        this.data = data;
        this.children = new LinkedList<TreeNode<T>>();
    }

    public TreeNode<T> AddChild(T child)
    {
        TreeNode<T> childNode = new TreeNode<T>(child) { parent = this };
        this.children.Add(childNode);
        return childNode;
    }
}