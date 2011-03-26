#region Header
/**
 * IJsonWrapper.cs
 *   Interface that represents a type capable of handling all kinds of JSON
 *   data. This is mainly used when mapping objects through JsonMapper, and
 *   it's implemented by JsonData.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using System.Collections;
using System.Collections.Specialized;


namespace LitJson
{
	//	定义Json数据类型
    public enum JsonType
    {
        None,

        Object,
        Array,
        String,
        Int,
        Long,
        Double,
        Boolean
    }

    //	从IList, IOrderedDictionary派生出接口，然后在JsonData中提供实现
    public interface IJsonWrapper : IList, IOrderedDictionary
    {
    	//	定义判断数据类型的属性
        bool IsArray   { get; }
        bool IsBoolean { get; }
        bool IsDouble  { get; }
        bool IsInt     { get; }
        bool IsLong    { get; }
        bool IsObject  { get; }
        bool IsString  { get; }

        //	获取指定类型的数据
        bool     GetBoolean ();
        double   GetDouble ();
        int      GetInt ();
        JsonType GetJsonType ();
        long     GetLong ();
        string   GetString ();

        //	设置指定类型的数据
        void SetBoolean  (bool val);
        void SetDouble   (double val);
        void SetInt      (int val);
        void SetJsonType (JsonType type);
        void SetLong     (long val);
        void SetString   (string val);

        //	把数据化为Json格式
        string ToJson ();
        void   ToJson (JsonWriter writer);
    }
}
