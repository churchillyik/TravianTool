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
	//	����Json��������
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

    //	��IList, IOrderedDictionary�������ӿڣ�Ȼ����JsonData���ṩʵ��
    public interface IJsonWrapper : IList, IOrderedDictionary
    {
    	//	�����ж��������͵�����
        bool IsArray   { get; }
        bool IsBoolean { get; }
        bool IsDouble  { get; }
        bool IsInt     { get; }
        bool IsLong    { get; }
        bool IsObject  { get; }
        bool IsString  { get; }

        //	��ȡָ�����͵�����
        bool     GetBoolean ();
        double   GetDouble ();
        int      GetInt ();
        JsonType GetJsonType ();
        long     GetLong ();
        string   GetString ();

        //	����ָ�����͵�����
        void SetBoolean  (bool val);
        void SetDouble   (double val);
        void SetInt      (int val);
        void SetJsonType (JsonType type);
        void SetLong     (long val);
        void SetString   (string val);

        //	�����ݻ�ΪJson��ʽ
        string ToJson ();
        void   ToJson (JsonWriter writer);
    }
}
