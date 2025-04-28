namespace AlgorithmsAndDataStructures;

class LinkedListNode<T> {
    // The element that is stored on this node.
    public T Data { get; set; }

    // The reference to the next node in the list.
    public LinkedListNode<T> Next { get; internal set; }
}

class LinkedList<T> {
    // The start node of this list
    // (usually called the list's head).
    private LinkedListNode<T> head;

    // The last node of this list.
    private LinkedListNode<T> end;

    // Current length of the list (for optimization).
    public int Length { get; private set; }

    public T ElementAt(int index) {
        // Get the node for the given index.
        LinkedListNode<T> node = this.GetNodeForIndex(index);

        // Return its data.
        return node.Data;
    }

    public void SetValue(int index, T value) {
        // Get the node for the given index.
        LinkedListNode<T> node = this.GetNodeForIndex(index);

        // Overwrite its data.
        node.Data = value;
    }

    public void Append(T value) {
        // Create a new node for the new value.
        var newNode = new LinkedListNode<T>() { Data = value };

        // Special case: The list is currently empty.
        // In that case, the new node becomes the new start of the list.
        if (this.head == null) {
            this.head = newNode;
            this.end = newNode;
        }
        // Normal case: Append the new node at the end of the list.
        else {
            this.end.Next = newNode;
            this.end = newNode;
        }

        this.Length++;
    }

    public void InsertAt(int index, T value) {
        // Get the node at the given index.
        LinkedListNode<T> node = this.GetNodeForIndex(index);

        // Create the new node to be inserted.
        var newNode = new LinkedListNode<T>();

        // Insert `newNode` between `node` and its successor.
        newNode.Next = node.Next;
        node.Next = newNode;

        // Now, we "shift" the data from `node` to `newNode`,
        // and then write `value` to `node.`
        newNode.Data = node.Data;
        node.Data = value;

        this.Length++;
    }

    public void RemoveAt(int index) {
        // Make sure the list is not already empty.
        if (this.head == null) {
            throw new IndexOutOfRangeException();
        }

        // Special case: We want to remove the head.
        if (index == 0) {
            this.RemoveHead();
        }
        // Normal case: Remove from the middle of the list
        // (this also includes removing the last element).
        else {
            this.RemoveFromMiddle(index);
        }

        this.Length--;
    }

    public void RemoveFromEnd() {
        this.RemoveAt(this.Length - 1);
    }

    public bool Search(T value) {
        LinkedListNode<T> currentNode = this.head;
        while (currentNode != null) {
            if (currentNode.Data.Equals(value)) {
                return true;
            }
            
            currentNode = currentNode.Next;
        }

        return false;
    }

    private LinkedListNode<T> GetNodeForIndex(int index) {
        if (index >= this.Length) {
            throw new IndexOutOfRangeException("index");
        }

        // We start with the head of the list.
        int i = 0;
        LinkedListNode<T> currentNode = this.head;

        // Traverse the list until we reach the index.
        while (currentNode != null) {
            if (i == index) {
                return currentNode;
            }

            currentNode = currentNode.Next;
            i++;
        }

        throw new IndexOutOfRangeException("index");
    }

    private void RemoveHead() {
        this.head = this.head.Next;

        // This might have cleared the entire list.
        // If so, we have to update `end`.
        if (this.head == null) {
            this.end = null;
        }
    }
    private void RemoveFromMiddle(int index) {
        // Get the node that should be remove as well as it's predecessor.
        // Note: This can be optimized. Think about how and implement an 
        // optimized version at home!
        LinkedListNode<T> predNode = this.GetNodeForIndex(index - 1);
        LinkedListNode<T> removeNode = this.GetNodeForIndex(index);

        // To remove the node, simply make the list "skip" it by setting
        // `predNode`'s successor to be `removeNode`'s successor.
        predNode.Next = removeNode.Next;

        // The removed node might have been the end of the list.
        // If so, we have to update our `end` reference.
        if (predNode.Next == null) {
            this.end = predNode;
        }
    }
}
