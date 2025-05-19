namespace AlgorithmsAndDataStructures;

class BinaryMinHeap<T> where T : IComparable<T> {
    // Dynamic array used to store the data of this binary heap.
    private DynamicArray<T> data;

    public BinaryMinHeap() {
        this.data = new DynamicArray<T>(10);
    }

    public int Length {
        get {
            return data.Length;
        }
    }

    public void Insert(T item) {
        // Append new element at the end of the array.
        this.data.Append(item);

        // Bubble-up until the heap property is statisfied.
        this.BubbleUp();
    }

    public T Extract() {
        // Extract result.
        T result = this.data.ElementAt(0);

        // Replace root with last element.
        this.Swap(0, this.data.Length - 1);
        this.data.RemoveFromEnd();

        // Bubble-down until the heap property is statisfied.
        this.BubbleDown();

        // Return.
        return result;
    }

    public T Peek() {
        return this.data.ElementAt(0);
    }

    private void BubbleUp() {
        int currentIndex = this.data.Length - 1;
        int currentParentIndex = (currentIndex - 1) / 2;

        while (currentIndex > 0) {
            // Compare item with it's parent.
            T elem = this.data.ElementAt(currentIndex);
            T parent = this.data.ElementAt(currentParentIndex);
            if (elem.CompareTo(parent) < 0) {
                Swap(currentIndex, currentParentIndex);

                currentIndex = currentParentIndex;
                currentParentIndex = (currentIndex - 1) / 2;
            }
            else {
                // If the heap property was not violated in this step,
                // we are done.
                return;
            }
        }
    }

    private void BubbleDown() {
        int currentIndex = 0;

        while (true) {
            int leftChildIndex = 2 * currentIndex + 1;
            int rightChildIndex = 2 * currentIndex + 2;

            // We are done when the current index is a leaf.
            if (leftChildIndex >= this.data.Length) {
                return;
            }

            // Otherwise, determine which of the current node and its
            // children is the smallest one.
            int smallestIndex = currentIndex;

            if (this.data.ElementAt(leftChildIndex).CompareTo(this.data.ElementAt(smallestIndex)) < 0) {
                smallestIndex = leftChildIndex;
            }

            if (rightChildIndex < this.data.Length && this.data.ElementAt(rightChildIndex).CompareTo(this.data.ElementAt(smallestIndex)) < 0) {
                smallestIndex = rightChildIndex;
            }

            // Swap if the smallest was not the current node.
            if (smallestIndex != currentIndex) {
                Swap(smallestIndex, currentIndex);
                currentIndex = smallestIndex;
            }
            // Otherwise, we are done.
            else {
                return;
            }
        }
    }

    private void Swap(int idx1, int idx2) {
        T temp = this.data.ElementAt(idx1);

        this.data.SetValue(idx1, this.data.ElementAt(idx2));
        this.data.SetValue(idx2, temp);
    }
}
