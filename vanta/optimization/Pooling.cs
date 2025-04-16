using System.Collections.Generic;
using UnityEngine;

namespace vanta.optimization
{
    internal class Pooling<T> where T : MonoBehaviour
    {
        private Stack<T> pool = new Stack<T>();
        private GameObject prefab;
    }
}
