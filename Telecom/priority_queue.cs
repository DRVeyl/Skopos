using System;
using System.Collections.Generic;

namespace σκοπός {
  public class PriorityQueue<TElement, TPriority>
      where TPriority : IComparable<TPriority> {
    private struct Entry {
      public TElement Element;
      public TPriority Priority;
    }

    private readonly List<Entry> _heap;
    private readonly Dictionary<TElement, int> _indices;

    public PriorityQueue() {
      _heap = new List<Entry>();
      _indices = new Dictionary<TElement, int>();
    }

    public int Count {
      get { return _heap.Count; }
    }

    public void Clear() {
      _heap.Clear();
      _indices.Clear();
    }

    /* ---------------- Public API ---------------- */

    public void Enqueue(TElement element, TPriority priority) {
      if (_indices.ContainsKey(element))
        throw new ArgumentException("Element already exists in the queue.");

      int index = _heap.Count;
      _heap.Add(new Entry { Element = element, Priority = priority });
      _indices[element] = index;
      HeapifyUp(index);
    }

    public TElement Dequeue() {
      if (_heap.Count == 0)
        throw new InvalidOperationException("Queue is empty.");

      Entry min = _heap[0];
      RemoveAt(0);
      return min.Element;
    }

    public bool TryDequeue(out TElement element, out TPriority priority) {
      if (_heap.Count == 0) {
        element = default(TElement);
        priority = default(TPriority);
        return false;
      }

      Entry min = _heap[0];
      element = min.Element;
      priority = min.Priority;
      RemoveAt(0);
      return true;
    }

    public TElement Peek() {
      if (_heap.Count == 0)
        throw new InvalidOperationException("Queue is empty.");

      return _heap[0].Element;
    }

    public bool TryPeek(out TElement element, out TPriority priority) {
      if (_heap.Count == 0) {
        element = default(TElement);
        priority = default(TPriority);
        return false;
      }

      Entry min = _heap[0];
      element = min.Element;
      priority = min.Priority;
      return true;
    }

    /// <summary>
    /// Decreases the priority of an existing element.
    /// Returns false if the element does not exist or the priority is not lower.
    /// </summary>
    public bool DecreasePriority(TElement element, TPriority newPriority) {
      int index;
      if (!_indices.TryGetValue(element, out index))
        return false;

      if (_heap[index].Priority.CompareTo(newPriority) <= 0)
        return false;

      _heap[index] = new Entry {
        Element = element,
        Priority = newPriority
      };

      HeapifyUp(index);
      return true;
    }

    public bool Remove(TElement element) {
      int index;
      if (!_indices.TryGetValue(element, out index))
        return false;

      RemoveAt(index);
      return true;
    }

    /* ---------------- Heap internals ---------------- */

    private void RemoveAt(int index) {
      int last = _heap.Count - 1;

      if (index != last)
        Swap(index, last);

      _indices.Remove(_heap[last].Element);
      _heap.RemoveAt(last);

      if (index < _heap.Count) {
        if (!HeapifyDown(index))
          HeapifyUp(index);
      }
    }

    private void HeapifyUp(int index) {
      while (index > 0) {
        int parent = (index - 1) >> 1;

        if (_heap[parent].Priority.CompareTo(_heap[index].Priority) <= 0)
          break;

        Swap(index, parent);
        index = parent;
      }
    }

    // Returns true if moved
    private bool HeapifyDown(int index) {
      int count = _heap.Count;
      bool moved = false;

      while (true) {
        int left = (index << 1) + 1;
        if (left >= count)
          break;

        int right = left + 1;
        int smallest = left;

        if (right < count &&
            _heap[right].Priority.CompareTo(_heap[left].Priority) < 0) {
          smallest = right;
        }

        if (_heap[index].Priority.CompareTo(_heap[smallest].Priority) <= 0)
          break;

        Swap(index, smallest);
        index = smallest;
        moved = true;
      }

      return moved;
    }

    private void Swap(int a, int b) {
      Entry tmp = _heap[a];
      _heap[a] = _heap[b];
      _heap[b] = tmp;

      _indices[_heap[a].Element] = a;
      _indices[_heap[b].Element] = b;
    }
  }
}
