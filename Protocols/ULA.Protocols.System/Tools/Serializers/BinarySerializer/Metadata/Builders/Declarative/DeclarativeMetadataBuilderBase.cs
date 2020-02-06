using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders.Declarative
{
    /// <summary>
    /// Represents declarative metadata builder, that gets information about type metadata using attributes
    /// </summary>
    public abstract class DeclarativeMetadataBuilderBase : IMetadataBuilder
    {
        #region [Constants]
        protected const string NULL_TYPE_MESSAGE = "Can't build metadata for Null type";
        protected const string NOT_MARKED_TYPE_MESAGE = "Can't create metadata for referenced type that is not marked as binary serializable";
        protected const string ABSTRACT_TYPE_MESSAGE = "Can't create metadata for abstract type";
        protected const string NO_DEFAULT_CONSTRUCTOR_MESSAGE = "Can't create metadata for type without default constructor";
        protected const string UNEXPECTED_EXCEPTION_MESSAGE = "An unexpected exception has been occured while building mmeber collection";
        protected const string BAD_PROPERTY_MESSAGE = "Can't create metadata for the type that has at least one property that is not read/writtable";
        protected const string BAD_METADATABUILDER_MESSAGE = "Can't create metadatabuilder for type";
        private const string DUBLICATED_OFFSET_ON_MEMBER_MESSAGE_PATTERN = "Dublicated offset on member '{0}'";
        private const string BUILDING_ERROR_MESSAGE_PATTERN = "An exception occured while building metadata for type: {0}";

        private const BindingFlags AVAILABLE_BINDING_FLAGS = BindingFlags.Instance |
                                                             BindingFlags.NonPublic |
                                                             BindingFlags.Public;
        #endregion [Constants]


        #region [Private fields]
        protected Type _type;
        protected Func<Type, object> _infoAccessor;
        #endregion [Private fields]


        #region [IMetadataBuilder members]
        /// <summary>
        /// Sets a type to build metadata for
        /// </summary>
        /// <param name="type"></param>
        public void SetTypeToBuild(Type type)
        {
            Guard.AgainstNullReference<MetadataBuilderException>(type, "type", NULL_TYPE_MESSAGE);
            
            this._type = type;
        }
        /// <summary>
        /// Gets a type information accessor
        /// </summary>
        /// <param name="informationAccessor"></param>
        public void SetInfoAccessor(Func<Type, object> informationAccessor)
        {
            this._infoAccessor = informationAccessor;
        }
        /// <summary>
        /// Builds metadata for a type
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <returns>Built metadata</returns>
        public abstract ISerializableTypeMetadata Build();
        /// <summary>
        /// Gets a type to build metadata for
        /// </summary>
        public Type TypeToBuild
        {
            get { return this._type; }
        }
        #endregion [IMetadataBuilder members]


        #region [virtual members]
        /// <summary>
        /// Builds metadata members collection for type's members, such as fields and properties
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="type">The type to build metadata members collection for</param>
        /// <returns>Built metadata members collection</returns>
        protected virtual MemberMetadata[] CreateMemberCollection(Type type)
        {
            try
            {
                var result = new SortedList<int, MemberMetadata>();
                this.ProcessFields(type, result);
                this.ProcessProperties(type, result);
                return result.Select(s => s.Value).ToArray();
            }
            catch (Exception error)
            {
                Guard.Throw<MetadataBuilderException>(
                    string.Format(BUILDING_ERROR_MESSAGE_PATTERN, type.FullName), error);
            }
            return null;
        }
        #endregion [virtual members]


        #region [Abstract members]
        /// <summary>
        /// Creates a metadata bulder for embedded custom type (the factory method pattern)
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="type">The type to create metadata builder for</param>
        /// <returns>The current metadata builder for the input type</returns>
        protected abstract IMetadataBuilder CreateTypeMetadataBuilder(Type type);
        #endregion [Abstract members]


        #region [Help members]
        /// <summary>
        /// Gets a metadata builder for a type
        /// </summary>
        /// <param name="infoAccessor">The accessor delegate instance for access to the type value information</param>
        /// <param name="type">The type to get metadata builder for</param>
        /// <returns>The current metadata builder for a type</returns>
        protected IMetadataBuilder GetMetadataBuilder(Func<Type, object> infoAccessor, Type type = null)
        {
            var currentType = type ?? this._type;
            if (currentType.Equals(typeof (string)))
            {
                var builder = new StringTypeMetadataBuilder();
                builder.SetInfoAccessor(infoAccessor);
                return builder;
            }

            if (currentType.IsPrimitive)
            {
                IMetadataBuilder builder;
                if (currentType.Equals(typeof (char)))
                {
                    builder = new CharTypeMetadataBuilder();
                    builder.SetInfoAccessor(infoAccessor);
                    return builder;
                }
                builder = new PrimitiveTypeMetadataBuilder();
                builder.SetTypeToBuild(currentType);
                return builder;
            }

            if (currentType.IsArray)
            {
                var builder = new ArrayTypeMetadataBuilder(new DeclarativeMetadataBuilder());
                builder.SetTypeToBuild(currentType);
                builder.SetInfoAccessor(infoAccessor);
                return builder;
            }

            if (currentType.IsInterface)
            {
                if (currentType.IsGenericType && 
                    (currentType.GetGenericTypeDefinition() == typeof(IEnumerable<>) 
                    || currentType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    )
                {
                    var builder = new GenericCollectionTypeMetadataBuilder(new DeclarativeMetadataBuilder());
                    builder.SetTypeToBuild(currentType);
                    builder.SetInfoAccessor(infoAccessor);
                    return builder;
                }
            }

            if (currentType.IsClass || currentType.IsValueType)
                return this.CreateTypeMetadataBuilder(currentType);

            Guard.Throw<MetadataBuilderException>(BAD_METADATABUILDER_MESSAGE, new NotSupportedException());
            return null;
        }
        
        private void ProcessProperties(IReflect type, IDictionary<int, MemberMetadata> result)
        {
            foreach (var property in type.GetProperties(AVAILABLE_BINDING_FLAGS).Where(new MemberSpecification()))
            {
                ValidateSerializableProperty(property);
                var index = GetMemberOffset(property);
                Guard.AgainstIsTrue<MetadataBuilderException>(result.ContainsKey(index),
                    string.Format(DUBLICATED_OFFSET_ON_MEMBER_MESSAGE_PATTERN, property.Name));

                var memberInfo = (IMemberInfo)Activator.CreateInstance(
                        typeof(DeclarativePropertyInfoMember<,>).MakeGenericType(new[] { this._type, property.PropertyType }), new object[] { property });
                result.Add(index, this.CreateMemberMetadata(memberInfo, IsSkipped(property)));
            }
        }
        private void ProcessFields(IReflect type, IDictionary<int, MemberMetadata> result)
        {
            foreach (var field in type.GetFields(AVAILABLE_BINDING_FLAGS).Where(new MemberSpecification()))
            {
                var index = GetMemberOffset(field);
                Guard.AgainstIsTrue<MetadataBuilderException>(result.ContainsKey(index),
                     string.Format(DUBLICATED_OFFSET_ON_MEMBER_MESSAGE_PATTERN, field.Name));

                var memberInfo = (IMemberInfo)Activator.CreateInstance(
                    typeof(DeclarativeFieldInfoMember<,>).MakeGenericType(new[] { this._type, field.FieldType }), new object[] { field });

                result.Add(index, this.CreateMemberMetadata(memberInfo, IsSkipped(field)));
            }
        }
        private MemberMetadata CreateMemberMetadata(IMemberInfo memberInfo, bool isSkipped)
        {
            return new MemberMetadataBuilder(memberInfo,
                        this.GetMetadataBuilder(memberInfo.GetInfo, memberInfo.GetMemberType()), isSkipped)
                        .Build();
        }
        private static bool IsSkipped(MemberInfo memberInfo)
        {
            return Attribute.GetCustomAttribute(memberInfo, typeof (BinarySkipMemberAttribute)) != null;
        }
        private static int GetMemberOffset(MemberInfo field)
        {
            return (int)((BinarySerializableMemberAttribute)Attribute.GetCustomAttribute(field,
                               typeof(BinarySerializableMemberAttribute))).Offset;
        }
        private static void ValidateSerializableProperty(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
                Guard.Throw<MetadataBuilderException>(BAD_PROPERTY_MESSAGE, new NotSupportedException());
        }
        #endregion [Help members]
    }
}