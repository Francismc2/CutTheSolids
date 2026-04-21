using UnityEngine;

namespace Engage.AFX.v1
{
    public class ValueComponent<T> : MonoBehaviour
    {
        [SerializeField]
        private T value;

        public T Value { get => value; set => this.value = value; }
    }
}