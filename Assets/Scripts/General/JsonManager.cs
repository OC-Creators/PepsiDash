using System.Text;
using System.IO;
using UnityEngine;

namespace General {
    public class JsonManager<T>
    {
        public string json;
        public string path;

        public JsonManager(string path) {
            this.path = path;
            Import(path);
        }

        public void Import(string path) {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    this.json = sr.ReadToEnd();
                }
            }
            
        }

        public void Export(string json_str, string path) {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLineAsync(json_str);
                } 
            }
        }

        public T Load() {
            return JsonUtility.FromJson<T>(json);
        }

        public void Dump(T obj) {
            var json_str = JsonUtility.ToJson(obj, prettyPrint:true);
            Export(json_str, this.path);
        }
    }
}