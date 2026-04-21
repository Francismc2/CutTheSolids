using System;
using UnityEngine;

namespace Engage.AFX.Anchor.v1
{
    [DefaultExecutionOrder(1)]
    [AddComponentMenu("AFX/Anchor/Anchor")]
    public class Anchor : MonoBehaviour
    {
        [SerializeField] private Transform anchorTransform;
        [SerializeField] private bool maintainOffset = true;

        public bool MaintainOffset { get => maintainOffset; set => maintainOffset = value; }
        public Transform ChildTransform { get => anchorTransform; set => anchorTransform = value; }
        public IAnchorSocket Socket { get => socket; set => socket = value; }

        public delegate void MaintainOffsetDelegate(bool maintainOffset);
        public delegate void EditModeDelegate(bool editMode);
        public delegate void SocketDelegate(IAnchorSocket socket);
        public delegate void OffsetDelegate(Vector3 position, Quaternion rotation, Vector3 scale);

        public MaintainOffsetDelegate MaintainOffsetChanged;
        public EditModeDelegate EditModeChanged;
        public SocketDelegate SocketChanged;
        public OffsetDelegate OffsetChanged;

        private IAnchorSocket socket;

        private Vector3 offsetPosition;
        private Quaternion offsetRotation;
        private Vector3 offsetScale;

        private bool editMode  = false;

        private void OnEnable()
        {
            SetupOffset();
        }

        private void LateUpdate()
        {
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            if (editMode) return;
            if (anchorTransform == null || socket == null) return;
            if (socket?.SocketTransform == null)
            {
                ClearSocket();
                return;
            }

            anchorTransform.SetPositionAndRotation(socket.SocketTransform.TransformPoint(offsetPosition), socket.SocketTransform.rotation * offsetRotation);
            anchorTransform.localScale = Vector3.Scale(socket.SocketTransform.localScale, offsetScale);
        }

        public void SetupOffset()
        {
            if (anchorTransform == null || socket == null) return;

            if (!maintainOffset)
            {
                anchorTransform.SetPositionAndRotation(socket.SocketTransform.position, socket.SocketTransform.rotation);
            }

            offsetPosition = socket.SocketTransform.InverseTransformPoint(anchorTransform.position);
            offsetRotation = Quaternion.Inverse(socket.SocketTransform.rotation) * anchorTransform.rotation;
            offsetScale = Vector3.Scale(anchorTransform.localScale, socket.SocketTransform.localScale);

            OffsetChanged?.Invoke(offsetPosition, offsetRotation, offsetScale);
        }

        public void SetMaintainOffset(bool maintainOffset)
        {
            this.maintainOffset = maintainOffset;
            SetupOffset();
            MaintainOffsetChanged?.Invoke(maintainOffset);
        }

        public void SetEditMode(bool editmode)
        {
            this.editMode = editmode;
            if (!editmode) SetupOffset();
            EditModeChanged?.Invoke(editmode);
        }

        public void SetSocket(IAnchorSocket socket)
        {
            if (socket == null) return;
            this.socket = socket;

            SetupOffset();
            SocketChanged?.Invoke(socket);
        }

        public void ClearSocket()
        {
            socket = null;
            SocketChanged?.Invoke(null);
        }
    }
}