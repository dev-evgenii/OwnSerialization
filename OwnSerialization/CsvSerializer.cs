using System.Text;

namespace OwnSerialization;

public class CsvSerializer<T> where T : class, new()
{   
    public string SerializeToString(IList<T> data)
    {
        string row;
        var sb = new StringBuilder();

        var type = typeof(T);        
        var properties = type.GetProperties();

        foreach (var item in data)
        {
            row = string.Empty;
            foreach (var property in properties)
            {   
                if (properties.LastOrDefault() == property) 
                {
                    row += $"{property.Name}={property.GetValue(item)}";
                }
                else
                {
                    row += $"{property.Name}={property.GetValue(item)},";
                }                
            }
            sb.AppendLine(row);
        }

        return sb.ToString();
    }

    public IList<T> DeserializeFromString(string data)
    {
        var retData = new List<T>();

        if (!string.IsNullOrEmpty(data))
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            
            if (properties.Length > 0)
            {
                var rows = data.Split(Environment.NewLine);
                if (rows.Length > 0)
                {
                    foreach (var row in rows)
                    {
                        var columns = row.Split(',');
                        if (columns.Length > 0)
                        {
                            var t = new T();
                            foreach (var column in columns)
                            {
                                int iValue;
                                string value;                                
                                foreach(var property in properties)
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
                }
            }
        }

        return retData;
    }  
}