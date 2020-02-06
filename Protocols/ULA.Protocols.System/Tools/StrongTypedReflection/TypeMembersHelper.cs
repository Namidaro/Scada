using System;
using System.Reflection;
using System.Reflection.Emit;

namespace YP.Toolkit.System.Tools.StrongTypedReflection
{
    public class TypeMembersHelper
    {
        public static Func<S, T> CreateFieldGetter<S, T>(FieldInfo field)
        {
            var methodName = field.ReflectedType.FullName + ".get_" + field.Name;
            var setterMethod = new DynamicMethod(methodName, typeof(T), new Type[1] { typeof(S) }, true);
            var gen = setterMethod.GetILGenerator();
            if (field.IsStatic)
                gen.Emit(OpCodes.Ldsfld, field);
            else
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldfld, field);
            }
            gen.Emit(OpCodes.Ret);
            return (Func<S, T>)setterMethod.CreateDelegate(typeof(Func<S, T>));
        }
        public static Action<S, T> CreateFieldSetter<S, T>(FieldInfo field)
        {
            var methodName = field.ReflectedType.FullName + ".set_" + field.Name;
            var setterMethod = new DynamicMethod(methodName, null, new Type[2] { typeof(S), typeof(T) }, true);
            var gen = setterMethod.GetILGenerator();
            if (field.IsStatic)
            {
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Stsfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Stfld, field);
            }
            gen.Emit(OpCodes.Ret);
            return (Action<S, T>)setterMethod.CreateDelegate(typeof(Action<S, T>));
        }
        public static Func<S, T> CreatePropertyGetter<S, T>(PropertyInfo property)
        {
            return (Func<S, T>) Delegate.CreateDelegate(typeof (Func<S, T>), property.GetGetMethod());
        }
        public static Action<S, T> CreatePropertySetter<S, T>(PropertyInfo property)
        {
            return (Action<S, T>) Delegate.CreateDelegate(typeof (Action<S, T>), property.GetSetMethod());
        }
    }
}