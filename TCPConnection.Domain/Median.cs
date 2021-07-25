using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// median within array of integers
    /// </summary>
    public class Median {
        private readonly int[] _array;
        public Median(int[] array) {
            _array = array;
        }
        /// <summary>
        /// returns median within array of integers
        /// </summary>
        /// <returns></returns>
        public decimal ToDecimal() {
            return (_array.Length % 2 != 0) ?
                QuickSelect(_array.Length / 2, 0, _array.Length - 1) :
                decimal.Add(
                    QuickSelect((_array.Length / 2), 0, _array.Length - 1),
                    QuickSelect((_array.Length / 2) + 1, 0, _array.Length - 1)
                ) / 2;
        }
        private int QuickSelect(int kthLowestIndex, int leftIndex, int rightIndex) {
            if ((rightIndex - leftIndex) <= 0) {
                return _array[leftIndex];
            }
            var pivotIndex = Partition(leftIndex, rightIndex);
            if (kthLowestIndex < pivotIndex) {
                return QuickSelect(kthLowestIndex, leftIndex, pivotIndex - 1);
            } else if (kthLowestIndex > pivotIndex) {
                return QuickSelect(kthLowestIndex, pivotIndex + 1, rightIndex);
            }
            return _array[pivotIndex];
        }
        private int Partition(int leftPointer, int rightPointer) {
            var pivotIndex = rightPointer;
            var pivot = _array[pivotIndex];
            rightPointer--;
            while (true) {
                while (_array[leftPointer] < pivot) {
                    leftPointer++;
                }
                while (rightPointer > -1 && _array[rightPointer] > pivot) {
                    rightPointer--;
                }
                if (leftPointer >= rightPointer) {
                    break;
                } else {
                    Swap(leftPointer, rightPointer);
                    leftPointer += 1;
                }
            }
            Swap(leftPointer, pivotIndex);
            return leftPointer;
        }
        private void Swap(int firstIndex, int secondIndex) {
            var copy = _array[firstIndex];
            _array[firstIndex] = _array[secondIndex];
            _array[secondIndex] = copy;
        }
    }
}
