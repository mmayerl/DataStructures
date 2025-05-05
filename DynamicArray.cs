namespace AlgorithmsAndDataStructures;

class DynamicArray<T> {
    // Factor by which the internal array will grow if
    // it runs out of capacity.
    private const double GROWTH_FACTOR = 2.0;

    // Internal array to hold our actual data.
    private T[] data;

    // Length of our dynamic array.
    public int Length { get; private set; }

    public DynamicArray(int initialCapacity) {
        // Create our array with the given initial capacity.
        this.data = new T[initialCapacity];
    }

    public T ElementAt(int index) {
        this.CheckIndex(index);
        return this.data[index];
    }

    public void SetValue(int index, T value) {
        this.CheckIndex(index);
        this.data[index] = value;
    }

    public void Append(T value) {
        // Check if we have to grow the array.
        this.GrowIfNecessary();

        // Append value.
        this.data[this.Length] = value;
        this.Length++;
    }

    public void InsertAt(int index, T value) {
        this.CheckIndex(index);

        // Check if we have to grow the array and do so if necessary.
        this.GrowIfNecessary();

        // Shift values to the right of the inserted position.
        for (int i = this.Length; i > index; i--) {
            this.data[i] = this.data[i - 1];
        }

        // Insert new value.
        this.data[index] = value;
        this.Length++;
    }

    public void RemoveFromEnd() {
        if (this.Length == 0) {
            throw new IndexOutOfRangeException("List is already empty.");
        }

        // Since values beyond `Length` are ignored, we don't actually
        // have to "remove" the value; we can simply decrease `Length`.
        this.Length--;
    }

    public void RemoveAt(int index) {
        this.CheckIndex(index);

        // Shift elements to the right of the index
        // by one position to the left.
        for (int i = index; i < this.Length - 1; i++) {
            this.data[i] = this.data[i + 1];
        }

        // Update length.
        this.Length--;
    }

    public bool Search(T value) {
        for (int i = 0; i < this.Length; i++) {
            if (this.data[i].Equals(value)) {
                return true;
            }
        }

        return false;
    }

    private void CheckIndex(int index) {
        if (index >= this.Length || index < 0) {
            throw new IndexOutOfRangeException("index");
        }
    }
    private void GrowIfNecessary() {
        // If the length of the dynamic array is the same as the
        // capacity of the internal array storage, we have to grow.
        if (this.Length == this.data.Length) {
            // Create a new, larger array to hold the data.
            T[] newData = new T[(int)(this.Length * GROWTH_FACTOR)];

            // Copy over data from old array.
            for (int i = 0; i < this.Length; i++) {
                newData[i] = this.data[i];
            }

            // Make the new array our internal data store.
            this.data = newData;
        }
    }
}
