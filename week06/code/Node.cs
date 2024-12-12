using System.Diagnostics;

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
        if (value == Data) {
            return;
        }

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
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
        if (value == Data) {
            return true;
        }

        if (value < Data) {
            // look to the left
            if (Left is null) {
                return false;
            }
            else {
                return Left.Contains(value);
            }
        }
        else {
            // look to the right
            if (Right is null) {
                return false;
            }
            else {
                return Right.Contains(value);
            }
        }
    }

    public int GetHeight()
    {
        if (Left is not null && Right is not null) {
            int leftTree = Left.GetHeight();
            int rightTree = Right.GetHeight();
            if (leftTree > rightTree) {
                return leftTree + 1;
            }
            else {
                return rightTree + 1;
            }
        }
        else if (Left is not null) {
            return Left.GetHeight() + 1;
        }
        else if (Right is not null) {
            return Right.GetHeight() + 1;
        }
        else {
            return 1;
        }
        
    }

    public void IsValid(){
        
    }
}