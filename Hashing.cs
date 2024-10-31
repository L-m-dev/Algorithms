using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms {
    public class Node<T> {
        public String Key { get; set; }
        public T Value { get; set; }
        public Node(String key, T value) {
            this.Key = key;
            this.Value = value;
        }
    }
    public class HashStructure<T> {
        private Node<T> Node { get; set; }
        private LinkedList<Node<T>>[] Table { get; set; }
        private int _bucketQuantity;
        private int _occupiedBuckets;

        public HashStructure(int bucketQuantity = 1) {
            this._bucketQuantity = bucketQuantity;
            Table = new LinkedList<Node<T>>[_bucketQuantity];

        }

        public int HashFunction<T>(Node<T> node) {
            if (node.Key.Length < 1) {
                throw new ArgumentException(nameof(node), "Key needs minimum 1 length.");
            }
            int asciiValue = AsciiSumOfString(node.Key);
            int bucket = asciiValue % _bucketQuantity;

            return bucket;
        }

        /// <summary>
        /// Resizes array when hit maximum. Currently not used as buckets are pre-defined.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private void Resize(T[] array) {
            if (array == null) {
                throw new ArgumentNullException(nameof(array), "Array cannot be null");
            }

            GetLoadFactor();
            T[] newArray;

            if (array.Length == 0) {
                array = new T[1];
            }
            if (this._bucketQuantity == this._occupiedBuckets) {

                newArray = new T[array.Length * 2];
                for (int i = 0; i < array.Length; i++) {
                    newArray[i] = array[i];
                }
                array = newArray;
            }
        }

        private void GetLoadFactor() {
            if (this.Table == null) {
                throw new ArgumentNullException(nameof(Table), "Input cannot be null");
            }

            int _occupiedBuckets = 0;
            int totalCapacity = Table.Length;
            for (int i = 0; i < Table.Length; i++) {
                if (Table[i] is not null) {
                    _occupiedBuckets++;
                }
            }
        }

        public void Add(Node<T> node) {
            if (String.IsNullOrEmpty(node.Key) || node.Value == null) {
                throw new ArgumentException("Malformed Node");
            }

            int bucket = HashFunction(node);
            if (bucket < 0 || bucket == null) {
                throw new Exception("Invalid Bucket Address");
            }

            if (this.Table[bucket] == null) {
                LinkedList<Node<T>> linkedList = new LinkedList<Node<T>>();
                Table[bucket] = linkedList;
                linkedList.AddLast(node);
                _occupiedBuckets++;
                return;
            }
            else {
                var nodeExists = GetNodeIfExisting(node, Table[bucket]);

                if (nodeExists == null) {
                    Table[bucket].AddLast(node);
                }
                else {
                    if (!nodeExists.Value.Equals(node.Value)) {
                        nodeExists.Value = node.Value;
                    }
                }
                return;
            }
        }

        private int AsciiSumOfString(String text) {
            int sum = 0;
            foreach (char c in text) {
                sum += (int)c;
            }
            return Math.Min(int.MaxValue, sum);
        }

        private Node<T>? GetNodeIfExisting(Node<T> node, LinkedList<Node<T>> linkedList) {
            bool keyFound = false;
            foreach (var item in linkedList) {
                if (item.Key == node.Key) {
                    return item;
                }
            }
            return null;
        }
    }
}