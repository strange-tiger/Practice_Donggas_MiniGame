using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace MyHeap
{
    public class MapNode
    {
        public (int x, int z) Position { get; private set; }
        public int Cost { get; private set; }

        public int Evaluate(int count, int heuristic)
        {
            Cost = count + heuristic;

            return Cost;
        }

        public int Evaluate(int count, UnityEngine.Vector3 destination)
        {
            int heuristic = Mathf.Abs((int)destination.x - Position.x) + Mathf.Abs((int)destination.z - Position.z);
            
            Cost = Evaluate(count, heuristic);

            return Cost;
        }
    }

    // �ּ���
    public class MapHeap
    {
        protected List<MapNode> container = new List<MapNode>();

        public virtual int Top()
        {
            return container[0].Cost;
        }

        public virtual bool Empty()
        {
            return container.Count == 0;
        }

        public virtual int Size()
        {
            return container.Count;
        }

        public virtual void Push(MapNode value)
        {
            // 1. �� ���� �����͸� ����
            container.Add(value);

            // 2. ���� �Һ����� ������ ������ ������ ��ȯ
            // ù ��° ����� �ε����� 1�� �����Ѵ�.
            int currentIndex = Size();

            while (currentIndex > 1)
            {
                // 2-1. �θ� ��� �ε����� ���Ѵ�.
                int parentIndex = currentIndex / 2;

                // 2-2. �θ� ���� ��
                if (container[parentIndex - 1].Cost < container[currentIndex - 1].Cost)
                {
                    // 2-2-1. �θ� ������ �� �۴ٸ� ���� �Һ����� ����, ����
                    break;
                }

                // 2-3. �θ� ���� �ٲٰ� ���� ��ġ�� ����
                Swap(parentIndex - 1, currentIndex - 1);
                currentIndex = parentIndex;
            }
        }

        public virtual int Pop()
        {
            // 1. ��ȯ�� �� ����
            int top = Top();

            // 2. ������ ��带 ��Ʈ ���� ����
            container[0] = container[Size() - 1];

            // 3. ������ ��带 ����
            container.RemoveAt(Size() - 1);

            // 4. ���� �Һ����� ������ ������ ������ ��ȯ
            // ù ��° ����� �ε����� 1�� �����Ѵ�.
            int currentSize = Size();
            int currentIndex = 1;

            while (currentIndex < currentSize)
            {
                // 4-1. �ڽ� ����� �ε����� ���Ѵ�.
                int left = currentIndex * 2;
                int right = left + 1;

                // 4-1-1. �ڽ��� �����ؾ� �Ѵ�.
                if (left > currentSize)
                {
                    break;
                }

                // 4-2. �ڽ� ��� �� �� ���� ��带 ã�´�. 
                int child = left;
                if (right <= currentSize && container[left - 1].Cost > container[right - 1].Cost)
                {
                    child = right;
                }

                // 4-3. �ڽ� ���� ��
                if (container[currentIndex - 1].Cost >= container[child - 1].Cost)
                {
                    // 4-3-1. �ڽ��� ������ �۴ٸ� ���� �Һ����� ����, ����
                    break;
                }

                // 4-4. �ڽ� ���� �ٲٰ� ���� ��ġ�� ����
                Swap(currentIndex - 1, child - 1);
                currentIndex = child;
            }

            // 5. ������ �� ��ȯ
            return top;
        }

        // �ε����� �޾� container �� ��ȯ
        protected virtual void Swap(int parIndex, int curIndex)
        {
            MapNode temp = container[parIndex];
            container[parIndex] = container[curIndex];
            container[curIndex] = temp;
        }

        // ���� ����.
        public virtual void Clear()
        {
            container.Clear();
        }
    }

    // �ּ���
    public class Heap
    {
        protected List<int> container = new List<int>();

        public virtual int Top()
        {
            return container[0];
        }

        public virtual bool Empty()
        {
            return container.Count == 0;
        }

        public virtual int Size()
        {
            return container.Count;
        }

        public virtual void Push(int value)
        {
            // 1. �� ���� �����͸� ����
            container.Add(value);

            // 2. ���� �Һ����� ������ ������ ������ ��ȯ
            // ù ��° ����� �ε����� 1�� �����Ѵ�.
            int currentIndex = Size();

            while (currentIndex > 1)
            {
                // 2-1. �θ� ��� �ε����� ���Ѵ�.
                int parentIndex = currentIndex / 2;

                // 2-2. �θ� ���� ��
                if (container[parentIndex - 1] < container[currentIndex - 1])
                {
                    // 2-2-1. �θ� ������ �� �۴ٸ� ���� �Һ����� ����, ����
                    break;
                }

                // 2-3. �θ� ���� �ٲٰ� ���� ��ġ�� ����
                Swap(parentIndex - 1, currentIndex - 1);
                currentIndex = parentIndex;
            }
        }

        public virtual int Pop()
        {
            // 1. ��ȯ�� �� ����
            int top = Top();

            // 2. ������ ��带 ��Ʈ ���� ����
            container[0] = container[Size() - 1];

            // 3. ������ ��带 ����
            container.RemoveAt(Size() - 1);

            // 4. ���� �Һ����� ������ ������ ������ ��ȯ
            // ù ��° ����� �ε����� 1�� �����Ѵ�.
            int currentSize = Size();
            int currentIndex = 1;

            while (currentIndex < currentSize)
            {
                // 4-1. �ڽ� ����� �ε����� ���Ѵ�.
                int left = currentIndex * 2;
                int right = left + 1;

                // 4-1-1. �ڽ��� �����ؾ� �Ѵ�.
                if (left > currentSize)
                {
                    break;
                }

                // 4-2. �ڽ� ��� �� �� ���� ��带 ã�´�. 
                int child = left;
                if (right <= currentSize && container[left - 1] > container[right - 1])
                {
                    child = right;
                }

                // 4-3. �ڽ� ���� ��
                if (container[currentIndex - 1] >= container[child - 1])
                {
                    // 4-3-1. �ڽ��� ������ �۴ٸ� ���� �Һ����� ����, ����
                    break;
                }

                // 4-4. �ڽ� ���� �ٲٰ� ���� ��ġ�� ����
                Swap(currentIndex - 1, child - 1);
                currentIndex = child;
            }

            // 5. ������ �� ��ȯ
            return top;
        }

        // �ε����� �޾� container �� ��ȯ
        protected virtual void Swap(int parIndex, int curIndex)
        {
            int temp = container[parIndex];
            container[parIndex] = container[curIndex];
            container[curIndex] = temp;
        }

        // ���� ����.
        public virtual void Clear()
        {
            container.Clear();
        }
    }

    #region �Ϲ�ȭ�õ�
    /*
        public class Heap<T> where T : IComparable
        {
            protected List<T> container = new List<T>();

            public virtual T Top()
            {
                return container[0];
            }

            public virtual bool Empty()
            {
                return container.Count == 0;
            }

            public virtual int Size()
            {
                return container.Count;
            }

            public virtual void Push(T value)
            {
                // 1. ���� �� ���� �����͸� �����Ѵ�.
                container.Add(value);

            // 2. ���� �Һ����� ������ ������ �����͸� �ٲ��ش�.
                // ù ��° ��带 1�� �����Ѵ�.
                int currentIndex = Size();

                while (currentIndex > 1)
                {
                    // 2-1. �θ� ��带 ã�´�.
                    int parentIndex = currentIndex / 2;

                    // 2-2. �θ� ���� ���Ѵ�.
                    // MaxHeap
                    //if (container[parentIndex - 1] >= container[currentIndex - 1])
                    //{
                    //    // 2-2-1. �θ� ������ �� ũ�ٸ� ���� �Һ����� �����ϴ� ���̹Ƿ� ����
                    //    break;
                    //}

                    // 2-3. �θ� ���� �ٲٰ� ���� ��ġ�� �����Ѵ�.
                    Swap(parentIndex - 1, currentIndex - 1);
                    currentIndex = parentIndex;
                }
            }

            protected virtual void Swap(int parIndex, int curIndex)
            {
                int temp = container[parIndex];
                container[parIndex] = container[curIndex];
                container[curIndex] = temp;
            }

            public virtual T Pop()
            {
                T top = Top();



                return top;
            }

            public virtual void Clear()
            {
                container.Clear();
            }
        }
    */
    #endregion
}
