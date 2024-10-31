using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms {
    public class Node<T> {
        public string Key {   get; set; }
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

            UpdateLoadFactor(this.Table);

            Node<T> node = new(key, value);

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

        private int HashFunction(string key) {
            if (key.Length < 1) {
                throw new ArgumentException("Key should minimum 1 length.");
            }
            int asciiValue = AsciiSumOfString(key);
            int bucket = asciiValue % _bucketQuantity;
            return bucket;
        }

        private void UpdateLoadFactor(LinkedList<Node<T>>[] table) {

            if (this.Table == null) {
                throw new ArgumentNullException(nameof(Table), "Table cannot be null");
            }


            LinkedList<Node<T>>[] resizedTable;

            if (table.Length == 0) {
                resizedTable = new LinkedList<Node<T>>[1];
                this.Table = resizedTable;
                return;
            }


            int totalCapacity = Table.Length;
            _occupiedBuckets = 0;
            
            for (int i = 0; i < Table.Length; i++) {
                if (Table[i] is not null && Table[i].Count > 0) {
                    _occupiedBuckets++;
                }
            }

            int thresholdPercent = (_bucketQuantity * 75) / 100;

            if (this._occupiedBuckets > thresholdPercent) {
                //Increase table
                IncreaseTableSize(this.Table);              
            }
        }

        public void IncreaseTableSize(LinkedList<Node<T>>[] oldTable) {

            LinkedList<Node<T>>[] resizedTable = new LinkedList<Node<T>>[oldTable.Length * 2];
            this.Table = resizedTable;
            this._bucketQuantity = this.Table.Length - 1;

            for (int i = 0; i < oldTable.Length; i++) {
                if (oldTable[i] != null) {
                    foreach (var item in oldTable[i]) {
                        this.Add(item.Key, item.Value);
                    }
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