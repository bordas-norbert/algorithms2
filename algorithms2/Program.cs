using System.Collections;
using System.Drawing;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace algorithms2
{
    /*public class Tree
    {
        public Node root;
        private int count = 0;
        public class Node
        {
            public Node leftChild, rightChild;
            public int value;
            public Node(int value)
            {
                this.value = value;
            }
            public override string ToString()
            {
                return "Node= " + value;
            }
        }
        public int size() { return count; }
        public void insert(int value)
        {
            var node = new Node(value);
            count++;
            if (root == null)
            {
                root = node;
                return;
            }

            var current = root;
            while (true)
            {
                if (current.value > value)
                {
                    if (current.leftChild == null)
                    {
                        current.leftChild = node;
                        break;
                    }
                    current = current.leftChild;
                }
                else
                {
                    if (current.rightChild == null)
                    {
                        current.rightChild = node;
                        break;
                    }
                    current = current.rightChild;
                }
            }
        }
        public bool find(int value)
        {
            var current = root;
            while(current != null)
            {
                if (current.value == value)
                    return true;
                else if(value < current.value)
                {
                    current = current.leftChild;
                }
                else
                {
                    current= current.rightChild;
                }
            }
            return false;
        }
        public void traversePreorder()
        {
            traversePreorder(root);
        }
        public void traverseInorder()
        {
            traverseInorder(root);
        }
        private void traverseInorder(Node root)
        {
            if(root == null) return;

            traverseInorder(root.leftChild);
            Console.WriteLine(root.value + " ");
            traverseInorder(root.rightChild);
        }
        public void traversePostorder()
        {
            traversePostorder(root);
        }
        private void traversePostorder(Node root)
        {
            if (root == null) return;

            traversePostorder(root.leftChild);
            traversePostorder(root.rightChild);
            Console.WriteLine(root.value + " ");
        }
        private void traversePreorder(Node root)
        {
            if(root == null)
                return;

            Console.Write(root.value + " ");
            traversePreorder(root.leftChild);
            traversePreorder(root.rightChild);
        }
        public int height()
        {
            return height(root);
        }
        private int height(Node root)
        {
            if (root == null) return -1;
            if(root.leftChild == null && root.rightChild == null)
            {
                return 0;
            }
            return 1 + (Math.Max(height(root.leftChild), height(root.rightChild)));
        }
        private bool isLeaf(Node node)
        {
            return (node.leftChild == null) && (node.rightChild == null);
        }
        public int min()
        {
            if (root == null)
                throw new Exception("illegal");
            //only goes to the left
            //thus way it finds in O(log n) the least 
            var current = root;
            var last = current;
            while (current != null) 
            {
                last = current;
                current = current.leftChild;
            }
            return last.value;
        }
        //o(n)
        private int min(Node root)
        {
            if (isLeaf(root))
                return root.value;

            var left = min(root.leftChild);
            var right = min(root.rightChild);

            return 1 + Math.Min(Math.Min(left, right), root.value);
        }
        public Array getKthNodes(int k)
        {
            ArrayList l = new();
            printNodes(root, k,ref l);
            return l.ToArray();

        }
        private void printNodes(Node root, int k, ref ArrayList list)
        {
            if (root != null)
            {
                if (k == 0)
                { list.Add(root.value); return; }
                else
                {
                    printNodes(root.leftChild, k - 1,ref list);
                    printNodes(root.rightChild, k - 1,ref list);
                }
            }
 
        }
        public void levelOrderTraversal()
        {
            for( int i = 0; i <= height(); i++)
            {
                foreach(var item in getKthNodes(i))
                    Console.Write(item + " ");
                Console.WriteLine();
            }
        }
        private bool equals(Node first, Node second)
        {
            if (first == null && second == null) return true;

            if(first != null && second != null)
            {
                return first.value == second.value && equals(first.leftChild, second.leftChild) && equals(first.rightChild, second.rightChild);
            }
            return false;
        }
        public bool equals(Tree secondTree)
        {
            if( secondTree == null ) return false;

            return equals(root, secondTree.root);
        }
        public bool isValidSearchTree()
        {
            return validation(root, int.MinValue, int.MaxValue);
        }
        public void addRoots()
        {
            int aux = root.value;
            root.rightChild.value = root.value + 120;
        }
        private bool validation(Node root, int minValue, int maxValue)
        {
           if(root == null)
            return true;

            if (root.value > maxValue || root.value < minValue)
                return false;

            return validation(root.leftChild, minValue, root.value - 1) &&
            validation(root.rightChild, root.value + 1, maxValue);
           
        }
        public int countLeaves()
        {
            int counter = 0;
            countLeaves(root,ref counter);
            return counter;
        }
        private void countLeaves(Node root,ref int counter)
        {
            if (root == null) return;

            if (root.leftChild == null && root.rightChild == null)
            {
                counter++;
                return;
            }
            countLeaves(root.leftChild,ref counter);
            countLeaves(root.rightChild,ref counter);
        }
        public int max()
        {
            if (root == null)
                throw new Exception("Empty stack.");
            int maxValue = root.value;
            max(ref maxValue,root);
            return maxValue;
        }
        private void max(ref int maxValue, Node root)
        {
            if (root == null)
                return;
            if (root.value > maxValue)
                maxValue = root.value;
            max(ref maxValue,root.leftChild);
            max(ref maxValue,root.rightChild);
        }

        public bool contains(int value)
        {
            bool result = false;
            containsNr(root, value, ref result);
            return result;
        }

        private void containsNr(Node root, int value, ref bool containsValue)
        {
            if(root == null)
                return;

            if (root.value == value)
            {   
                containsValue = true;
                return; 
            }
            
            containsNr(root.leftChild, value, ref containsValue);
            containsNr(root.rightChild, value, ref containsValue);
        }
        public bool areSiblings(int value1, int value2)
        {
            bool areSibs = false;
            areSiblings(root, new Node(value1), new Node(value2), ref areSibs);
            return areSibs;
        }
        private void areSiblings(Node root, Node node1, Node node2, ref bool areSib)
        {
            if (root == null || root.leftChild == null || root.rightChild == null)
                return;
            
            if ((root.rightChild.value == node1.value && root.leftChild.value == node2.value) ||
               (root.rightChild.value == node2.value && root.leftChild.value == node1.value))
            {
                areSib = true;
                return;
            }

            areSiblings(root.leftChild, node1, node2, ref areSib);
            areSiblings(root.rightChild, node1, node2, ref areSib);
            
        }
        public void ancestors(int value)
        {
            LinkedList<Node> list = new LinkedList<Node>();
            LinkedList<Node> rezultat = new LinkedList<Node>();
            getAncestors(root, new Node(value), ref list, ref rezultat);  

            foreach(var item in rezultat)
                Console.Write(item.value + " ");
        }

        private void getAncestors(Node currentNode, Node node, 
        ref LinkedList<Node> list, ref LinkedList<Node> rez)
        {
            if( currentNode == null ) return;

            if( currentNode.value == node.value )
            {
                foreach(var item in list)
                    rez.AddLast(item);
                return;
            }
            else
            {
                list.AddLast(currentNode);
                getAncestors(currentNode.leftChild, node, ref list, ref rez);
                getAncestors(currentNode.rightChild, node, ref list, ref rez);
                list.RemoveLast();

            }
        }
    }   */

    /*public class AVLTree
    {
        AVLNode root;
        public class AVLNode
        {
            public int value, height = 0;
            public AVLNode leftChild, rightChild;
            public AVLNode(int value) 
            {
                this.value = value;
            }
            public override string ToString()
            {
                return "Node = " + (this.value).ToString();
            }
        }
        public void insert(int value)
        {
            root = insert(root, value);
        }
        private AVLNode insert(AVLNode root, int value)
        {
            if (root == null)
            {
                root = new AVLNode(value);
                return root;
            }
            if (root.value < value)
                root.leftChild = insert(root.leftChild, value);
            else
                root.rightChild = insert(root.rightChild, value);

            setHeight(root);

            return balance(root);

        }

        private AVLNode balance(AVLNode root)
        {
            if (isLeftHeavy(root))
            {
                if (balanceFactor(root.leftChild) < 0) // left rotate leftchild then right rotate root
                    root.leftChild = rotateLeft(root.leftChild);
                return rotateRight(root.rightChild);
            }

            else if (isRightHeavy(root))
                {
                if (balanceFactor(root.rightChild) > 0) // right rightchild then left rotate root
                    root.rightChild = rotateRight(root.rightChild);

                return rotateLeft(root.leftChild);
                }
            return root;

        }
        private AVLNode rotateLeft(AVLNode root)
        {
            var newRoot = root.rightChild;

            root.rightChild = newRoot.leftChild;
            newRoot.leftChild = root;

            setHeight(root);
            setHeight(newRoot);

            return newRoot;
        }

        private AVLNode rotateRight(AVLNode root)
        {
            var newRoot = root.leftChild;

            root.leftChild = newRoot.rightChild;
            newRoot.rightChild = root;

            setHeight(root);
            setHeight(newRoot);

            return newRoot;
        }

        private void setHeight(AVLNode root)
        {
            root.height = (Math.Max(height(root.rightChild),
                                    height(root.leftChild)));
        }
        private bool isLeftHeavy(AVLNode node)
        {
            return balanceFactor(node) > 1;
        }

        private int balanceFactor(AVLNode node)
        {
            if (node == null)
                return 0;
            return height(node.leftChild) - height(node.rightChild);
        }

        private bool isRightHeavy(AVLNode node)
        {
            return balanceFactor(node) < -1;
        }
        
        private int height(AVLNode node1)
        {
            if (node1 == null)
                return -1;
            return node1.height;
        }

        public bool isBalanced()
        {
            return isBalanced(root);
        }

        private bool isBalanced(AVLNode root)
        {
            if (root == null)
                return true;

            var balanceFactor = height(root.leftChild) - height(root.rightChild);

            return Math.Abs(balanceFactor) <= 1 &&
                    isBalanced(root.leftChild) &&
                    isBalanced(root.rightChild);
        }
        public bool isPerfect()
        {
            return isPerfect(root);
        }

        private bool isPerfect(AVLNode root)
        {
            return isBalanced(root) && isFull(root);
        }

        public bool isFull(AVLNode node)
        {
            if (node == null)
                return true;

            // Check if the node has either no children or both children
            if (node.leftChild == null && node.rightChild == null)
                return true;
            if (node.leftChild != null && node.rightChild != null)
                return isFull(node.leftChild) && isFull(node.rightChild);

            return false;
        }

    }*/
    public class Heap
    {
        private int[] v = new int[10];
        private int size = 0;
        public bool isFull()
        {
            return size == v.Length;
        }
        public int max()
        {
            if (isEmpty())
                throw new Exception("empty");

            return v[0];
        }
        public void insert(int value)
        {
            if (isFull())
                throw new Exception("heap is full");
            v[size++] = value;

            bubbleUp();
        }
        private void bubbleUp()
        {
            int index = size - 1;
            
            while (index > 0 && v[index] > v[parent(index)])
            {
                swap(index, parent(index));
                index = parent(index);
            }

        }
        private int parent(int index)
        {
            return (index - 1) / 2;
        }
        private void swap(int first, int second)
        {
            int temp = v[first];
            v[first] = v[second];
            v[second] = temp;
        }
        public bool isEmpty() { return size == 0; }
        public int remove()
        {
            if (isEmpty())
                throw new Exception("empty");
            var root = v[0];
            v[0] = v[--size];
            
            bubbleDown();
            return root;
        }
        private void bubbleDown()
        {
            var index = 0;
            while (index <= size && !isValidParent(index))
            {
                var largerChildIdx = largerChildIndex(index);
                swap(index, largerChildIdx);
                index = largerChildIdx;
            }
        }
        private bool hasLeftChild(int index)
        {
            return (leftChildIndex(index) <= size);
        }
        private bool hasRightChild(int index)
        {
            return (rightChildIndex(index) <= size);
        }
        private int largerChildIndex(int index)
        {
            if (!hasLeftChild(index))
                return index;
            if(!hasRightChild(index))
                return leftChildIndex(index);

            return (leftChild(index) > rightChild(index))
                    ? leftChildIndex(index)
                    : rightChildIndex(index);
        }
        private bool isValidParent(int index)
        {
            if (!hasLeftChild(index))
                return true;

            var isValid = v[index] >= leftChild(index);
            if (hasRightChild(index))
                isValid = isValid & v[index] >= rightChild(index);
            
            return isValid;
                
        }
        private int rightChild(int index)
        {
            return v[rightChildIndex(index)];
        }
        private int leftChild(int index)
        {
            return v[leftChildIndex(index)];
        }
        private int leftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        private int rightChildIndex(int index)
        {
            return index * 2 + 2;
        }
    }
    public class MaxHeap
    {
        public static void heapify(int[] array)
        {
            int lastParentIndex = array.Length / 2 - 1;
            for ( int i = lastParentIndex; i>=0; i--)
            {
                heapify(array, i);
            }
            
        }
        private static void heapify(int[] array, int index)
        {
            var largerIndex = index;

            var leftIndex = index * 2 + 1;
            if (leftIndex < array.Length && array[leftIndex] > array[largerIndex])
            {
                largerIndex = leftIndex;
            }
            var rightIndex = index * 2 + 2;
            if (rightIndex < array.Length && array[rightIndex] > array[largerIndex])
                largerIndex = rightIndex;

            if (index == largerIndex)
                return;

            swap(array, index, largerIndex);
            heapify(array, largerIndex);

            
        }

        private static void swap(int[] array, int first, int second)
        {
            var temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
        public static int getkthLargest(int[] array, int k)
        {
            var heap = new Heap();
            foreach(var number in array)
            {
                heap.insert(number);
            }
            for (int i = 0; i < k - 1; i++)
                heap.remove();

            return heap.max();
        }

        public static bool isMaxHeap(int[] array)
        {
            int index = 0;

            while(index < array.Length)
            {
                if (!isValidParent(array, index))
                {
                    return false;
                }
                index++;
            }
            return true;
        }

        private static bool isValidParent(int[] arr, int idx)
        {
            return arr[idx] >= leftChildOf(arr, idx) && arr[idx] >= rightChildOf(arr, idx);
        }

        private static int rightChildOf(int[] arr, int idx)
        {
            int rightChildIndex = idx * 2 + 2;
            if (rightChildIndex >= arr.Length)
                return int.MinValue;
            return arr[rightChildIndex];
        }

        private static int leftChildOf(int[] arr, int idx)
        {
            int leftChildIndex = idx * 2 + 1;
            if (leftChildIndex >= arr.Length)
                return int.MinValue;
            return arr[leftChildIndex];
        }
    }
    public class Node
    {
        public int key;
        public string value;

        public Node(int key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return this.key + " " + this.value;
        }

    }
    public class MinHeap
    {
        private Node[] items = new Node[10];
        private int size = 0;
        public bool isempty = false;
        public void insert(Node value)
        {
            if (isFull())
                throw new Exception("heap is full");
            items[size++] = value;

            bubbleUp();
        }

        private bool isFull()
        {
            return size == items.Length;
        }

        private void bubbleUp()
        {
            int index = size - 1;

            while (index > 0 && items[index].key < items[parent(index)].key)
            {
                swap(index, parent(index));
                index = parent(index);
            }

        }
        private int parent(int index)
        {
            return (index - 1) / 2;
        }
        private void swap(int first, int second)
        {
            var temp = items[first];
            items[first] = items[second];
            items[second] = temp;
        }
        public Node remove()
        {
            if (isEmpty())
                throw new Exception("empty");
            var root = items[0];
            items[0] = items[--size];

            bubbleDown();
            return root;
        }

        private bool isEmpty()
        {
            isempty = size == 0;
            return isempty;
        }

        private void bubbleDown()
        {
            var index = 0;
            while (index <= size && !isValidParent(index))
            {
                var largerChildIdx = largerChildIndex(index);
                swap(index, largerChildIdx);
                index = largerChildIdx;
            }
        }
        private bool hasLeftChild(int index)
        {
            return (leftChildIndex(index) <= size);
        }
        private bool hasRightChild(int index)
        {
            return (rightChildIndex(index) <= size);
        }
        private int largerChildIndex(int index)
        {
            if (!hasLeftChild(index))
                return index;
            if (!hasRightChild(index))
                return leftChildIndex(index);

            return (leftChild(index).key > rightChild(index).key)
                    ? leftChildIndex(index)
                    : rightChildIndex(index);
        }
        private bool isValidParent(int index)
        {
            if (!hasLeftChild(index))
                return true;

            var isValid = items[index].key <= leftChild(index).key;
            if (hasRightChild(index))
                isValid = isValid & items[index].key <= rightChild(index).key;

            return isValid;

        }
        private Node rightChild(int index)
        {
            return items[rightChildIndex(index)];
        }
        private Node leftChild(int index)
        {
            return items[leftChildIndex(index)];
        }
        private int leftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        private int rightChildIndex(int index)
        {
            return index * 2 + 2;
        }
    }

    public class MinPriorityQueue
    {
        private MinHeap minheap = new();

        public void add(string value, int priority)
        {
            minheap.insert(new Node(priority, value));
        }

        public void remove()
        {
            try
            {
                minheap.remove();
            }

            catch(Exception e)
            {
                Console.WriteLine("queue is empty");
            }
        }
        public bool isEmpty()
        {
            return minheap.isempty;
        }
    }
    public class Trie
    {
        public static int ALPH_SIZE = 26;
        public class Node
        {
            public char value;
            public Dictionary<char, Node> children = new Dictionary<char, Node>();
            public bool isEndOfWord;

            public Node(char value)
            {
                this.value = value;
            }

            public override string ToString()
            {
                return "value= " + value;
            }

            public bool hasChild(char ch)
            {
                return children.ContainsKey(ch);
            }

            public void addChild(char ch)
            {
                children.Add(ch, new Node(ch));
            }
            public void removeChild(char ch)
            {
                children.Remove(ch);
            }
            public Node getChild(char ch)
            {
                return children[ch];
            }

            public Node[] getChildren()
            {
                return children.Values.ToArray();
            }

            public bool hasChildren()
            {
                return !(children.Count == 0);
            }
        }

        private Node root = new Node(' ');

        public void insert(string word)
        {
            var current = root;
            foreach(var ch in word.ToCharArray())
            {
                if (!current.hasChild(ch))
                    current.addChild(ch);
                current = current.getChild(ch);
            }
            current.isEndOfWord = true;
        }

        public bool contains(string word)
        {
            if(word == null) return false;
            var current = root;
            foreach(var ch in word)
            {
                if (!current.hasChild(ch))
                    return false;

                current = current.getChild(ch);
            }
            return true;
        }
        public void traverse()
        {
            traverse(root);
        }
        private void traverse(Node root)
        {
            Console.WriteLine(root.value);

            foreach(var child in root.getChildren())
            { traverse(child); }
        }

        public void remove(string word)
        {
            if (word == null) return;
            remove(root, word, 0);
        }
        private void remove(Node root, string word, int index)
        {
            if(index == word.Length)
            {
                root.isEndOfWord = false;
                return;
            }

            var ch = word[index];
            var child = root.getChild(ch);
            if (child == null)
                return;


            remove(child, word, index + 1);

            if(!child.hasChildren() && !child.isEndOfWord)
            {
                root.removeChild(ch);
            }
        }
        
        public List<string> findWords(string prefix)
        {
            List<string> words = new List<string>();
            var lastNode = findLastNodeOf(prefix);
            findWords(lastNode, prefix, words);
            return words;
        }

        private void findWords(Node root, string prefix, List<string> words)
        {
            if (root == null)
                return;

            if(root.isEndOfWord)
                words.Add(prefix);

            foreach (var child in root.getChildren())
                findWords(child, prefix + child.value, words);
        }
        private Node findLastNodeOf(string prefix)
        {
            if (prefix == null)
                return null;
            var current = root;
            foreach(var ch in prefix)
            {
                var child = current.getChild(ch);
                if (child == null)
                    return null;
                current = child;
            }
            return current;
        }
        public bool containsRecursive(string word)
        {
            if (word == null)
                return false;
            bool found = false;
            containsRecursive(root,word, word, "",ref found);
            return found;
        }

        private void containsRecursive(Node current,string copy, string word, string prefix, ref bool found)
        {
            if (prefix == copy && current.isEndOfWord)
            {
                found = true;
                return;
            }
            foreach(char ch in word)
            {
                if(current.hasChild(ch))
                containsRecursive(current.getChild(ch),copy, word.Substring(1), prefix + ch,ref found);
            }
        }
        public int countWords()
        {
            var current = root;
            int count = 0;
            countWords(root,ref count);
            return count;
        }

        private void countWords(Node root, ref int count)
        {
            foreach (var child in root.getChildren())
            {
                if (child.isEndOfWord)
                    count++;
                countWords(child, ref count);
            }
        }

        public string longestCommonPrefix()
        {
            string prefix = "", result = "";
            longestCommonPrefix(root, prefix, ref result, 0);
            
            return result;
        }

        private void longestCommonPrefix(Node root, string prefix, ref string result, int max)
        {
            foreach (var child in root.getChildren())
            {
                if (root.getChildren().Length >= max)
                {
                    max = root.getChildren().Length;
                    result = prefix;
                    if (child.getChildren().Length >= max)
                        prefix = prefix + child.value;
                    longestCommonPrefix(child, prefix, ref result, max);
                }
            }
        }
    }
    internal class Program
        {
        static void Main(string[] args)
        {
            Trie trie = new();
            trie.insert("car");
            //trie.insert("dog");

            Console.WriteLine(trie.longestCommonPrefix());

           
        }
    }
    
}