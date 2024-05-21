using System.Text;

namespace OwnSerialization;

public class CsvSerializer<T> where T : class, new()
{   
    public string SerializeToString(IList<T> data)
    {
        var rows = new StringBuilder();        

        var type = typeof(T);        
        var properties = type.GetProperties();

        foreach (var item in data)
        {            
            foreach (var property in properties)
            {   
                if (properties.LastOrDefault() == property) 
                {
                    rows.AppendLine($"{property.Name}={property.GetValue(item)}");
                }
                else
                {
                    rows.Append($"{property.Name}={property.GetValue(item)},");
                }                
            }            
        }

        return rows.ToString();
    }

    public IEnumerable<T> DeserializeFromString(string data)
    {
        var retData = new List<T>();

        if (!string.IsNullOrEmpty(data))
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            if (properties.Length == 0) return retData;

            var rows = data.Split(Environment.NewLine);
            if (rows.Length == 0) return retData;
                       
            foreach (var row in rows)
            {
                var columns = row.Split(',');
                if (columns.Length == 0) return retData;
              
                var t = new T();
                foreach (var column in columns)
                {
                    int iValue;
                    string value;
                    foreach (var property in properties)
                    {
                        if (column.Contains(property.Name))
                        {
                            if (property.PropertyType == typeof(string))
                            {
                                value = column.Replace(property.Name, string.Empty).Replace("=", string.Empty).Trim();
                                property.SetValue(t, value, null);
                            }
                            else
                            if (property.PropertyType == typeof(int))
                            {
                                iValue = Convert.ToInt32(column.Replace(property.Name, string.Empty).Replace("=", string.Empty).Trim());
                                property.SetValue(t, iValue, null);
                            }
                        }
                    }
                }
                retData.Add(t);
            }
        }

        return retData;
    }  
}