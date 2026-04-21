using System;
using UnityEditor;

namespace Engage.BuildTools
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SettingAttribute : Attribute
    {
        private readonly string key;

        public SettingAttribute(string key)
        {
            this.key = key;
        }
    }

    public interface ISetting
    {
        void Reset();
    }

    public class Setting<T> : ISetting
    {
        private bool hasValue;
        public bool HasValue => typeof(T).IsValueType ? hasValue : value != null;

        public Action<T> OnUpdate;

        private T value;
        public T Value
        {
            get
            {
                if (!HasValue && typeof(T).IsValueType)
                {
                    throw new InvalidOperationException();
                }

                return value;
            }
            set
            {
                if (!this.value.Equals(value))
                {
                    OnUpdate?.Invoke(value);
                }

                this.value = value;
            }
        }

        public T ResetValue;

        public Setting(T setting)
        {
            hasValue = typeof(T).IsValueType;
            value = setting;
            ResetValue = setting;
            OnUpdate = null;
        }

        public void Reset()
        {
            value = ResetValue;
            OnUpdate?.Invoke(ResetValue);
        }

        public static implicit operator T (Setting<T> setting) => setting.Value;
        public static implicit operator Setting<T>(T setting) => new Setting<T>(setting);
    }

    public class Settings<T> where T : new()
    {
        private static T instance;
        protected static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }

        protected static string GetKey(string name) => name + " Key";

        protected void RegisterBool(string key, ref Setting<bool> setting, bool defaultValue)
        {
            setting = EditorPrefs.GetBool(key, defaultValue);
            setting.ResetValue = defaultValue;
            setting.OnUpdate = (update) => EditorPrefs.SetBool(key, update);
        }

        protected void RegisterFloat(string key, ref Setting<float> setting, float defaultValue)
        {
            setting = EditorPrefs.GetFloat(key, defaultValue);
            setting.ResetValue = defaultValue;
            setting.OnUpdate = (update) => EditorPrefs.SetFloat(key, update);
        }

        protected void RegisterInt(string key, ref Setting<int> setting, int defaultValue)
        {
            setting = EditorPrefs.GetInt(key, defaultValue);
            setting.ResetValue = defaultValue;
            setting.OnUpdate = (update) => EditorPrefs.SetInt(key, update);
        }

        protected void RegisterString(string key, ref Setting<string> setting, string defaultValue)
        {
            setting = EditorPrefs.GetString(key, defaultValue);
            setting.ResetValue = defaultValue;
            setting.OnUpdate = (update) => EditorPrefs.SetString(key, update);
        }

        protected void Reset(string key, ISetting setting)
        {
            EditorPrefs.DeleteKey(key);
            setting.Reset();
        }
    }
}