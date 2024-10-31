using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms {
    public class Node<T> {
        public string Key { get; set; }
        public T Value { get; set; }
        public Node(string key, T value) {
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

       


        public T Get(string key) {
            int bucket = HashFunction(key);
            if (Table[bucket] == null) {
                throw new KeyNotFoundException($"Key {key} wasn't found in {nameof(Table)}");
            }
            foreach (var item in Table[bucket]) {
                if (string.Equals(item.Key, key, StringComparison.Ordinal)) {
                    return item.Value;
                }
            }
            throw new KeyNotFoundException($"Key {key} wasn't found in {nameof(Table)}");
        }
        
        public T Remove(string key) {
            int bucket = HashFunction(key);
            if (Table[bucket] == null) {
                throw new KeyNotFoundException($"Key {key} wasn't found in {nameof(Table)}");
            }
            //Think about resizing logic when removing.
            foreach (var item in Table[bucket]) {
                if (string.Equals(item.Key, key, StringComparison.Ordinal)) {
                    Table[bucket].Remove(item);
                    return item.Value;
                }
            }
            throw new KeyNotFoundException($"Key {key} wasn't found in {nameof(Table)}");
        }

        public void Add(string key, T value) {

            if (string.IsNullOrEmpty(key) || value == null) {
                throw new ArgumentNullException();
            }

            Node<T> node = new(key, value);

            GetLoadFactor();

            int thresholdPercent = (_bucketQuantity * 75) / 100;
            if (this._occupiedBuckets > thresholdPercent) {
                this.Resize(this.Table);
            }

            int bucket = HashFunction(node.Key);
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

        private void Resize(LinkedList<Node<T>>[] table) {
            if (table == null) {
                throw new ArgumentNullException(nameof(table), "Array cannot be null");
            }

            GetLoadFactor();

            LinkedList<Node<T>>[] newArray;

            if (table.Length == 0) {
                newArray = new LinkedList<Node<T>>[1];
                this.Table = newArray;
                return;
            }

            int thresholdPercent = (_bucketQuantity * 75) / 100;

            if (this._occupiedBuckets > thresholdPercent) {

                newArray = new LinkedList<Node<T>>[table.Length * 2];
                this.Table = newArray;
                _bucketQuantity = this.Table.Length - 1;

                for (int i = 0; i < table.Length; i++) {
                    if (table[i] != null) {
                        foreach (var item in table[i]) {
                            this.Add(item.Key, item.Value);
                        }
                    }
                }
            }

        }

        private int HashFunction(string key) {
            if (key.Length < 1) {
                throw new ArgumentException("Key should minimum 1 length.");
            }
            int asciiValue = AsciiSumOfString(key);
            int bucket = asciiValue % _bucketQuantity;
            return bucket;
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
        private int AsciiSumOfString(string text) {
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