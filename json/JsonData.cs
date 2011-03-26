#region Header
/**
 * JsonData.cs
 *   Generic type to hold JSON data (objects, arrays, and so on). This is
 *   the default type returned by JsonMapper.ToObject().
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;


namespace LitJson
{
	//	ʵ��IJsonWrapper�ӿڣ�ͬʱʵ��JsonData���͵ıȽϽӿں�ת���ӿ�
    public class JsonData : IJsonWrapper, IEquatable<JsonData>, IConvertible
    {
    	//	����JsonType��ö�ٵĸ��������ݣ��Լ����ݵļ��������ݶ�Ӧ��JsonType����
        #region Fields
        private IList<JsonData>               inst_array;
        private bool                          inst_boolean;
        private double                        inst_double;
        private int                           inst_int;
        private long                          inst_long;
        private IDictionary<string, JsonData> inst_object;
        private string                        inst_string;
        private string                        json;
        private JsonType                      type;

        //	����һ��<����ֵ>�ı�����ʵ��IOrderedDictionary�ӿ�
        private IList<KeyValuePair<string, JsonData>> object_list;
        #endregion


        //	ʵ��IJsonWrapper�ӿ����ж��������͵�����
        //	���ﻹʵ����һ��Count���ԣ���Ϊ�����ض���ICollection�ӿ��е����Զ�׼����
        #region Properties
        public int Count {
            get { return EnsureCollection ().Count; }
        }

        public bool IsArray {
            get { return type == JsonType.Array; }
        }

        public bool IsBoolean {
            get { return type == JsonType.Boolean; }
        }

        public bool IsDouble {
            get { return type == JsonType.Double; }
        }

        public bool IsInt {
            get { return type == JsonType.Int; }
        }

        public bool IsLong {
            get { return type == JsonType.Long; }
        }

        public bool IsObject {
            get { return type == JsonType.Object; }
        }

        public bool IsString {
            get { return type == JsonType.String; }
        }
        #endregion


        //	�ض���ICollection�ӿ��е�����
        #region ICollection Properties
        //	��������
        int ICollection.Count {
            get {
                return Count;
            }
        }

        //	�Ƿ�ͬ������
        bool ICollection.IsSynchronized {
            get {
                return EnsureCollection ().IsSynchronized;
            }
        }

        //	ͬ��������
        object ICollection.SyncRoot {
            get {
                return EnsureCollection ().SyncRoot;
            }
        }
        #endregion


        //	�ض���IDictionary�ӿ��е�����
        #region IDictionary Properties
        //	�Ƿ�̶���С����
        bool IDictionary.IsFixedSize {
            get {
                return EnsureDictionary ().IsFixedSize;
            }
        }

        //	�Ƿ�ֻ������
        bool IDictionary.IsReadOnly {
            get {
                return EnsureDictionary ().IsReadOnly;
            }
        }

        //	�������ԣ���ȡobject_list�е����м���
        ICollection IDictionary.Keys {
            get {
                EnsureDictionary ();
                IList<string> keys = new List<string> ();

                foreach (KeyValuePair<string, JsonData> entry in
                         object_list) {
                    keys.Add (entry.Key);
                }

                return (ICollection) keys;
            }
        }

        //	��ֵ���ԣ���ȡobject_list�е����м�ֵ
        ICollection IDictionary.Values {
            get {
                EnsureDictionary ();
                IList<JsonData> values = new List<JsonData> ();

                foreach (KeyValuePair<string, JsonData> entry in
                         object_list) {
                    values.Add (entry.Value);
                }

                return (ICollection) values;
            }
        }
        #endregion


		//	�ض���IJsonWrapper�ӿ��е�����
        #region IJsonWrapper Properties
        bool IJsonWrapper.IsArray {
            get { return IsArray; }
        }

        bool IJsonWrapper.IsBoolean {
            get { return IsBoolean; }
        }

        bool IJsonWrapper.IsDouble {
            get { return IsDouble; }
        }

        bool IJsonWrapper.IsInt {
            get { return IsInt; }
        }

        bool IJsonWrapper.IsLong {
            get { return IsLong; }
        }

        bool IJsonWrapper.IsObject {
            get { return IsObject; }
        }

        bool IJsonWrapper.IsString {
            get { return IsString; }
        }
        #endregion


        //	�ض���IList�ӿ��е�����
        #region IList Properties
        //	�Ƿ�̶���С����
        bool IList.IsFixedSize {
            get {
                return EnsureList ().IsFixedSize;
            }
        }

        //	�Ƿ�ֻ������
        bool IList.IsReadOnly {
            get {
                return EnsureList ().IsReadOnly;
            }
        }
        #endregion


        //	��Բ�ͬ���������ͣ��ض����������������м�����JsonData���������Ӧ��
        //	�ض���IDictionary�ӿ��е�������
        #region IDictionary Indexer
        object IDictionary.this[object key] {
            get {
                return EnsureDictionary ()[key];
            }

            set {
                if (! (key is String))
                    throw new ArgumentException (
                        "The key has to be a string");

                JsonData data = ToJsonData (value);

                this[(string) key] = data;
            }
        }
        #endregion


        //	�ض���IOrderedDictionary�ӿ��е�������
        #region IOrderedDictionary Indexer
        object IOrderedDictionary.this[int idx] {
            get {
                EnsureDictionary ();
                return object_list[idx].Value;
            }

            set {
                EnsureDictionary ();
                JsonData data = ToJsonData (value);
                
                KeyValuePair<string, JsonData> old_entry = object_list[idx];

                inst_object[old_entry.Key] = data;

                KeyValuePair<string, JsonData> entry =
                    new KeyValuePair<string, JsonData> (old_entry.Key, data);

                object_list[idx] = entry;
            }
        }
        #endregion


        //	�ض���IList�ӿ��е�������
        #region IList Indexer
        object IList.this[int index] {
            get {
                return EnsureList ()[index];
            }

            set {
                EnsureList ();
                JsonData data = ToJsonData (value);

                this[index] = data;
            }
        }
        #endregion


        //	����JsonData�����������Ϊinst_object�ֵ��Ա��object_list˳���ֵ��Ա��������
        #region Public Indexers
        public JsonData this[string prop_name] {
            get {
                EnsureDictionary ();
                return inst_object[prop_name];
            }

            set {
                EnsureDictionary ();

                KeyValuePair<string, JsonData> entry =
                    new KeyValuePair<string, JsonData> (prop_name, value);

                if (inst_object.ContainsKey (prop_name)) {
                    for (int i = 0; i < object_list.Count; i++) {
                        if (object_list[i].Key == prop_name) {
                            object_list[i] = entry;
                            break;
                        }
                    }
                } else
                    object_list.Add (entry);

                inst_object[prop_name] = value;

                json = null;
            }
        }

        public JsonData this[int index] {
            get {
                EnsureCollection ();

                if (type == JsonType.Array)
                    return inst_array[index];

                return object_list[index].Value;
            }

            set {
                EnsureCollection ();

                if (type == JsonType.Array)
                    inst_array[index] = value;
                else {
                    KeyValuePair<string, JsonData> entry = object_list[index];
                    KeyValuePair<string, JsonData> new_entry =
                        new KeyValuePair<string, JsonData> (entry.Key, value);

                    object_list[index] = new_entry;
                    inst_object[entry.Key] = value;
                }

                json = null;
            }
        }
        #endregion


        //	����JsonData��Ĺ��캯����������Object��Array���ͣ�
        #region Constructors
        public JsonData ()
        {
        }

        public JsonData (bool boolean)
        {
            type = JsonType.Boolean;
            inst_boolean = boolean;
        }

        public JsonData (double number)
        {
            type = JsonType.Double;
            inst_double = number;
        }

        public JsonData (int number)
        {
            type = JsonType.Int;
            inst_int = number;
        }

        public JsonData (long number)
        {
            type = JsonType.Long;
            inst_long = number;
        }

        public JsonData (object obj)
        {
            if (obj is Boolean) {
                type = JsonType.Boolean;
                inst_boolean = (bool) obj;
                return;
            }

            if (obj is Double) {
                type = JsonType.Double;
                inst_double = (double) obj;
                return;
            }

            if (obj is Int32) {
                type = JsonType.Int;
                inst_int = (int) obj;
                return;
            }

            if (obj is Int64) {
                type = JsonType.Long;
                inst_long = (long) obj;
                return;
            }

            if (obj is String) {
                type = JsonType.String;
                inst_string = (string) obj;
                return;
            }

            throw new ArgumentException (
                "Unable to wrap the given object with JsonData");
        }

        public JsonData (string str)
        {
            type = JsonType.String;
            inst_string = str;
        }
        #endregion


        //	������ʽת��<JsonData = value>��������Object��Array���ͣ�
        #region Implicit Conversions
        public static implicit operator JsonData (Boolean data)
        {
            return new JsonData (data);
        }

        public static implicit operator JsonData (Double data)
        {
            return new JsonData (data);
        }

        public static implicit operator JsonData (Int32 data)
        {
            return new JsonData (data);
        }

        public static implicit operator JsonData (Int64 data)
        {
            return new JsonData (data);
        }

        public static implicit operator JsonData (String data)
        {
            return new JsonData (data);
        }
        #endregion


        //	������ʽת��<value = (value_type)JsonData>��������Object��Array���ͣ�
        #region Explicit Conversions
				public static implicit operator Boolean(JsonData data)
        {
            if (data.type != JsonType.Boolean)
                throw new InvalidCastException (
                    "Instance of JsonData doesn't hold a boolean");

            return data.inst_boolean;
        }

				public static implicit operator Double(JsonData data)
        {
            if (data.type != JsonType.Double)
                throw new InvalidCastException (
                    "Instance of JsonData doesn't hold a double");

            return data.inst_double;
        }

				public static implicit operator Int32(JsonData data)
        {
            if (data.type != JsonType.Int)
                throw new InvalidCastException (
                    "Instance of JsonData doesn't hold an int");

            return data.inst_int;
        }

				public static implicit operator Int64(JsonData data)
        {
            if (data.type != JsonType.Long)
                throw new InvalidCastException (
                    "Instance of JsonData doesn't hold an int");

            return data.inst_long;
        }

				public static implicit operator String(JsonData data)
        {
            if (data.type != JsonType.String)
                throw new InvalidCastException (
                    "Instance of JsonData doesn't hold a string");

            return data.inst_string;
        }
        #endregion


        //	�ض���ICollection�ӿ��еķ���
        #region ICollection Methods
        void ICollection.CopyTo (Array array, int index)
        {
            EnsureCollection ().CopyTo (array, index);
        }
        #endregion


        //	�ض���IDictionary�ӿ��еķ���
        #region IDictionary Methods
        //	��Ӽ���ֵ
        void IDictionary.Add (object key, object value)
        {
            JsonData data = ToJsonData (value);

            EnsureDictionary ().Add (key, data);

            KeyValuePair<string, JsonData> entry =
                new KeyValuePair<string, JsonData> ((string) key, data);
            object_list.Add (entry);

            json = null;
        }

        //	�������
        void IDictionary.Clear ()
        {
            EnsureDictionary ().Clear ();
            object_list.Clear ();
            json = null;
        }

        //	�ж��Ƿ���ĳ����
        bool IDictionary.Contains (object key)
        {
            return EnsureDictionary ().Contains (key);
        }

        //	��ȡ��ǰ��ö�������൱���α꣩
        //	����ֱ�ӵ���IOrderedDictionary��GetEnumerator����
        //	���ص���object_list��Ա��ö����
        IDictionaryEnumerator IDictionary.GetEnumerator ()
        {
            return ((IOrderedDictionary) this).GetEnumerator ();
        }

        //	ɾ��ĳ����
        void IDictionary.Remove (object key)
        {
            EnsureDictionary ().Remove (key);

            for (int i = 0; i < object_list.Count; i++) {
                if (object_list[i].Key == (string) key) {
                    object_list.RemoveAt (i);
                    break;
                }
            }

            json = null;
        }
        #endregion


        //	�ض���IEnumerable�ӿ��еķ���
        #region IEnumerable Methods
        IEnumerator IEnumerable.GetEnumerator ()
        {
            return EnsureCollection ().GetEnumerator ();
        }
        #endregion


        /*	ʵ��IJsonWrapper�ӿ���
        	��ȡָ����������/����ָ����������/�����ݻ�ΪJson��ʽ�ķ���	*/
        #region IJsonWrapper Methods
        bool IJsonWrapper.GetBoolean ()
        {
            if (type != JsonType.Boolean)
                throw new InvalidOperationException (
                    "JsonData instance doesn't hold a boolean");

            return inst_boolean;
        }

        double IJsonWrapper.GetDouble ()
        {
            if (type != JsonType.Double)
                throw new InvalidOperationException (
                    "JsonData instance doesn't hold a double");

            return inst_double;
        }

        int IJsonWrapper.GetInt ()
        {
            if (type != JsonType.Int)
                throw new InvalidOperationException (
                    "JsonData instance doesn't hold an int");

            return inst_int;
        }

        long IJsonWrapper.GetLong ()
        {
            if (type != JsonType.Long)
                throw new InvalidOperationException (
                    "JsonData instance doesn't hold a long");

            return inst_long;
        }

        string IJsonWrapper.GetString ()
        {
            if (type != JsonType.String)
                throw new InvalidOperationException (
                    "JsonData instance doesn't hold a string");

            return inst_string;
        }

        void IJsonWrapper.SetBoolean (bool val)
        {
            type = JsonType.Boolean;
            inst_boolean = val;
            json = null;
        }

        void IJsonWrapper.SetDouble (double val)
        {
            type = JsonType.Double;
            inst_double = val;
            json = null;
        }

        void IJsonWrapper.SetInt (int val)
        {
            type = JsonType.Int;
            inst_int = val;
            json = null;
        }

        void IJsonWrapper.SetLong (long val)
        {
            type = JsonType.Long;
            inst_long = val;
            json = null;
        }

        void IJsonWrapper.SetString (string val)
        {
            type = JsonType.String;
            inst_string = val;
            json = null;
        }

        string IJsonWrapper.ToJson ()
        {
            return ToJson ();
        }

        void IJsonWrapper.ToJson (JsonWriter writer)
        {
            ToJson (writer);
        }
        #endregion


        //	�ض���IList�ӿ��еķ���
        #region IList Methods
        //	���ֵ
        int IList.Add (object value)
        {
            return Add (value);
        }

        //	�������
        void IList.Clear ()
        {
            EnsureList ().Clear ();
            json = null;
        }

        //	�Ƿ����ĳ��ֵ
        bool IList.Contains (object value)
        {
            return EnsureList ().Contains (value);
        }

        //	����ĳ��ֵ���±�
        int IList.IndexOf (object value)
        {
            return EnsureList ().IndexOf (value);
        }

        //	��ĳ���±�֮�����ֵ
        void IList.Insert (int index, object value)
        {
            EnsureList ().Insert (index, value);
            json = null;
        }

        //	ɾ��ĳ��ֵ
        void IList.Remove (object value)
        {
            EnsureList ().Remove (value);
            json = null;
        }

        //	ɾ��ָ���±��ֵ
        void IList.RemoveAt (int index)
        {
            EnsureList ().RemoveAt (index);
            json = null;
        }
        #endregion


        //	�ض���IOrderedDictionary�ӿ��еķ���
        #region IOrderedDictionary Methods
        //	��ȡö����
        IDictionaryEnumerator IOrderedDictionary.GetEnumerator ()
        {
            EnsureDictionary ();

            return new OrderedDictionaryEnumerator (
                object_list.GetEnumerator ());
        }

        //	��ָ����λ�ò������ֵ
        void IOrderedDictionary.Insert (int idx, object key, object value)
        {
            string property = (string) key;
            JsonData data  = ToJsonData (value);

            this[property] = data;

            KeyValuePair<string, JsonData> entry =
                new KeyValuePair<string, JsonData> (property, data);

            object_list.Insert (idx, entry);
        }

        //	ɾ��ָ��λ�õ�����
        void IOrderedDictionary.RemoveAt (int idx)
        {
            EnsureDictionary ();

            inst_object.Remove (object_list[idx].Key);
            object_list.RemoveAt (idx);
        }
        #endregion


        //	˽�з���
        #region Private Methods
        private ICollection EnsureCollection ()
        {
            if (type == JsonType.Array)
                return (ICollection) inst_array;

            if (type == JsonType.Object)
                return (ICollection) inst_object;

            throw new InvalidOperationException (
                "The JsonData instance has to be initialized first");
        }

        private IDictionary EnsureDictionary ()
        {
            if (type == JsonType.Object)
                return (IDictionary) inst_object;

            if (type != JsonType.None)
                throw new InvalidOperationException (
                    "Instance of JsonData is not a dictionary");

            type = JsonType.Object;
            inst_object = new Dictionary<string, JsonData> ();
            object_list = new List<KeyValuePair<string, JsonData>> ();

            return (IDictionary) inst_object;
        }

        private IList EnsureList ()
        {
            if (type == JsonType.Array)
                return (IList) inst_array;

            if (type != JsonType.None)
                throw new InvalidOperationException (
                    "Instance of JsonData is not a list");

            type = JsonType.Array;
            inst_array = new List<JsonData> ();

            return (IList) inst_array;
        }

        private JsonData ToJsonData (object obj)
        {
            if (obj == null)
                return null;

            if (obj is JsonData)
                return (JsonData) obj;

            return new JsonData (obj);
        }

        //	�ݹ�д����
        private static void WriteJson (IJsonWrapper obj, JsonWriter writer)
        {
            if (obj.IsString) {
                writer.Write (obj.GetString ());
                return;
            }

            if (obj.IsBoolean) {
                writer.Write (obj.GetBoolean ());
                return;
            }

            if (obj.IsDouble) {
                writer.Write (obj.GetDouble ());
                return;
            }

            if (obj.IsInt) {
                writer.Write (obj.GetInt ());
                return;
            }

            if (obj.IsLong) {
                writer.Write (obj.GetLong ());
                return;
            }

            if (obj.IsArray) {
                writer.WriteArrayStart ();
                foreach (object elem in (IList) obj)
                    WriteJson ((JsonData) elem, writer);
                writer.WriteArrayEnd ();

                return;
            }

            if (obj.IsObject) {
                writer.WriteObjectStart ();

                foreach (DictionaryEntry entry in ((IDictionary) obj)) {
                    writer.WritePropertyName ((string) entry.Key);
                    WriteJson ((JsonData) entry.Value, writer);
                }
                writer.WriteObjectEnd ();

                return;
            }
        }
        #endregion


        public int Add (object value)
        {
            JsonData data = ToJsonData (value);

            json = null;

            return EnsureList ().Add (data);
        }

        public void Clear ()
        {
            if (IsObject) {
                ((IDictionary) this).Clear ();
                return;
            }

            if (IsArray) {
                ((IList) this).Clear ();
                return;
            }
        }

        public bool Equals (JsonData x)
        {
            if (x == null)
                return false;

            if (x.type != this.type)
                return false;

            switch (this.type) {
            case JsonType.None:
                return true;

            case JsonType.Object:
                return this.inst_object.Equals (x.inst_object);

            case JsonType.Array:
                return this.inst_array.Equals (x.inst_array);

            case JsonType.String:
                return this.inst_string.Equals (x.inst_string);

            case JsonType.Int:
                return this.inst_int.Equals (x.inst_int);

            case JsonType.Long:
                return this.inst_long.Equals (x.inst_long);

            case JsonType.Double:
                return this.inst_double.Equals (x.inst_double);

            case JsonType.Boolean:
                return this.inst_boolean.Equals (x.inst_boolean);
            }

            return false;
        }

        public JsonType GetJsonType ()
        {
            return type;
        }

        public void SetJsonType (JsonType type)
        {
            if (this.type == type)
                return;

            switch (type) {
            case JsonType.None:
                break;

            case JsonType.Object:
                inst_object = new Dictionary<string, JsonData> ();
                object_list = new List<KeyValuePair<string, JsonData>> ();
                break;

            case JsonType.Array:
                inst_array = new List<JsonData> ();
                break;

            case JsonType.String:
                inst_string = default (String);
                break;

            case JsonType.Int:
                inst_int = default (Int32);
                break;

            case JsonType.Long:
                inst_long = default (Int64);
                break;

            case JsonType.Double:
                inst_double = default (Double);
                break;

            case JsonType.Boolean:
                inst_boolean = default (Boolean);
                break;
            }

            this.type = type;
        }

        public string ToJson ()
        {
            if (json != null)
                return json;

            StringWriter sw = new StringWriter ();
            JsonWriter writer = new JsonWriter (sw);
            writer.Validate = false;

            WriteJson (this, writer);
            json = sw.ToString ();

            return json;
        }

        public void ToJson (JsonWriter writer)
        {
            bool old_validate = writer.Validate;

            writer.Validate = false;

            WriteJson (this, writer);

            writer.Validate = old_validate;
        }

        public override string ToString ()
        {
            switch (type) {
            case JsonType.Array:
                return "JsonData array";

            case JsonType.Boolean:
                return inst_boolean.ToString ();

            case JsonType.Double:
                return inst_double.ToString ();

            case JsonType.Int:
                return inst_int.ToString ();

            case JsonType.Long:
                return inst_long.ToString ();

            case JsonType.Object:
                return "JsonData object";

            case JsonType.String:
                return inst_string;
            }

            return "Uninitialized JsonData";
        }

		public object ToObject(Type t)
		{
			if(type != JsonType.Object)
				throw new InvalidCastException(
						"Instance of JsonData doesn't hold an object");

			Assembly asm = Assembly.GetAssembly(t);
			object result;
			try
			{
				if(t.IsInterface && inst_object.ContainsKey("__fullname"))
					t = asm.GetType(inst_object["__fullname"]);
				result = asm.CreateInstance(t.FullName);
			}
			catch { return null; }
			foreach(PropertyInfo p_info in t.GetProperties())
			{
				if(!p_info.IsDefined(typeof(JsonAttribute), false))
					continue;
				if(!inst_object.ContainsKey(p_info.Name))
					continue;
				p_info.SetValue(result, Convert.ChangeType(inst_object[p_info.Name], p_info.PropertyType), null);

			}

			foreach(FieldInfo f_info in t.GetFields())
			{
				if(!f_info.IsDefined(typeof(JsonAttribute), false))
					continue;
				if(!inst_object.ContainsKey(f_info.Name))
					continue;
				f_info.SetValue(result, Convert.ChangeType(inst_object[f_info.Name], f_info.FieldType));
			}

			return result;
		}

		#region IConvertible ��Ա

		public TypeCode GetTypeCode()
		{
			switch(type)
			{
				case JsonType.Boolean:
					return TypeCode.Boolean;
				case JsonType.Double:
					return TypeCode.Double;
				case JsonType.Int:
					return TypeCode.Int32;
				case JsonType.Long:
					return TypeCode.Int64;
				case JsonType.String:
					return TypeCode.String;
				case JsonType.Object:
					return TypeCode.Object;
				case JsonType.Array:
					return TypeCode.Object;
			}
			return TypeCode.Empty;
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			return (bool)this;
		}

		public byte ToByte(IFormatProvider provider)
		{
			return (byte)this;
		}

		public char ToChar(IFormatProvider provider)
		{
			return (char)this;
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			if(type == JsonType.Long)
				return new DateTime(inst_long);
			if(type == JsonType.String)
				try
				{
					return DateTime.Parse(inst_string);
				}
				catch { }
			return DateTime.MinValue;
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			return (decimal)this;
		}

		public double ToDouble(IFormatProvider provider)
		{
			return (double)this;
		}

		public short ToInt16(IFormatProvider provider)
		{
			return (short)this;
		}

		public int ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		public long ToInt64(IFormatProvider provider)
		{
			return (long)this;
		}

		[CLSCompliant(false)]
		public sbyte ToSByte(IFormatProvider provider)
		{
			return 0;
		}

		public float ToSingle(IFormatProvider provider)
		{
			return 0;
		}

		public string ToString(IFormatProvider provider)
		{
			return (string)this;
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			if(type == JsonType.None)
			{
				try
				{
					Assembly asm = Assembly.GetAssembly(conversionType);
					return asm.CreateInstance(conversionType.FullName);
				}
				catch { }
				return null;
			}
			if(type == JsonType.Array && conversionType.IsArray)
				return ToArray(conversionType);
			if(conversionType.IsEnum)
			{
				return ToInt32(null);
			}
			else if(conversionType.GetInterface("IDictionary") != null)
			{
				return ToDictionary(conversionType);
			}
			else if(conversionType.GetInterface("IList") != null)
			{
				return ToList(conversionType);
			}
			return ToObject(conversionType);
		}

		[CLSCompliant(false)]
		public ushort ToUInt16(IFormatProvider provider)
		{
			return 0;
		}

		[CLSCompliant(false)]
		public uint ToUInt32(IFormatProvider provider)
		{
			return 0;
		}

		[CLSCompliant(false)]
		public ulong ToUInt64(IFormatProvider provider)
		{
			return 0;
		}

		#endregion

		private object ToArray(Type t)
		{
			if(type != JsonType.Array)
				throw new InvalidCastException(
						"Instance of JsonData doesn't hold an array");

			Assembly asm = Assembly.GetAssembly(t);
			Array result = Array.CreateInstance(t.GetElementType(), inst_array.Count);

			for(int i = 0; i < inst_array.Count; i++)
			{
				result.SetValue(Convert.ChangeType(inst_array[i], t.GetElementType()), i);
			}

			return result;
		}

		private object ToList(Type t)
		{
			if(type != JsonType.Array)
				throw new InvalidCastException(
						"Instance of JsonData doesn't hold an array");

			Assembly asm = Assembly.GetAssembly(t);
			IList result = asm.CreateInstance(t.FullName) as IList;

			bool isGeneric = result.GetType().IsGenericType;
			Type[] ts = result.GetType().GetGenericArguments();
			foreach(var x in inst_array)
			{
				if(ts.Length > 0)
					result.Add(Convert.ChangeType(x, ts[0]));
				else
					result.Add(x);
			}

			return result;
		}

		private object ToDictionary(Type t)
		{
			if(type != JsonType.Object)
				throw new InvalidCastException(
						"Instance of JsonData doesn't hold an object");

			Assembly asm = Assembly.GetAssembly(t);
			IDictionary result = asm.CreateInstance(t.FullName) as IDictionary;

			bool isGeneric = result.GetType().IsGenericType;
			Type[] ts = result.GetType().GetGenericArguments();
			foreach(var x in inst_object)
			{
				if(ts.Length > 1)
					result.Add(Convert.ChangeType(x.Key, ts[0]), Convert.ChangeType(x.Value, ts[1]));
				else
					result.Add(x.Key, x.Value);
			}
			return result;
		}
	}


    //	ʵ��IOrderedDictionary�ӿڣ�����IEnumerator<T>��
    internal class OrderedDictionaryEnumerator : IDictionaryEnumerator
    {
        IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;


        public object Current {
            get { return Entry; }
        }

        public DictionaryEntry Entry {
            get {
                KeyValuePair<string, JsonData> curr = list_enumerator.Current;
                return new DictionaryEntry (curr.Key, curr.Value);
            }
        }

        public object Key {
            get { return list_enumerator.Current.Key; }
        }

        public object Value {
            get { return list_enumerator.Current.Value; }
        }


        public OrderedDictionaryEnumerator (
            IEnumerator<KeyValuePair<string, JsonData>> enumerator)
        {
            list_enumerator = enumerator;
        }


        public bool MoveNext ()
        {
            return list_enumerator.MoveNext ();
        }

        public void Reset ()
        {
            list_enumerator.Reset ();
        }
    }
}
