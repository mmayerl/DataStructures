# Aufgabenblatt 1

## Übung 1: Ziffernsumme

```csharp
public static int SumOfDigits(int number) {
    if (number < 10) {
        return number;
    }

    return number % 10 + SumOfDigits(number / 10);
}
```

## Übung 2: Binary Search

```csharp
public static bool BinarySearch(int[] data, int searchValue) {
    return BinarySearch(data.AsSpan(), searchValue);
}

public static bool BinarySearch(Span<int> data, int searchValue) {
    // We are done, if the data is empty.
    if (data.Length == 0) {
        return false;
    }

    int centerIndex = data.Length / 2;

    // Check current center.
    if (data[centerIndex] == searchValue) {
        return true;
    }

    // If we have not found the value in the center, recurse left or right.
    if (searchValue < data[centerIndex]) {
        return BinarySearch(data[..centerIndex], searchValue);
    }
    else {
        return BinarySearch(data[(centerIndex + 1)..], searchValue);
    }
}
```

## Übung 3: Merge Sort

```csharp
public static List<double> MergeSort(List<double> list) {
    // Base case: We are done if the list has at most one element remaining.
    if (list.Count <= 1) {
        return list;
    }

    // Split list in half.
    int middleIndex = list.Count / 2;
    var leftList = list[..middleIndex];
    var rightList = list[middleIndex..];

    // Now, recursively merge sort both lists.
    leftList = MergeSort(leftList);
    rightList = MergeSort(rightList);

    // Finally, merge both lists.
    return Merge(leftList, rightList);
}

public static List<double> Merge(List<double> leftList, List<double> rightList) {
    var result = new List<double>(leftList.Count + rightList.Count);
    int i = 0;
    int j = 0;

    // Iterate in lockstep and get the smallest element from either side.
    while (i < leftList.Count && j < rightList.Count) {
        if (leftList[i] <= rightList[j]) {
            result.Add(leftList[i]);
            i++;
        }
        else {
            result.Add(rightList[j]);
            j++;
        }
    }

    // Add the remaining elements.
    while (i < leftList.Count) {
        result.Add(leftList[i]);
        i++;
    }

    while (j < rightList.Count) {
        result.Add(rightList[j]);
        j++;
    }

    // Done.
    return result;
}
```

## Übung 4: Quicksort

```csharp
static void QuickSort<T>(List<T> list) where T : IComparable<T> {
    QuickSort(list, 0, list.Count - 1);
}

static void QuickSort<T>(List<T> list, int begin, int end) where T : IComparable<T> {
    // Recursion base case: Stop if there are not at least two elements in the (sub)list.
    if (begin >= end) {
        return;
    }

    // Step 1: Select pivot.
    // For this simple implementation, we choose the first element of the (sub)list
    // as the pivot. More sophisticated selection methods exist.
    T pivot = list[begin];

    // Step 2: Rearrange elements around the pivot.
    int curLeft = begin + 1;
    int curRight = end;

    while (true) {
        // Scan from left until we find an item bigger than pivot.
        while (list[curLeft].CompareTo(pivot) <= 0 && curLeft < end) {
            curLeft++;
        }

        // Scan from right until we find an element smaller than the pivot.
        while (list[curRight].CompareTo(pivot) >= 0 && curRight > begin) {
            curRight--;
        }

        // Break if we crossed.
        if (curLeft >= curRight) {
            break;
        }

        // Swap if we did not cross.
        Swap(list, curLeft, curRight);
    }

    int pivotPosition = curRight;
    Swap(list, begin, pivotPosition);

    // Step 3: Recurse.
    QuickSort(list, begin, pivotPosition - 1);
    QuickSort(list, pivotPosition + 1, end);
}

static void Swap<T>(List<T> list, int idx1, int idx2) {
    T temp = list[idx1];
    list[idx1] = list[idx2];
    list[idx2] = temp;
}
```

## Übung 5: Komplexitätsbestimmung

* 5.1: `O(n)`. Dies ist eine rekursive lineare Suche, wir müssen also schlimmstenfalls die gesamte Liste von Werten durchgehen.
* 5.2: `O(n)`. Beide Inputs müssen die gleiche Länge (`n`) haben, und die einzige Schleife im Code macht `n` Durchläufe.
* 5.3: `O(n log n)`, da der aufgerufene Quicksort-Algorithmus diese Komplexität hat.
* 5.4: `O(n²)`. Beide Listen (`data` und `res` beinhalten `O(n)` Elemente), und wir haben verschachtelte Schleifen, welche über diese Listen iterieren.
* 5.5: `O(n)`. Die `Contains`- und `Add`-Methoden von einem `HashSet` sind `O(1)`. Diese Operationen werden von der Schleife `n` mal wiederholt.
* 5.6: `O(n log n)`. Wir rufen Quicksort auf, was `O(n log n)` ist. Die Schleife ist eine lineare Iteration durch `data`, also `O(n)`.

## Übung 6: Klammerung

Dieses Problem wird dadurch vereinfacht, dass wir nur runde Klammern behandeln müssen. Wir müssen also nicht überprüfen, ob _unterschiedliche_ Klammertypen korrekt gepaart sind.

Dadurch wird aus diesem Problem eine reine Zählaufgabe. Wir müssen nur sicherstellen, dass die Anzahl öffnender und schließender Klammern im String übereinstimmen. Dafür können wir eine simple Zählervariable verwenden. 

Wir iterieren den String Zeichen für Zeichen. Wenn wir einer öffnenden Klammer begegnen, erhöhen wir den Zähler um eins. Wenn wir einer schließenden Klammer begegnen, verringern wir den Zähler um eins.

Die Klammerung ist falsch, wenn
1. wir am Ende des Strings ankommen und der Zähler ungleich null ist, oder
2. der Zähler während der Iteration negativ wird.

Code:

```csharp
public static bool CheckParens(string text) {
    int parensCounter = 0;

    foreach (char c in text) {
        if (c == '(') {
            parensCounter++;
        }
        else if (c == ')') {
            parensCounter--;
        }

        // First possible violation: Parens count becomes negative.
        // This happens when the text closes a parenthesis that was
        // never opened.
        if (parensCounter < 0) {
            return false;
        }
    }

    // Second possible violation: The parens count is not zero at the end.
    // This happens if the text contains at least one opening parenthesis
    // that is never closed.
    if (parensCounter != 0) {
        return false;
    }

    // Otherwise, the parentheses are correct.
    return true;
}
```

Die Laufzeitkomplexität dieser Lösung ist `O(n)`, was die bestmögliche Komplexität für dieses Problem ist.
