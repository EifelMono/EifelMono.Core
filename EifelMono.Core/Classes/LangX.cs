using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using EifelMono.Core.Extensions;

namespace EifelMono.Core
{
    public class LangX : IEquatable<LangX>
    {
        #region Core
        [JsonIgnore]
        private int HashCode { get; set; }

        // I Need this for serialization
        public string ResX { get; protected set; } = "";

        public string FormattedText { get; protected set; } = "";

        // I Need this for serialization
        [JsonIgnore]
        public string Text
        {
            get
            {
                if (Args != null && Args.Length > 1)
                    return string.Format(FormattedText, Args);
                else
                    return FormattedText;
            }
        }

        public string FormatText(params object[] args)
        {
            if (args.Length == 0)
                return FormattedText;
            Args = args;
            return Text;
        }

        [JsonIgnore]
        public object[] Args { get; set; }

        public LangX()
        {
            ItemsAdd(this);
        }

        [JsonIgnore]
        public static Func<string, string> OnResXPrefix { get; set; } = null;

        public static int ResxPrefixDepth = 0;

        private static string ResXPrefix(string filePathName)
        {
            var result = Path.GetFileNameWithoutExtension(filePathName);
            if (ResxPrefixDepth > 0)
            {
                var dir = Path.GetDirectoryName(filePathName);
                for (int i = 0; i < ResxPrefixDepth; i++)
                {
                    var name = Path.GetFileName(dir);
                    if (!string.IsNullOrEmpty(name))
                    {
                        dir = Path.GetDirectoryName(dir);
                        if (result != null)
                            if (result.Length > 0)
                                result = "." + result;
                        result = name + result;
                    }
                }
            }
            if (OnResXPrefix != null)
                result = OnResXPrefix(filePathName);
            if (!result.EndsWith(".", StringComparison.Ordinal))
                result += ".";
            return result;
        }

        public static LangX NewX(string formattedText = "", [CallerMemberName] string propertyName = "", [CallerFilePath] string filePathName = "")
        {
            var prefix = Path.GetFileNameWithoutExtension(filePathName);
            return new LangX
            {
                FormattedText = formattedText,
                ResX = $"{ResXPrefix(filePathName)}{propertyName}"
            };
        }

        public static LangX PrefixNewX(string prefix, string formattedText = "", [CallerMemberName] string propertyName = "", [CallerFilePath] string filePathName = "")
        {
            if (string.IsNullOrEmpty(prefix))
                prefix = "";
            else
                if (!prefix.EndsWith(".", StringComparison.Ordinal))
                prefix += ".";
            return new LangX
            {
                FormattedText = formattedText,
                ResX = $"{prefix}{propertyName}"
            };
        }
        #endregion

        #region Items

        static First FirstInit = new First();

        protected static void Init(Assembly assembly)
        {
            if (assembly == null)
                return;
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetTypeInfo().GetCustomAttribute(typeof(LangXAttribute)) != null)
                {
                    foreach (var p in type.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                    {
                        p.GetValue(null, null);
                        break;
                    }
                }
            }
        }

        public static void Init()
        {
            if (FirstInit.IsFirst)
            {
                Init(Assembly.GetEntryAssembly());
                foreach (var assemblyName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                    Init(Assembly.Load(assemblyName));
            }
        }


        public static List<LangX> Items { get; private set; } = new List<LangX>();

        private static LangX ItemsAdd(LangX langX)
        {
            lock (Items)
            {
                if (!Items.Contains(langX))
                    Items.Add(langX);
            }
            return langX;
        }

        public static void ReadFromFile(string filename)
        {
            Load(JsonConvert.DeserializeObject<List<LangX>>(File.ReadAllText(filename)));
        }

        public static void WriteToFile(string filename)
        {
            Init();
            File.WriteAllText(filename, JsonConvert.SerializeObject(Items, Formatting.Indented));
        }

        public static void Load(List<LangX> items)
        {
            foreach (var item in items)
            {
                var langXItem = Items.First(i => i.ResX == item.ResX);
                if (langXItem != null)
                    langXItem.FormattedText = item.FormattedText;
            }
        }
        #endregion

        #region IEquatable<LangX> Members

        public bool Equals(LangX other)
        {
            if (other == null)
                return false;
            if (object.ReferenceEquals(ResX, other.ResX))
                return true;
            return string.Compare(ResX, other.ResX, StringComparison.Ordinal) == 0;
        }
        #endregion

        #region Others
        public override bool Equals(object obj)
        {
            return Equals(obj as LangX);
        }

        public override int GetHashCode()
        {
            if (HashCode == 0)
                HashCode = ResX.GetHashCode();
            return HashCode;
        }

        public override string ToString()
        {
            return $"{ResX} {Text}";
        }

        public static bool operator ==(LangX left, LangX right)
        {
            if ((object)left == null)
                return ((object)right == null);
            else if ((object)right == null)
                return ((object)left == null);
            return left.ResX.Equals(right.ResX);
        }

        public static bool operator !=(LangX left, LangX right)
        {
            return !(left?.ResX == right?.ResX);
        }
        #endregion


    }
}
