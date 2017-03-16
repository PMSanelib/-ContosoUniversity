using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Dtos
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<Error>();
        }

        public List<Error> Errors { get; set; }

        public static object Lock = new object();

        public void AddError(string key, string value)
        {
            lock (Lock)
            {
                var obj = Errors.FirstOrDefault(x => x.Key == key);
                if (obj == null)
                {
                    obj = new Error { Key = key };
                    Errors.Add(obj);
                }
                obj.Lines.Add(value);
            }
        }

        public bool IsValid
        {
            get { return Errors.Count == 0; }
        }

        public override string ToString()
        {
            return Errors.Aggregate(string.Empty, (current, vo) =>
                current + string.Format("{0}:{1}{2}", vo.Key,
                vo.Lines.Aggregate(string.Empty, (c, l) => c + l), Environment.NewLine));
        }

        public string Uri { get; set; }
        public bool RedirectRequired { get; set; }

        public class Error
        {
            public string Key {get; set;}
            public List<string> Lines {get; set;}
        }
    }
}