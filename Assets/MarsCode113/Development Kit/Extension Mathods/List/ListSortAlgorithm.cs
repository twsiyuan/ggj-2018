using System;
using System.Collections.Generic;

namespace ListExtension.SortAlgorithm
{
    public static class ListSortAlgorithm
    {

        private static void Swap<T>(this List<T> data, int index1, int index2)
        {
            if(index1 == index2)
                return;

            T temp = data[index1];
            data[index1] = data[index2];
            data[index2] = temp;
        }


        #region [ 快速排序法 ]

        public static void QuickSort<T>(this List<T> data) where T : IComparable<T>
        {
            QuickSort(data, 0, data.Count - 1);
        }


        private static void QuickSort<T>(List<T> data, int left, int right) where T : IComparable<T>
        {
            if(left >= right)
                return;

            var i = left - 1;   // 指派左側索引
            var j = right;      // 指派右側索引

            while(i < j) {
                // i 遞增並且往右尋找大於或等於基準的對象，i 必須小於 j
                while(++i < j && data[i].CompareTo(data[right]) < 0)
                    ;

                // j 遞減並且往左尋找小於或等於基準的對象，j 必須大於 i
                while(--j > i && data[j].CompareTo(data[right]) > 0)
                    ;

                // 交換元素並且繼續迴圈
                if(i < j)
                    Swap(data, i, j);
            }

            Swap(data, i, right);           // 調換指標元素與 i 元素
            QuickSort(data, left, i - 1);   // 切割指標 i 左側物件的遞迴排序
            QuickSort(data, i + 1, right);  // 切割指標 i 右側物件的遞迴排序
        }

        #endregion


        #region [ 插入排序法 ]

        public static void InsertionSort<T>(this List<T> data) where T : IComparable<T>
        {
            var n = data.Count;
            int i, j;
            T temp;

            for(i = 1; i < n; i++) {
                temp = data[i];
                j = i - 1;

                while(j > -1 && data[j].CompareTo(temp) > 0) {
                    data[j + 1] = data[j];
                    j--;
                }

                data[j + 1] = temp;
            }
        }

        #endregion


        #region [ 希爾排序法 ]

        public static void ShellSort<T>(this List<T> data) where T : IComparable<T>
        {
            var n = data.Count;
            var gap = n / 2;
            int i, j;
            T temp;

            while(gap > 0) {
                for(i = gap; i < n; i++) {
                    temp = data[i];
                    j = i - gap;

                    while(j > -1 && data[j].CompareTo(temp) > 0) {
                        data[j + gap] = data[j];
                        j -= gap;
                    }

                    data[j + gap] = temp;
                }

                gap /= 2;
            }
        }

        #endregion


        #region [ 堆積排序法 ]

        public static void HeapSort<T>(this List<T> data) where T : IComparable<T>
        {
            var n = data.Count;

            for(var p = (n / 2) - 1; p >= 0; p--)
                MaxHeapify(data, p, n);

            for(n = n - 1; n > 0; n--) {
                Swap(data, 0, n);
                MaxHeapify(data, 0, n);
            }
        }


        private static void MaxHeapify<T>(List<T> data, int parent, int range) where T : IComparable<T>
        {
            var max = parent;
            var leafL = parent * 2 + 1;
            var leafR = parent * 2 + 2;

            if(leafL < range && data[leafL].CompareTo(data[max]) > 0)
                max = leafL;

            if(leafR < range && data[leafR].CompareTo(data[max]) > 0)
                max = leafR;

            if(max != parent) {
                Swap(data, parent, max);
                MaxHeapify(data, max, range);
            }
        }

        #endregion

    }

    /**************************************************
    * 描述
    *   整合了常用的排序法，資料型態為 IComparable 泛型
    * 
    * 說明
    *   快速排序法(不穩定排序)
    *   - 絕大部分使用都會得到很好的效率
    *   - 不適合用在已經大致排序過的資料對象
    *   - 不適合用於反轉排序
    *   
    *   插入排序法(穩定排序)
    *   - 針對已經大致排序過的資料會有非常優秀的效率
    *   - 需要穩定排序的時候
    *   
    *   希爾排序法(不穩定排序)
    *   - 資料長度較小
    *   - 適合用於反轉排序
    *   
    *   堆積排序法(不穩定排序)
    *   - 資料長度很大的時候
    *   
    * 用法
    *   引用 ListSortAlgorithm
    *   泛型 T 必須實作 IComparable 介面
    *   
    *   ex:使用堆積排序法進行排序
    *   -> list.HeapSort();
    **************************************************/

}



