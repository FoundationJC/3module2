using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3module2
{
    public class BinaryTree<T> : IEnumerable<T> where T : class
    {
        private BinaryTreeNode<T>? root; // ? — корінь може бути відсутнім (порожнє дерево)
        private IComparer<T> comparer; // readonly — задається лише в конструкторі

        // Конструктор приймає компаратор (щоб знати, як впорядковувати)
        public BinaryTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
            root = null;
        }

        // Додавання елемента
        public void Add(T item)
        {
            root = AddRecursive(root, item);
        }

        private BinaryTreeNode<T> AddRecursive(BinaryTreeNode<T>? node, T item)
        {
            if (node == null)
                return new BinaryTreeNode<T>(item);

            // Порівнюємо: якщо item < node.Data -> вліво, інакше вправо
            if (comparer.Compare(item, node.Data) < 0)
                node.Left = AddRecursive(node.Left, item);
            else
                node.Right = AddRecursive(node.Right, item);

            return node;
        }

        // Реалізація IEnumerable<T> для foreach (прямий порядок - preorder)
        public IEnumerator<T> GetEnumerator()
        {
            return PreOrder(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Прямий порядок (preorder): вузол -> ліво -> право
        private IEnumerable<T> PreOrder(BinaryTreeNode<T>? node)
        {
            if (node == null) yield break;

            yield return node.Data;                           // 1. Вузол
            foreach (var item in PreOrder(node.Left))         // 2. Ліве піддерево
                yield return item;
            foreach (var item in PreOrder(node.Right))        // 3. Праве піддерево
                yield return item;
        }
    }
}
