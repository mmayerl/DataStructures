namespace AlgorithmsAndDataStructures;

class DoublyLinkedListNode<T> {
    // The element that is stored on this node.
    public T Data { get; set; }

    // The reference to the next node in the list.
    internal DoublyLinkedListNode<T> Next { get; set; }

    // The reference to the previous node in the list.
    internal DoublyLinkedListNode<T> Previous { get; set; }
}

class DoublyLinkedList<T> {
    // The start node of this list
    // (usually called the list's head).
    private DoublyLinkedListNode<T> head;

    // The last node of this list.
    private DoublyLinkedListNode<T> end;

    // Current length of the list (for optimization).
    public int Length { get; private set; }

    public DoublyLinkedListNode<T> NodeAt(int index) {
        int i = 0;
        DoublyLinkedListNode<T> currentNode = this.head;

        // Traverse the list until we reach the index.
        while (currentNode != null) {
            if (i == index) {
                return currentNode;
            }

            currentNode = currentNode.Next;
            i++;
        }

        // If we come here, we have arrived at the end of the list
        // without reaching the requested index.
        throw new IndexOutOfRangeException("index");
    }

    public T ElementAt(int index) {
        // Get the node for the given index.
        DoublyLinkedListNode<T> node = this.NodeAt(index);

        // Return its data.
        return node.Data;
    }

    public void SetValue(int index, T value) {
        // Get the node for the given index.
        DoublyLinkedListNode<T> node = this.NodeAt(index);

        // Overwrite its data.
        node.Data = value;
    }

    public void Append(T value) {
        // Create a new node for the new value.
        var newNode = new DoublyLinkedListNode<T>() { Data = value };

        // Special case: The list is currently empty.
        // In that case, the new node becomes the new start of the list.
        if (this.head == null) {
            this.head = newNode;
            this.end = newNode;
        }
        // Normal case: Append the new node at the end of the list.
        else {
            newNode.Previous = this.end;
            this.end.Next = newNode;
            this.end = newNode;
        }

        this.Length++;
    }

    public void InsertAt(int index, T value) {
        // Get the node at the given index.
        DoublyLinkedListNode<T> node = this.NodeAt(index);

        // Insert.
        this.InsertBefore(node, value);
    }

    public void InsertBefore(DoublyLinkedListNode<T> node, T value) {
        // Create the new node to be inserted.
        var newNode = new DoublyLinkedListNode<T>();

        // Insert `newNode` between `node` and its successor.
        newNode.Next = node.Next;
        newNode.Previous = node;

        node.Next = newNode;
        newNode.Next.Previous = newNode;

        // Now, we "shift" the data from `node` to `newNode`,
        // and then write `value` to `node.`
        newNode.Data = node.Data;
        node.Data = value;

        this.Length++;
    }

    public void RemoveAt(int index) {
        // Get node.
        DoublyLinkedListNode<T> node = this.NodeAt(index);

        // Remove.
        this.RemoveNode(node);
    }

    public void RemoveFromEnd() {
        this.RemoveNode(this.end);
    }

    public void RemoveNode(DoublyLinkedListNode<T> node) {
        // Make sure this is a legal node.
        if (node == null) {
            throw new ArgumentException("node");
        }

        // Make sure the list is not already empty.
        if (this.head == null) {
            throw new IndexOutOfRangeException();
        }

        // Special case: We want to remove the head.
        if (this.head == node) {
            this.RemoveHead();
        }
        // Normal case: Remove from the middle of the list
        // (this also includes removing the last element).
        else {
            this.RemoveFromMiddle(node);
        }
    }

    public bool Search(T value) {
        DoublyLinkedListNode<T> currentNode = this.head;
        while (currentNode != null) {
            if (currentNode.Data.Equals(value)) {
                return true;
            }
            
            currentNode = currentNode.Next;
        }

        return false;
    }

    private void RemoveHead() {
        this.head = this.head.Next;
        this.head.Previous = null;

        // This might have cleared the entire list.
        // If so, we have to update `end`.
        if (this.head == null) {
            this.end = null;
        }

        this.Length--;
    }
    private void RemoveFromMiddle(DoublyLinkedListNode<T> node) {
        // To remove the node, simply make the list "skip" it.
        node.Previous.Next = node.Next;

        if (node.Next != null) {
            node.Next.Previous = node.Previous;
        }

        // The removed node might have been the end of the list.
        // If so, we have to update our `end` reference.
        if (node.Previous.Next == null) {
            this.end = node.Previous;
        }

        this.Length--;
    }
}
