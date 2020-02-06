using System;
using System.Linq.Expressions;
using System.Reflection;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.StrongTypedReflection
{
    /// <summary>
    /// Represents strongly type reflection mechanism
    /// </summary>
    public static class StrongTypeReflection
    {
        #region [Constants]
        private const string METODCALL_ERRORMESSAGE = "Method call expression is unsupportable";
        private const string INVALIDEXPRESSION_ERRORMESSAGE = "Invalid expression";
        #endregion [Constants]


        #region [Nested types]
        /// <summary>
        /// Represents access for strongly type reflection to static members
        /// </summary>
        public static class StaticMembers
        {
            /// <summary>
            /// Gets the System.Reflection.FieldInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current expression member to get System.Reflection.FieldInfo instance from</param>
            /// <returns>System.Reflection.FieldInfo instance for current expression member</returns>
            public static FieldInfo Field<T>(Expression<Func<T>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<FieldInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.PropertyField instance for current expression member
            /// </summary>
            /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current expression member to get System.Reflection.PropertyInfo instance from</param>
            /// <returns>System.Reflection.PropertyInfo instance for current expression member</returns>
            public static PropertyInfo Property<T>(Expression<Func<T>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<PropertyInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <param name="member">The current expression member to get System.Reflection.MethodInfo instance from</param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method(Expression<Action> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current expression member to get System.Reflection.MethodInfo instance from</param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T>(Expression<Action<T>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current expression member to get System.Reflection.MethodInfo instance from</param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2>(Expression<Action<T1, T2>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current axpression member to get System.Reflection.MethodInfo instance from</param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3>(Expression<Action<T1, T2, T3>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T4">The type of the forth parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current axpression member to get System.Reflection.MethodInfo instnace from</param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3, T4>(Expression<Action<T1, T2, T3, T4>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T4">The type of the forth parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current axpression member to get System.Reflection.MethodInfo instnace from</param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3, T4, T5>(Expression<Action<T1, T2, T3, T4, T5>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T4">The type of the forth parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member">The current axpression member to get System.Reflection.MethodInfo instnace from</param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3, T4, T5, T6>(Expression<Action<T1, T2, T3, T4, T5, T6>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
        }
        /// <summary>
        /// Represents access for strongly type reflection to instance members
        /// </summary>
        /// <typeparam name="TMember">The type of instance to get members from</typeparam>
        public static class InstanceOf<TMember>
        {
            /// <summary>
            /// Gets the System.Reflection.FieldInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.FieldInfo instance for current expression member</returns>
            public static FieldInfo Field<T>(Expression<Func<TMember, T>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<FieldInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.PropertyField instance for current expression member
            /// </summary>
            /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.PropertyInfo instance for current expression member</returns>
            public static PropertyInfo Property<T>(Expression<Func<TMember, T>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<PropertyInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <param name="member"></param>
            /// <returns></returns>
            public static MethodInfo Method(Expression<Action<TMember>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T>(Expression<Action<TMember, T>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2>(Expression<Action<TMember, T1, T2>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3>(Expression<Action<TMember, T1, T2, T3>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T4">The type of the forth parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3, T4>(Expression<Action<TMember, T1, T2, T3, T4>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T4">The type of the forth parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3, T4, T5>(Expression<Action<TMember, T1, T2, T3, T4, T5>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
            /// <summary>
            /// Gets the System.Reflection.MethodInfo instance for current expression member
            /// </summary>
            /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T4">The type of the forth parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates</typeparam>
            /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates</typeparam>
            /// <param name="member"></param>
            /// <returns>System.Reflection.MethodInfo instance for current expression member</returns>
            public static MethodInfo Method<T1, T2, T3, T4, T5, T6>(Expression<Action<TMember, T1, T2, T3, T4, T5, T6>> member) { return StrongTypeReflection.GetMemberFromLambdaExpression<MethodInfo>(member); }
        }
        /// <summary>
        /// Represents the name-value pair for strongly type reflection members 
        /// (Only for fields, method parameters and properties)
        /// </summary>
        /// <typeparam name="T">The type of value of the strongly type reflection member</typeparam>
        public struct MemberNameValuePair<T>
        {
            #region [Private fields]
            private readonly string _name;
            private readonly T _value;
            #endregion [Private fields]


            #region [Properties]
            /// <summary>
            /// The name of the strongly type reflection member
            /// </summary>
            public string Name { get { return this._name; } }
            /// <summary>
            /// The value of the strongly type reflection member
            /// </summary>
            public T Value { get { return this._value; } }
            #endregion [Properties]


            #region [Ctor's]
            /// <summary>
            /// Initializes the instance of <see cref="MemberNameValuePair{T}"/>
            /// </summary>
            /// <param name="name">The name of the strongly type reflection member</param>
            /// <param name="value">The value of the strongly type reflection member</param>
            public MemberNameValuePair(string name, T value)
            {
                this._name = name;
                this._value = value;
            }
            #endregion [Ctor's]
        }
        #endregion [Nested types]


        #region [Public members]
        /// <summary>
        /// Gets the name of a code token using strongly type reflection
        /// </summary>
        /// <typeparam name="T">The type of value represents by a code token</typeparam>
        /// <param name="member">The code token to get the name as expression instance</param>
        /// <returns>The name of a code token</returns>
        public static string GetMemberName<T>(Expression<Func<T>> member)
        {
            return StrongTypeReflection.GetMemberFromExpression<MemberInfo>(member.Body).Name;
        }
        /// <summary>
        /// Gets the name of a code token using strongly type reflection
        /// </summary>
        /// <typeparam name="TMember">The type of instance that contains current code token</typeparam>
        /// <typeparam name="T">The type of value represents by a code token</typeparam>
        /// <param name="member">The code token to get the name as expression instance</param>
        /// <returns>The name of a code token</returns>
        public static string GetMemberName<TMember, T>(Expression<Func<TMember, T>> member)
        {
            return StrongTypeReflection.GetMemberFromExpression<MemberInfo>(member.Body).Name;
        }
        /// <summary>
        /// Gets the name of a code token using strongly type reflection
        /// </summary>
        /// <typeparam name="T">The type of value represents by a code token</typeparam>
        /// <param name="member">The code token to get the name as expression instance</param>
        /// <returns>The name of a code token</returns>
        public static string GetMemberName<T>(Expression<Action<T>> member)
        {
            return StrongTypeReflection.GetMemberFromExpression<MemberInfo>(member.Body).Name;
        }
        /// <summary>
        /// Gets the name-value pair of a code token using strongly type reflection
        /// </summary>
        /// <typeparam name="T">The type of value represents by a code token</typeparam>
        /// <param name="member">The code token to get the name as expression instance</param>
        /// <returns>The name-value pair of a code token</returns>
        public static MemberNameValuePair<T> GetMemberNameValuePair<T>(Expression<Func<T>> member)
        {
            var memberExpressionBody = member.Body as MemberExpression;
            if (memberExpressionBody != null)
                return StrongTypeReflection.GetMemberValueNamePair<T>(memberExpressionBody);
            var unaryExpresionBody = member.Body as UnaryExpression;
            Guard.AgainstNullReference<StrongTypeReflectionException>(unaryExpresionBody, StrongTypeReflection.METODCALL_ERRORMESSAGE);
            // ReSharper disable PossibleNullReferenceException
            var unaryExpressionBodyOperand = unaryExpresionBody.Operand as MemberExpression;
            Guard.AgainstNullReference<StrongTypeReflectionException>(unaryExpressionBodyOperand, StrongTypeReflection.METODCALL_ERRORMESSAGE);
            return StrongTypeReflection.GetMemberValueNamePair<T>(unaryExpressionBodyOperand);
        }
        #endregion [Public members]


        #region [Help members]
        private static T GetMemberFromLambdaExpression<T>(LambdaExpression expression) where T : MemberInfo { return GetMemberFromExpression<T>(expression.Body); }
        private static MemberNameValuePair<T> GetMemberValueNamePair<T>(MemberExpression expression)
        {
            var name = expression.Member.Name;
            var value = (T)((FieldInfo)expression.Member).GetValue(((ConstantExpression)expression.Expression).Value);
            return new MemberNameValuePair<T>(name, value);
        }
        private static T GetMemberFromExpression<T>(Expression expression) where T : MemberInfo
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member as T;
            var methodCallExpression = expression as MethodCallExpression;
            if (methodCallExpression != null)
                return methodCallExpression.Method as T;
            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
                return GetMemberFromUnaryExpression<T>(unaryExpression);
            Guard.Throw<StrongTypeReflectionException>(StrongTypeReflection.INVALIDEXPRESSION_ERRORMESSAGE);
            return null;
        }
        private static T GetMemberFromUnaryExpression<T>(UnaryExpression unaryExpression) where T : MemberInfo
        {
            var methodCallExpression = unaryExpression.Operand as MethodCallExpression;
            if (methodCallExpression != null)
                return methodCallExpression.Method as T;
            return ((MemberExpression)unaryExpression.Operand).Member as T;
        }
        #endregion [Help members]
    }
}
