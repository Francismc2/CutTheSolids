using UnityEngine;

namespace Engage.AFX.v1
{
    public class ValueComponent<T> : MonoBehaviour
    {
        [SerializeField, StringOnlyTextArea(3, 10)]
        private T value;

        public T Value { get => value; set => this.value = value; }
    }
}