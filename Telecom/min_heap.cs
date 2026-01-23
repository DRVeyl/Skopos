using System;
using System.Collections.Generic;

namespace σκοπός {
  public class MinHeap<TKey, TValue> where TKey : IComparable<TKey> {
    private struct Entry {
      public TKey Key;
      public TValue Value;
    }

    private readonly List<Entry> heap = new List<Entry>();
    private readonly Dictionary<TValue, int> indices =
        new Dictionary<TValue, int>();

    public int Count => heap.Count;

    public void Clear() {
      heap.Clear();
      indices.Clear();
    }

    public void Insert(TKey key, TValue value) {
      int i = heap.Count;
      heap.Add(new Entry { Key = key, Value = value });
      indices[value] = i;
      HeapifyUp(i);
    }

    public void ExtractMin(out TKey key, out TValue value) {
      Entry min = heap[0];
      key = min.Key;
      value = min.Value;
      RemoveAt(0);
    }

    public void Remove(TValue value) {
      if (!indices.TryGetValue(value, out int i)) return;
      RemoveAt(i);
    }

    private void RemoveAt(int i) {
      int last = heap.Count - 1;

      if (i != last) {
        Swap(i, last);
      }

      indices.Remove(heap[last].Value);
      heap.RemoveAt(last);

      if (i < heap.Count) {
        // Only one direction is usually needed
        if (!HeapifyDown(i)) {
          HeapifyUp(i);
        }
      }
    }

    private void HeapifyUp(int i) {
      while (i > 0) {
        int p = (i - 1) >> 1;
        if (heap[p].Key.CompareTo(heap[i].Key) <= 0) break;
        Swap(i, p);
        i = p;
      }
    }

    // Returns true if moved
    private bool HeapifyDown(int i) {
      int count = heap.Count;
      bool moved = false;

      while (true) {
        int left = (i << 1) + 1;
        if (left >= count) break;

        int right = left + 1;
        int smallest = left;

        if (right < count &&
            heap[right].Key.CompareTo(heap[left].Key) < 0) {
          smallest = right;
        }

        if (heap[i].Key.CompareTo(heap[smallest].Key) <= 0) break;

        Swap(i, smallest);
        i = smallest;
        moved = true;
      }
      return moved;
    }

    private void Swap(int a, int b) {
      Entry tmp = heap[a];
      heap[a] = heap[b];
      heap[b] = tmp;

      indices[heap[a].Value] = a;
      indices[heap[b].Value] = b;
    }
  }
}
