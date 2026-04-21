using UnityEngine;
namespace Engage.AFX.v1
{
    [System.Serializable]
    public class ObjectReferenceSlot : System.IComparable
    {
        [SerializeField]
        private string referenceName;
        [SerializeField]
        private Object referenceValue;
        [SerializeField]
        private string typeAsString;

        public string ReferenceName { get => referenceName; }

        public System.Type SlotType
        {
            get
            {
                if (string.IsNullOrEmpty(typeAsString))
                {
                    if (referenceValue != null)
                    {
                        typeAsString = referenceValue.GetType().AssemblyQualifiedName;
                    }
                    else
                    {
                        return null;
                    }
                }

                return System.Type.GetType(typeAsString);
            }
        }

        public Object ReferenceValue
        {
            get => referenceValue;
            set
            {
                if (value == null || value.GetType() != SlotType)
                {
                    Debug.LogError($"[{this}] Reference Value must match slot type: {SlotType} You tired to add {value.GetType()}");
                    return;
                }

                referenceValue = value;
            }
        }

        public ObjectReferenceSlot(string referenceNameIN, UnityEngine.Object referenceValueIN, System.Type typeIn)
        {
            referenceName = referenceNameIN;
            referenceValue = referenceValueIN;
            typeAsString = typeIn.AssemblyQualifiedName;
        }

        public int CompareTo(object obj)
        {
            var otherRefSlot = obj as ObjectReferenceSlot;
            if (otherRefSlot == null) return 1;
            if (otherRefSlot.SlotType == null) return 1;
            if (otherRefSlot.referenceName == null) return 1;

            int result = typeAsString.CompareTo(otherRefSlot.SlotType.AssemblyQualifiedName);

            if (result != 0) return result;

            return referenceName.CompareTo(otherRefSlot.referenceName);
        }
    }
}
