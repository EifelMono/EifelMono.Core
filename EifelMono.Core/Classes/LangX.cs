using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using EifelMono.Core.Extensions;

namespace EifelMono.Core
{
    public class LangXCore
    {
        public string ResX { get; set; } = "";

        public string FormatText { get; set; } = "";
    }

    public class LangX : LangXCore, IEquatable<LangX>
    {
        #region Attribute
        [AttributeUsage(AttributeTargets.Class)]
        public class LangXAttribute : Attribute
        {
        }
        #endregion

        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        public LangX()
        {
            ItemsAdd(this);
        }
        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        public LangX(string formatText, [CallerMemberName] string propertyName = "", [CallerFilePath] string filePathName = "") :
            this()
        {
            ResX = $"{ResXPrefix(filePathName)}{propertyName}";
            FormatText = formatText;
        }

        #region Core
        [JsonIgnore]
        private int HashCode { get; set; }

        // I Need this for serialization
        [JsonIgnore]
        public string Text
        {
            get
            {
                if (Args != null && Args.Length > 0)
                    try
                    {
                        return string.Format(FormatText, Args);
                    }
                    catch
                    {
                        return FormatText;
                    }
                else
                    return FormatText;
            }
        }

        public string FormatParams(params object[] args)
        {
            if (args.Length == 0)
                return FormatText;
            Args = args;
            return Text;
        }

        [JsonIgnore]
        public object[] Args { get; set; }

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

        #endregion

        #region Items

        static First FirstInit = new First();

        protected static void FindAssemblyLangXs(Assembly assembly)
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

        public static void FindLangXs()
        {
            if (FirstInit.IsFirst)
            {
                try
                {
                    FindAssemblyLangXs(Assembly.GetEntryAssembly());
                    foreach (var assemblyName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                        try
                        {
                            FindAssemblyLangXs(Assembly.Load(assemblyName));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
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
            Load(JsonConvert.DeserializeObject<List<LangXCore>>(File.ReadAllText(filename)));
        }

        public static void WriteToFile(string filename)
        {
            FindLangXs();
            File.WriteAllText(filename, JsonConvert.SerializeObject(Items, Formatting.Indented));
        }

        public static void Load(List<LangXCore> items)
        {
            foreach (var item in items)
            {
                try
                {
                    var langXItem = Items.First(i => i.ResX == item.ResX);
                    if (langXItem != null)
                        langXItem.FormatText = item.FormatText;
                }
                catch (Exception ex)
                {
                    ex.LogException();
                }
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

        public static LangX Empty { get; private set; } = new LangX("");
    }
}
