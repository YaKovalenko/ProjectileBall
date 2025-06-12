using UnityEngine;

namespace Core.Services.InputService
{
    public interface IInputService
    {
        bool IsPointerPressed();
        Vector2 GetPointerPosition();
        bool TryGetClickedObject(out GameObject obj);
    }
}