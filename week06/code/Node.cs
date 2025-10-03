public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1: Do not insert duplicate values
        if (value == Data)
        {
            return; // Value already exists, do nothing.
        }

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else // This block now only handles 'value > Data'
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Problem 2: Implement Contains
        if (value == Data)
        {
            return true;
        }

        if (value < Data)
        {
            // If the value is smaller, it must be in the left subtree.
            // Return true if the left child is not null and contains the value.
            return Left is not null && Left.Contains(value);
        }
        else
        {
            // If the value is larger, it must be in the right subtree.
            // Return true if the right child is not null and contains the value.
            return Right is not null && Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // Problem 4: Implement GetHeight
        // Get the height of the left subtree. If Left is null, its height is 0.
        var leftHeight = Left?.GetHeight() ?? 0;
        
        // Get the height of the right subtree. If Right is null, its height is 0.
        var rightHeight = Right?.GetHeight() ?? 0;
        
        // The height of the tree rooted at this node is 1 (for this node) plus the
        // height of the taller of the two subtrees.
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}